using RaidBot.Engine.Bot.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RaidBot.Protocol.Types;
using RaidBot.Engine.Utility.Pathfinding;
using System.Drawing;
using RaidBot.Protocol.Messages;
using static RaidBot.Engine.Bot.Managers.DellayManager;
using RaidBot.Engine.Bot.Frames.GameContext;
using RaidBot.Engine.Bot.Managers.GameContext;

namespace RaidBot.Engine.Bot.Managers.FightContext
{
    public class FightManager : Manager
    {

        #region Generic types

        public enum FightState
        {
            None,
            Loading,
            Die,
            Reconnecting,
            Placement,
            PlacementDone,
            PlacementError,
            Fighting,
        }

        public enum TurnState
        {
            Other,
            Self,
        }

        public class FighterDetails
        {
            public GameFightFighterInformations Fighter { get; set; }
            public short ActionPointBase { get; set; }
            public short MovementPointBase { get; set; }
            public int InvockationRemaning { get; set; }
            public int Distance { get; set; }
            public short CellId
            {
                get
                {
                    return Fighter.Disposition.CellId;
                }
                set
                {
                    Fighter.Disposition.CellId = value;
                }
            }

            public bool IsCriticalState
            {
                get
                {
                    //TODO
                    return false;
                }
            }

            public void EndRound()
            {
                Fighter.Stats.ActionPoints = ActionPointBase;
                Fighter.Stats.MovementPoints = MovementPointBase;
            }

            public FighterDetails(GameFightFighterInformations fighter)
            {
                Fighter = fighter;
                ActionPointBase = fighter.Stats.ActionPoints;
                MovementPointBase = fighter.Stats.MovementPoints;
            }
        }

        #endregion

        #region Properties

        private SpellStackItem lastSpell = null;
        public bool IsFighting { get { return State != FightState.None; } }
        public ObservableProperty<FightState> State { get; set; }
        public ObservableProperty<TurnState> Turn { get; set; }

        public Ia Ia { get; private set; }
        public short[] PlacementForDefender { get; set; }
        public FighterDetails Player
        {
            get
            {
                if (Fighters.Get().ContainsKey(Brain.State.Player.BaseInformation.Get().Id_))
                {
                    return Fighters.Get()[Brain.State.Player.BaseInformation.Get().Id_];
                }
                return null;
            }
        }
        public ObservableProperty<Dictionary<double, FighterDetails>> Fighters { get; set; }
        public ObservableProperty<Dictionary<double, short>> EntitiesDispositons { get; private set; }
        public CharacterCharacteristicsInformations FighterStats { get; set; }

        #endregion

        #region Main logic

        public FightManager(Brain brain) : base(brain)
        {
            PlacementForDefender = null;
            EntitiesDispositons = new ObservableProperty<Dictionary<double, short>>(new Dictionary<double, short>());
            Fighters = new ObservableProperty<Dictionary<double, FighterDetails>>(new Dictionary<double, FighterDetails>());
            State = new ObservableProperty<FightState>(FightState.None);
            State.changed += State_changed;
            Turn = new ObservableProperty<TurnState>(TurnState.Other);
            Turn.changed += Turn_changed;
            Ia = new Ia(Brain);
            Brain.CurrentState.changed += CurrentState_changed;
        }

        private void CurrentState_changed(Brain.BrainState data)
        {
            if (data == Brain.BrainState.Fight)
                return;
            State.Set(FightState.None);
        }

        private async void Turn_changed(TurnState data)
        {
            if (data == TurnState.Other)
                return;
            await Task.Delay(DellayManager.GetInstance().Get(DellayType.FightTurnBegin));
            if (State != FightState.Fighting && State != FightState.PlacementDone)
            {
                // Warning maybe this function can make simoultaneous call to process turn :X
                Warn("Fight is not ready, waiting 1sec before aborting turn");
                int cptr = 0;
                while (State != FightState.Fighting && cptr < 10)
                {
                    await Task.Delay(100);
                    cptr++;
                }
                if (State != FightState.Fighting)
                {
                    Log("Aborting turn");
                    return;
                }
            }
            foreach (SpellStackItem item in Brain.State.UserConfig.Get().SpellStack)
                item.MyTurn();
            try
            {
                await Ia.ProcessTurn();
            }
            catch (Exception e)
            {
                Error(e.ToString());
            }
        }

