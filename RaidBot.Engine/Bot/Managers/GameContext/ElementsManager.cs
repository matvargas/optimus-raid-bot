using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RaidBot.Common.Default.Loging;
using RaidBot.Data.IO.D2I;
using RaidBot.Data.IO.D2O;
using RaidBot.Engine.Bot.Data;
using RaidBot.Engine.Bot.Data.Context;
using RaidBot.Protocol.DataCenter;
using RaidBot.Protocol.Messages;
using RaidBot.Protocol.Types;
namespace RaidBot.Engine.Bot.Managers.GameContext
{
    public class ElementsManager : Manager
    {

        public ObservableProperty<int[]> UsedCells { get; }

        // All types of actors in signle collections
        public ObservableProperty<Dictionary<double, GameContextActorInformations>> Actors { get; }
        /// <summary>
        /// Different type of actors
        /// </summary>
        public ObservableProperty<Dictionary<double, GroupMonsterDetails>> GroupMonster { get; }

        public ObservableProperty<Dictionary<double, GameRolePlayNpcWithQuestInformations>> NpcWithQuest { get; }
        public ObservableProperty<Dictionary<double, NpcDetails>> Npc { get; }
        public ObservableProperty<Dictionary<double, GameRolePlayTreasureHintInformations>> TreasureHint { get; }
        public ObservableProperty<Dictionary<double, GameRolePlayTaxCollectorInformations>> TaxCollector { get; }
        public ObservableProperty<Dictionary<double, GameRolePlayPrismInformations>> Prismes { get; }
        public ObservableProperty<Dictionary<double, GameRolePlayCharacterInformations>> Character { get; }
        public ObservableProperty<Dictionary<double, GameRolePlayMerchantInformations>> Merchant { get; }

        public ObservableProperty<Dictionary<double, ElementDetails>> Elements;

        public ElementsManager(Brain brain) : base(brain)
        {
            Brain.State.MapComplementaryInformations.changed += MapComplementaryInformations_changed;
            GroupMonster = new ObservableProperty<Dictionary<double, GroupMonsterDetails>>(new Dictionary<double, GroupMonsterDetails>());
            NpcWithQuest = new ObservableProperty<Dictionary<double, GameRolePlayNpcWithQuestInformations>>(new Dictionary<double, GameRolePlayNpcWithQuestInformations>());
            Npc = new ObservableProperty<Dictionary<double, NpcDetails>>(new Dictionary<double, NpcDetails>());
            TreasureHint = new ObservableProperty<Dictionary<double, GameRolePlayTreasureHintInformations>>(new Dictionary<double, GameRolePlayTreasureHintInformations>());
            TaxCollector = new ObservableProperty<Dictionary<double, GameRolePlayTaxCollectorInformations>>(new Dictionary<double, GameRolePlayTaxCollectorInformations>());
            Prismes = new ObservableProperty<Dictionary<double, GameRolePlayPrismInformations>>(new Dictionary<double, GameRolePlayPrismInformations>());
            Character = new ObservableProperty<Dictionary<double, GameRolePlayCharacterInformations>>(new Dictionary<double, GameRolePlayCharacterInformations>());
            Actors = new ObservableProperty<Dictionary<double, GameContextActorInformations>>(new Dictionary<double, GameContextActorInformations>());
            Merchant = new ObservableProperty<Dictionary<double, GameRolePlayMerchantInformations>>(new Dictionary<double, GameRolePlayMerchantInformations>());
            Elements = new ObservableProperty<Dictionary<double, ElementDetails>>(new Dictionary<double, ElementDetails>());
            UsedCells = new ObservableProperty<int[]>(Enumerable.Repeat(0, 560).ToArray());
        }

        public ElementDetails ElementByCellId(short cell)
        {
            foreach (ElementDetails e in Elements.Get().Values)
                if (e.CellId == cell)
                    return e;
            return null;
        }

        public NpcDetails NpcByCellId(short cell)
        {
            foreach (NpcDetails e in Npc.Get().Values)
                if (e.Informations.Disposition.CellId == cell)
                    return e;
            return null;
        }

