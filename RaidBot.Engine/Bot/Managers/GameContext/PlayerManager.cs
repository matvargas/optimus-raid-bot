using RaidBot.Data.IO.D2P.Map;
using RaidBot.Engine.Bot.Data.Context;
using RaidBot.Engine.Utility;
using RaidBot.Engine.Utility.Pathfinding;
using RaidBot.Protocol.Enums;
using RaidBot.Protocol.Messages;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidBot.Engine.Bot.Managers.GameContext
{
    public class PlayerManager : Manager
    {
        Random rnd = new Random();

        internal void UseCharacPoint(object boostableCharacteristicEnunm)
        {
            throw new NotImplementedException();
        }

        public MovementManager Movement { get; }

        public event Action<bool> Regen;
        public void OnRegen(bool regen)
        {
            if (Regen != null) Regen(regen);
        }

        public PlayerManager(Brain brain) : base(brain)
        {
            Movement = new MovementManager(brain);
            Regen += PlayerManager_Regen;
        }

        private async void PlayerManager_Regen(bool obj)
        {
            if (obj)
            {
                await Task.Delay(1000);
                Log("Begin regen...");
                Brain.SendMessage(new EmotePlayRequestMessage().InitEmotePlayRequestMessage(1));
            }
        }

        /// <summary>
        /// Try to incrase a chareacteristicv and roud touse to the charac ranges
        /// it't return te real number of point used
        /// </summary>
        /// <param name="charac">Characteristic to incrasse</param>
        /// <param name="toUse">Nbr charac point to use (will be rounded to math charac ranges)</param>
        /// <returns></returns>
        public uint UseCharacPoint(BoostableCharacteristicEnum charac, uint toUse)
        {
            List<List<uint>> GetRanges()
            {
                switch (charac)
                {
                    case BoostableCharacteristicEnum.BOOSTABLE_CHARAC_AGILITY:
                        return Brain.State.Player.Breed.StatsPointsForAgility;
                    case BoostableCharacteristicEnum.BOOSTABLE_CHARAC_CHANCE:
                        return Brain.State.Player.Breed.StatsPointsForChance;
                    case BoostableCharacteristicEnum.BOOSTABLE_CHARAC_INTELLIGENCE:
                        return Brain.State.Player.Breed.StatsPointsForIntelligence;
                    case BoostableCharacteristicEnum.BOOSTABLE_CHARAC_STRENGTH:
                        return Brain.State.Player.Breed.StatsPointsForStrength;
                    case BoostableCharacteristicEnum.BOOSTABLE_CHARAC_VITALITY:
                        return Brain.State.Player.Breed.StatsPointsForVitality;
                    case BoostableCharacteristicEnum.BOOSTABLE_CHARAC_WISDOM:
                        return Brain.State.Player.Breed.StatsPointsForWisdom;
                    default:
                        throw new Exception("Invalide charac");
                }
            }
            short GetCurrentState()
            {
                switch (charac)
                {
                    case BoostableCharacteristicEnum.BOOSTABLE_CHARAC_AGILITY:
                        return Brain.State.Player.Characteristics.Get().Agility.Base;
                    case BoostableCharacteristicEnum.BOOSTABLE_CHARAC_CHANCE:
                        return Brain.State.Player.Characteristics.Get().Chance.Base;
                    case BoostableCharacteristicEnum.BOOSTABLE_CHARAC_INTELLIGENCE:
                        return Brain.State.Player.Characteristics.Get().Intelligence.Base;
                    case BoostableCharacteristicEnum.BOOSTABLE_CHARAC_STRENGTH:
                        return Brain.State.Player.Characteristics.Get().Strength.Base;
                    case BoostableCharacteristicEnum.BOOSTABLE_CHARAC_VITALITY:
                        return Brain.State.Player.Characteristics.Get().Vitality.Base;
                    case BoostableCharacteristicEnum.BOOSTABLE_CHARAC_WISDOM:
                        return Brain.State.Player.Characteristics.Get().Wisdom.Base;
                    default:
                        throw new Exception("Invalide charac");
                }
            }
            uint GetPointToUse()
            {
                List<List<uint>> range = GetRanges();
                uint pointToUse = 0;
                short fake = GetCurrentState();
                while (toUse > 0)
                {
                    foreach (List<uint> curr in range)
                    {
                        if (fake < curr[0])
                        {
                            if (toUse >= curr[1])
                            {
                                pointToUse += curr[1];
                                toUse -= curr[1];
                                fake++;
                                break;
                            }
                            else
                                return pointToUse;
                        }
                    }
                }
                return pointToUse;
            }
            toUse = GetPointToUse();
            if (Brain.CurrentState.Get() == Brain.BrainState.Fight)
            {
                Error("Can't upgrade charac in fight");
                return 0;
            }
            if (toUse > 0)
                Brain.SendMessage(new StatsUpgradeRequestMessage().InitStatsUpgradeRequestMessage(false, (byte)charac, (short)toUse));
            return toUse;
        }

        public async Task<bool> ChangeMap(DirectionsEnum dir)
        {
            List<short> possible = new List<short>();
            short x = 0;
            foreach (CellData cell in Brain.State.CurrentMap.Get().Cells)
            {
                if (cell.mov && cell.allowWalkRP && MovementManager.GetChangeMapDirection(x, cell.mapChangeData) == dir && x != Brain.State.Player.RolePlayInformations.Get().Disposition.CellId)
                    possible.Add(x);
                x++;
            }
            if (possible.Count == 0)
                return false;
            Log("Changeing map ...");
            if (!Brain.PartyManager.IsFollower)
                if (await Brain.PartyManager.PropageChangeMap(dir) != 0)
                    Error("Can't propage movement to followrs !");
            int maxRetry = 4;
            while (maxRetry-- > 0)
                if (await Movement.Move(possible[rnd.Next(0, possible.Count)], true))
                    return true;
            return false;
        }


        public async void UseInteractive(ElementDetails element, SkillDetails skill)
        {
            // Sun door
            if (skill.ElementSkill.SkillId == 339)
            {
                try
                {
                    await Brain.PlayerManager.Movement.Move(element.CellId);
                    return;
                }
                catch
                {
                    Error("Can't go to door at {0}", element.CellId);
                }
            }
            List<short> cells = Brain.PlayerManager.Movement.GetContactCells(element.CellId);
            foreach (short cell in cells)
            {
                try
                {
                    Log("Go to interactive at {0}", element.CellId);
                    cells.RemoveAt(0);
                    await Brain.PlayerManager.Movement.Move(cell);
                    Brain.SendMessage(new InteractiveUseRequestMessage().InitInteractiveUseRequestMessage(element.Id, skill.ElementSkill.SkillInstanceUid));
                    Log("Sendeing use request {0}", cell);
                    break;
                }
                catch
                {
                    Error("Can't go to " + cell);
                }
            }
        }


        public async Task<bool> AttackGroupMonster(GroupMonsterDetails group)
        {
            int oldGroupPos = group.CellId;
            Point p = PathingUtils.CellIdToCoord((short)group.CellId);
            short[] cells = PathingUtils.CellsByDistance(PathingUtils.PointsToCellids(SpellShape.GetInstance().ShapeCrossAndStar(p.X, p.Y, 0, 1)), Brain.State.Player.RolePlayInformations.Get().Disposition.CellId).Reverse().ToArray();
            foreach (short cell in cells)
            {
                if (cell >= 0 && cell < 560 && Brain.State.CurrentMap.Get().Cells[cell].mov && Brain.State.CurrentMap.Get().Cells[cell].allowWalkRP)
                {
                    try
                    {
                        TaskCompletionSource<bool> task = new TaskCompletionSource<bool>();
                        Log("Go to group monsters at {0}", group.CellId);
                        if (!await Brain.PlayerManager.Movement.Move(cell, false))
                            return false;
                        bool done = false;
                        void changed(Brain.BrainState state)
                        {
                            if (!done)
                            {
                                done = true;
                                Brain.CurrentState.changed -= changed;
                                if (state == Brain.BrainState.Fight)
                                    task.SetResult(true);
                                else
                                    task.SetResult(false);
                            }
                        }
                        //async Task timeout()
                        //{
                        //    await Task.Delay(8000);
                        //    if (done)
                        //        return;
                        //    Error("Attacking monster timeout !");
                        //    Brain.CurrentState.changed -= changed;
                        //    task.SetResult(false);
                        //}
                        //timeout();
                        Brain.CurrentState.changed += changed;
                        if (oldGroupPos != group.CellId)
                        {
                            Warn("Group monster has moved pending attack !");
                            return false;
                        }
                        Brain.SendMessage(new GameRolePlayAttackMonsterRequestMessage().InitGameRolePlayAttackMonsterRequestMessage(group.Informatons.ContextualId));
                        return await task.Task;
                    }
                    catch
                    {
                        Error("Can't go to {0}", cell);
                        return false;
                    }
                }
            }
            return false;
        }
    }
}