        private async void State_changed(FightState data)
        {
            switch (State.Get())
            {
                case FightState.None:
                    PlacementForDefender = null;
                    Fighters.Set(new Dictionary<double, FighterDetails>());
                    EntitiesDispositons.Set(new Dictionary<double, short>());
                    break;
                case FightState.Reconnecting:
                    PlacementForDefender = null;
                    Fighters.Set(new Dictionary<double, FighterDetails>());
                    EntitiesDispositons.Set(new Dictionary<double, short>());
                    break;
                case FightState.Placement:
                    await Task.Delay(DellayManager.GetInstance().Get(DellayType.FightPlacement));
                    if (Brain.State.UserConfig.Get().FightPlacement != FightPlacementType.None)
                        ProcessPlacement(Brain.State.UserConfig.Get().FightPlacement == FightPlacementType.Near);
                    else
                        State.Set(FightState.PlacementDone);
                    break;
                case FightState.PlacementError:
                case FightState.PlacementDone:
                    await Task.Delay(DellayManager.GetInstance().Get(DellayType.FightSayReady));
                    if (State.Get() == FightState.PlacementError || State.Get() == FightState.PlacementDone)
                    {
                        Log("Fight say ready !");
                        Brain.SendMessage(new GameFightReadyMessage().InitGameFightReadyMessage(true)); // Important todo check if the fight is not already started
                    }
                    else
                        Log("Can't say ready, fight already started !");
                    PlacementForDefender = null;
                    State.Set(FightState.Fighting);
                    break;
                case FightState.Fighting:
                    foreach (SpellStackItem sp in Brain.State.UserConfig.Get().SpellStack)
                        sp.LoadPredicate(Brain);
                    try
                    {
                        Brain.CurrentState.Set(Brain.BrainState.Fight);
                        Log("Fight preparation done !");
                        Brain.State.UserConfig.Get().SpellStack.ForEach((e) => e.Clear());
                        Player.InvockationRemaning = Brain.State.Player.InvockPoint;
                        if (Fighters.Get() != null)
                            foreach (FighterDetails fight in Fighters.Get().Values)
                                if (fight.Fighter.Stats.Summoned && fight.Fighter.Stats.Summoner == Player.Fighter.ContextualId)
                                    Player.InvockationRemaning--;
                    }
                    catch
                    {
                        Warn("Can't process fight begin");
                    }
                    break;
            }
        }

        public void ProcessPlacement(bool nearFromEnnemies)
        {
            if (PlacementForDefender == null || PlacementForDefender.Length == 0)
            {
                Log("Unabel to process placement, no cells avaiable !");
                return;
            }
            IDictionary<short, double> distances = new Dictionary<short, double>();
            foreach (short cell in PlacementForDefender)
            {
                distances[cell] = double.MaxValue;
                Point cp = PathingUtils.CellIdToCoord(cell);
                foreach (FighterDetails fight in Fighters.Get().Values.Where(fight => fight.Fighter.TeamId != Player.Fighter.TeamId))
                {
                    double distance = (double)PathingUtils.DistanceToPoint(cp, PathingUtils.CellIdToCoord(EntitiesDispositons.Get()[fight.Fighter.ContextualId]));
                    distances[cell] = Math.Min(distances[cell], distance);
                }
            }
            short[] sorted;
            if (nearFromEnnemies)
                sorted = distances.OrderBy(elem => elem.Value).Select(e => e.Key).ToArray();
            else
                sorted = distances.OrderByDescending(elem => elem.Value).Select(e => e.Key).ToArray();
            foreach (short cell in sorted)
            {
                if (cell == EntitiesDispositons.Get()[Player.Fighter.ContextualId])
                {
                    State.Set(FightState.PlacementDone);
                    return;
                }
                foreach (FighterDetails detail in Fighters.Get().Values)
                {
                    if (EntitiesDispositons.Get()[detail.Fighter.ContextualId] == cell)
                        goto cont;
                }
                Brain.SendMessage(new GameFightPlacementPositionRequestMessage().InitGameFightPlacementPositionRequestMessage(cell));
                Log("Process placement from {0} to {0} ", EntitiesDispositons.Get()[Player.Fighter.ContextualId], cell);
                return;
            cont:
                continue;
            }
        }

        #endregion

        #region API

        #region Basics (move and turn)

        public async void EndTurn()
        {
            await Task.Delay(DellayManager.GetInstance().Get(DellayType.FightTurnFinish));
            try
            {
                if (Brain.CurrentState.Get() != Brain.BrainState.Fight || Turn != TurnState.Self)
                    Warn("Can't end turn");
                else if (Fighters.Get() != null && Player != null && Fighters.Get().Count > 1 && State.Get() != FightState.Die)
                {
                    Brain.FightManager.Player.EndRound();
                    Brain.SendMessage(new GameFightTurnFinishMessage().InitGameFightTurnFinishMessage(false));
                    Log("Turn finished !");
                }
            }
            catch (Exception e)
            {
                Error("Failed to end turn !\n{0}", e.ToString());
            }
        }