        private void MapComplementaryInformations_changed(Protocol.Messages.MapComplementaryInformationsDataMessage data)
        {
            UsedCells.Set(Enumerable.Repeat(0, 560).ToArray());
            Dictionary<double, ElementDetails> elements = new Dictionary<double, ElementDetails>();
            Dictionary<double, StatedElement> stats = new Dictionary<double, StatedElement>();
            foreach (StatedElement stat in data.StatedElements)
            {
                stats[stat.ElementId] = stat;
            }
            foreach (InteractiveElement elem in data.InteractiveElements)
            {
                if (!elem.OnCurrentMap)
                    continue;
                ElementDetails e;
                if (stats.ContainsKey(elem.ElementId))
                    e = new ElementDetails(stats[elem.ElementId], elem);
                else
                    e = new ElementDetails(elem, Brain);
                if (e.IsSolid)
                    UsedCells.Get()[e.CellId]++;
                elements.Add(e.Id, e);
            }
            Elements.Set(elements);

            Dictionary<double, GroupMonsterDetails> groupMonster = new Dictionary<double, GroupMonsterDetails>();
            Dictionary<double, GameRolePlayNpcWithQuestInformations> npcWithQuest = new Dictionary<double, GameRolePlayNpcWithQuestInformations>();
            Dictionary<double, NpcDetails> npc = new Dictionary<double, NpcDetails>();
            Dictionary<double, GameRolePlayTreasureHintInformations> treasureHint = new Dictionary<double, GameRolePlayTreasureHintInformations>();
            Dictionary<double, GameRolePlayTaxCollectorInformations> taxCollector = new Dictionary<double, GameRolePlayTaxCollectorInformations>();
            Dictionary<double, GameRolePlayPrismInformations> prismes = new Dictionary<double, GameRolePlayPrismInformations>();
            Dictionary<double, GameRolePlayCharacterInformations> character = new Dictionary<double, GameRolePlayCharacterInformations>();
            Dictionary<double, GameContextActorInformations> actors = new Dictionary<double, GameContextActorInformations>();
            Dictionary<double, GameRolePlayMerchantInformations> merchant = new Dictionary<double, GameRolePlayMerchantInformations>();
            foreach (GameContextActorInformations actor in data.Actors)
            {
                if (actor is GameRolePlayGroupMonsterInformations)
                    groupMonster.Add(actor.ContextualId, new GroupMonsterDetails((GameRolePlayGroupMonsterInformations)actor));
                else if (actor is GameRolePlayNpcWithQuestInformations)
                    npcWithQuest.Add(actor.ContextualId, (GameRolePlayNpcWithQuestInformations)actor);
                else if (actor is GameRolePlayNpcInformations)
                    npc.Add(actor.ContextualId, new NpcDetails((GameRolePlayNpcInformations)actor));
                else if (actor is GameRolePlayTreasureHintInformations)
                    treasureHint.Add(actor.ContextualId, (GameRolePlayTreasureHintInformations)actor);
                else if (actor is GameRolePlayTaxCollectorInformations)
                    taxCollector.Add(actor.ContextualId, (GameRolePlayTaxCollectorInformations)actor);
                else if (actor is GameRolePlayPrismInformations)
                    prismes.Add(actor.ContextualId, (GameRolePlayPrismInformations)actor);
                else if (actor is GameRolePlayCharacterInformations)
                    character.Add(actor.ContextualId, (GameRolePlayCharacterInformations)actor);
                else if (actor is GameRolePlayMerchantInformations)
                    merchant.Add(actor.ContextualId, (GameRolePlayMerchantInformations)actor);
                else
                    throw new Exception("Undefined actor type !");
                actors.Add(actor.ContextualId, actor);
                UsedCells.Get()[actor.Disposition.CellId]++;
            }
            GroupMonster.Set(groupMonster);
            NpcWithQuest.Set(npcWithQuest);
            Npc.Set(npc);
            TreasureHint.Set(treasureHint);
            TaxCollector.Set(taxCollector);
            Prismes.Set(prismes);
            Character.Set(character);
            Actors.Set(actors);
            Log("Actors updated ({0} character, {6} group monster, {1} npc, {2} npc with quest, {3} treasure hint, {4} tax collector, total {5})"
                , character.Count, npc.Count, npcWithQuest.Count, treasureHint.Count, taxCollector.Count, actors.Count, groupMonster.Count);

        }

        public void UpdateInteractive(InteractiveElementUpdatedMessage msg)
        {
            if (msg.InteractiveElement.OnCurrentMap == false)
                return;
            if (Elements.Get().ContainsKey(msg.InteractiveElement.ElementId))
            {
                Elements.Get()[msg.InteractiveElement.ElementId].Interactive = msg.InteractiveElement;
                Elements.Get()[msg.InteractiveElement.ElementId].LoadSkills();
                Elements.OnChanged();
            }
        }

        public void UpdateStated(StatedElementUpdatedMessage msg)
        {
            if (msg.StatedElement.OnCurrentMap == false)
                return;
            if (Elements.Get().ContainsKey(msg.StatedElement.ElementId))
            {
                Elements.Get()[msg.StatedElement.ElementId].Stated = msg.StatedElement;
                Elements.OnChanged();
            }
        }

        public void HandleMovement(GameMapMovementMessage msg)
        {
            if (!Actors.Get().ContainsKey(msg.ActorId))
            {
                Error("Updating unknow actor !");
                return;
            }
            GameContextActorInformations actor = Actors.Get()[msg.ActorId];
            UsedCells.Get()[actor.Disposition.CellId]--;
            actor.Disposition.CellId = msg.KeyMovements[msg.KeyMovements.Length - 1];
            UsedCells.Get()[actor.Disposition.CellId]++;
            Actors.OnChanged();
        }

        public void AddActor(GameRolePlayActorInformations actor)
        {
            if (actor is GameRolePlayGroupMonsterInformations)
            {
                GameRolePlayGroupMonsterInformations group = (GameRolePlayGroupMonsterInformations)actor;
                GroupMonster.Get()[actor.ContextualId] = new GroupMonsterDetails(group);
            }
            else if (actor is GameRolePlayNpcWithQuestInformations)
                NpcWithQuest.Get()[actor.ContextualId] = (GameRolePlayNpcWithQuestInformations)actor;
            else if (actor is GameRolePlayNpcInformations)
                Npc.Get()[actor.ContextualId] = new NpcDetails((GameRolePlayNpcInformations)actor);
            else if (actor is GameRolePlayTreasureHintInformations)
                TreasureHint.Get()[actor.ContextualId] = (GameRolePlayTreasureHintInformations)actor;
            else if (actor is GameRolePlayTaxCollectorInformations)
                TaxCollector.Get()[actor.ContextualId] = (GameRolePlayTaxCollectorInformations)actor;
            else if (actor is GameRolePlayPrismInformations)
                Prismes.Get()[actor.ContextualId] = (GameRolePlayPrismInformations)actor;
            else if (actor is GameRolePlayCharacterInformations)
                Character.Get()[actor.ContextualId] = (GameRolePlayCharacterInformations)actor;
            else if (actor is GameRolePlayMerchantInformations)
                Merchant.Get()[actor.ContextualId] = (GameRolePlayMerchantInformations)actor;
            else
                throw new Exception("Undefined actor type !");
            UsedCells.Get()[actor.Disposition.CellId]++;
            Actors.Get()[actor.ContextualId] = actor;
        }

        public void RemoveActor(double id)
        {
            GameContextActorInformations actor = Actors.Get()[id];
            if (actor is GameRolePlayGroupMonsterInformations)
                GroupMonster.Get().Remove(actor.ContextualId);
            else if (actor is GameRolePlayNpcWithQuestInformations)
                NpcWithQuest.Get().Remove(actor.ContextualId);
            else if (actor is GameRolePlayNpcInformations)
                Npc.Get().Remove(actor.ContextualId);
            else if (actor is GameRolePlayTreasureHintInformations)
                TreasureHint.Get().Remove(actor.ContextualId);
            else if (actor is GameRolePlayTaxCollectorInformations)
                TaxCollector.Get().Remove(actor.ContextualId);
            else if (actor is GameRolePlayPrismInformations)
                Prismes.Get().Remove(actor.ContextualId);
            else if (actor is GameRolePlayCharacterInformations)
                Character.Get().Remove(actor.ContextualId);
            else if (actor is GameRolePlayMerchantInformations)
                Merchant.Get().Remove(actor.ContextualId);
            else
                throw new Exception("Undefined actor type !");
            UsedCells.Get()[actor.Disposition.CellId]--;
            Actors.Get().Remove(id);
        }
    }
}