        public async Task<bool> Move(short cellid)
        {
            await Task.Delay(DellayManager.GetInstance().Get(DellayType.FightMovement));
            if (Brain.CurrentState.Get() != Brain.BrainState.Fight || Player == null || Turn.Get() != TurnState.Self)
                return false;
            if (cellid == Brain.FightManager.Player.CellId)
                return true;
            Pathfinder pf = new Pathfinder(Brain.FightManager.EntitiesDispositons.Get().Values.ToArray());
            pf.SetMap(Brain.State.CurrentMap.Get(), false);
            List<CellWithOrientation> path = pf.GetPath(Brain.FightManager.Player.CellId, cellid);
            if (path == null || path.Count > Brain.FightManager.Player.Fighter.Stats.MovementPoints + 1)
                return false;
            TaskCompletionSource<bool> task = new TaskCompletionSource<bool>();
            bool res = false;
            int delay = MovementManager.GetMoveDellay(path, MovementManager.WALK_LINEAR_VELOCITY, MovementManager.WALK_HORIZONTAL_DIAGONAL_VELOCITY, MovementManager.WALK_VERTICAL_DIAGONAL_VELOCITY);
            void HanldeSequenceEnd()
            {
                Brain.FightFrame.SequenceDone -= HanldeSequenceEnd;
                if (Player == null)
                {
                    Warn("Can't done movement, probabley disconnected !");
                    if (!task.Task.IsCompleted)
                        task.SetResult(false);
                    return;
                }
                if (res)
                {
                    Log("Movement sequence ended");
                    try
                    {
                        Player.CellId = (short)path[path.Count - 1].Id;
                    }
                    catch
                    {
                        Error("Failed to update player disposition, stashed into entitiesdispositions");
                    }
                    EntitiesDispositons.Get()[Player.Fighter.ContextualId] = (short)path[path.Count - 1].Id;
                }
                else
                    Error("movement sequence ended but false result");
                if (!task.Task.IsCompleted)
                    task.SetResult(res);
            }
            void HandleMoveResult(MovementResultEnum result)
            {
                if (result != MovementResultEnum.Error)
                    Log("Server accepte movement");
                else
                    Error("Server refuse movement (noMovementMessage)");
                Brain.MovementFrame.MovementResult -= HandleMoveResult;
                res = result != MovementResultEnum.Error;
                Brain.PlayerManager.Movement.OnMovementPerformed(path, delay, result != MovementResultEnum.Error);
            }
            if (Brain.CurrentState.Get() != Brain.BrainState.Fight)
            {
                Warn("Movement canceled, not in fight");
                return false;
            }
            Brain.FightFrame.SequenceDone += HanldeSequenceEnd;
            Brain.MovementFrame.MovementResult += HandleMoveResult;
            Brain.FightFrame.AckResponsDellay = delay;
            Brain.SendMessage(new GameMapMovementRequestMessage().InitGameMapMovementRequestMessage(PathingUtils.GetCompressedPath(path), Brain.State.CurrentMap.Get().Id));
            return await task.Task;
        }

        #endregion

        #region Spell cast

        public event Action<short, bool, bool> CastSpellUpdate;
        private void OnCastSpellUpdate(short cell, bool finish, bool success)
        {
            if (CastSpellUpdate != null) CastSpellUpdate(cell, finish, success);
        }

        public bool CanCastSpell(SpellStackItem spell, short target)
        {
            if (!spell.CanBeCasted(Player.InvockationRemaning))
                return false;
            double targetId;
            foreach (FighterDetails fighter in Brain.FightManager.Fighters.Get().Values)
                if (fighter.CellId == target)
                {
                    targetId = fighter.Fighter.ContextualId;
                    if (!spell.CanBeCasted(targetId, Player.InvockationRemaning))
                        return false;
                }
            //if (spell.Detail.CurrentLevel.MaxCastPerTurn)
            return true;
        }

        public async Task<bool> CastSpell(SpellStackItem spell, short target)
        {
            TaskCompletionSource<bool> task = new TaskCompletionSource<bool>();
            void SequenceDone(bool result)
            {
                if (result)
                {
                    bool casted = false;
                    foreach (FighterDetails fighter in Brain.FightManager.Fighters.Get().Values)
                        if (fighter.CellId == target)
                        {
                            double targetId = fighter.Fighter.ContextualId;
                            if (!spell.Casted(targetId, Player.InvockationRemaning))
                                Error("Internal error, trying to cast a spell that we konw we can't cast it");
                            casted = true;
                            break;
                        }
                    if (!casted)
                        spell.Casted(Player.InvockationRemaning);
                    OnCastSpellUpdate(target, true, true);
                }
                else
                {

                    spell.Failed = true;
                    OnCastSpellUpdate(target, true, false);
                }
                Brain.FightFrame.CastResult -= SequenceDone;
                task.SetResult(result);
            }
            await Task.Delay(DellayManager.GetInstance().Get(DellayType.FightSpellCasting));
            Log("Casting {0} on cell {1}", spell.Name, target);
            GameActionFightCastRequestMessage gafcrm = new GameActionFightCastRequestMessage();
            gafcrm.InitGameActionFightCastRequestMessage((short)spell.SpellId, target);
            OnCastSpellUpdate(target, false, true);
            lastSpell = spell;
            Brain.FightFrame.CastResult += SequenceDone;
            Brain.SendMessage(gafcrm);
            return await task.Task;
        }

        #endregion

        #endregion

        #region State

        public void Synchronize(GameFightSynchronizeMessage msg)
        {
            foreach (GameFightFighterInformations info in msg.Fighters)
            {
                if (Fighters.Get().ContainsKey(info.ContextualId))
                    Fighters.Get()[info.ContextualId].Fighter.Disposition = info.Disposition;
                if (EntitiesDispositons.Get().ContainsKey(info.ContextualId))
                    EntitiesDispositons.Get()[info.ContextualId] = info.Disposition.CellId;
            }
            EntitiesDispositons.OnChanged();
        }

        public void HandleMovement(GameMapMovementMessage msg)
        {
            try
            {
                EntitiesDispositons.Get()[msg.ActorId] = msg.KeyMovements[msg.KeyMovements.Length - 1];
                Fighters.Get()[msg.ActorId].Fighter.Disposition.CellId = msg.KeyMovements[msg.KeyMovements.Length - 1];
                EntitiesDispositons.OnChanged();
            }
            catch
            {
                Error("Can't handle movement for {0}", msg.ActorId);
            }
        }

        public void RemoveFighter(double id)
        {
            FighterDetails fight;
            if ((fight = Fighters.Get()[id]) != null)
            {
                if (fight.Fighter.Stats.Summoned && fight.Fighter.Stats.Summoner == Player.Fighter.ContextualId)
                    Player.InvockationRemaning++;
            }
            EntitiesDispositons.Get().Remove(id);
            Fighters.Get().Remove(id);
            Fighters.OnChanged();
            if (id == Player.Fighter.ContextualId)
            {
                State.Set(FightState.Die);
            }
        }

        public void HandleSummon(GameActionFightSummonMessage msg)
        {
            int nbr = 0;
            foreach (GameFightFighterInformations info in msg.Summons)
            {
                EntitiesDispositons.Get()[info.ContextualId] = info.Disposition.CellId;
                AddFighter(info);
                Log("Summon {0} added to entities !", info.ContextualId);
                nbr++;
            }
            if (msg.SourceId == Player.Fighter.ContextualId)
            {
                Log("Invock point delta {0}", -nbr);
                Player.InvockationRemaning -= nbr;
                if (lastSpell != null)
                    lastSpell.IsInvocke = true;
                else
                    Error("Something wrong, we make an invocke but lastSpell is null");
            }
        }

        public void AddFighter(GameFightFighterInformations fighter)
        {
            Fighters.Get()[fighter.ContextualId] = new FighterDetails(fighter);
            Log("Fighter {0} join fight", fighter.ContextualId);
            Fighters.OnChanged();
        }

        public void HandleSlide(GameActionFightSlideMessage slide)
        {
            if (!EntitiesDispositons.Get().ContainsKey(slide.TargetId) || !Fighters.Get().ContainsKey(slide.TargetId))
            {
                Error("Receive GameActionFightSlideMessage  but target actor is undefined !");
                return;
            }
            EntitiesDispositons.Get()[slide.TargetId] = slide.EndCellId;
            Fighters.Get()[slide.TargetId].CellId = slide.EndCellId;
            Fighters.OnChanged();
            Log("{0} slided from cell {1} to {2}", slide.TargetId, slide.StartCellId, slide.EndCellId);
        }

        public void HandleTeleport(GameActionFightTeleportOnSameMapMessage msg)
        {
            if (Fighters.Get().ContainsKey(msg.TargetId))
                Fighters.Get()[msg.TargetId].CellId = msg.CellId;
            EntitiesDispositons.Get()[msg.TargetId] = msg.CellId;
            Fighters.OnChanged();
        }

        public void UpdateFightersDisposition(IdentifiedEntityDispositionInformations[] dispositions)
        {
            foreach (IdentifiedEntityDispositionInformations dispo in dispositions)
            {
                EntitiesDispositons.Get()[dispo.Id_] = dispo.CellId;
            }
            EntitiesDispositons.OnChanged();
            if (State == FightManager.FightState.Placement)
                State.Set(FightState.PlacementDone);
            Log("Positions updated !");
        }


        #endregion

    }
}

