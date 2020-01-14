using RaidBot.Engine.Dispatcher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RaidBot.Protocol.Messages;
using RaidBot.Protocol.enums;
using static RaidBot.Engine.Bot.Managers.FightContext.FightManager;

namespace RaidBot.Engine.Bot.Frames.FightContext
{
    public class FightFrame : Frame
    {
        public FightFrame(Brain brain) : base(brain)
        {
            Sequences = new List<SequenceStartMessage>();
            ToSendAfterSequence = new Queue<NetworkMessage>();
        }

        #region Loading

        bool Reconnecting = false;
        [MessageHandlerAttribut(typeof(GameFightStartingMessage))]
        private void HandleGameFightStartingMessage(GameFightStartingMessage msg)
        {
            Reconnecting = (Brain.FightManager.State.Get() == FightState.Reconnecting);
            Brain.FightManager.State.Set(FightState.Loading);
            Log("Fight Loading ...");
        }

        [MessageHandlerAttribut(typeof(BasicNoOperationMessage))]
        private void HandleBasicNoOperationMessage(BasicNoOperationMessage msg)
        {
            if (Brain.FightManager.State.Get() != FightState.Loading)
                return;
            Sequences.Clear();
            if (Reconnecting)
            {
                Brain.FightManager.State.Set(FightState.Fighting);
                Log("Fight resumed !");
            }
            else
            {
                Brain.FightManager.State.Set(FightState.Placement);
                Log("Fight placement preparation phase ...");
            }
        }

        [MessageHandlerAttribut(typeof(GameFightPlacementPossiblePositionsMessage))]
        private void HandleGameFightPlacementPossiblePositionsMessage(GameFightPlacementPossiblePositionsMessage msg)
        {
            if (msg.TeamNumber == 0)
                Brain.FightManager.PlacementForDefender = msg.PositionsForChallengers;
            else
                Brain.FightManager.PlacementForDefender = msg.PositionsForDefenders;
        }

        [MessageHandlerAttribut(typeof(CharacterSelectedForceMessage))]
        private void HandleCharacterSelectedForceMessage(CharacterSelectedForceMessage msg)
        {
            Brain.FightManager.State.Set(FightState.Reconnecting);
        }

        [MessageHandlerAttribut(typeof(GameFightEndMessage))]
        private void HandleGameFightEndMessage(GameFightEndMessage msg)
        {
            Brain.FightManager.State.Set(FightState.None);
            Brain.CurrentState.Set(Brain.BrainState.Idle);
            Console.WriteLine("Fight finished !");
        }

        #endregion

        #region Entities

        [MessageHandlerAttribut(typeof(GameActionFightSummonMessage))]
        private void HandleGameActionFightSummonMessage(GameActionFightSummonMessage msg)
        {
            Brain.FightManager.HandleSummon(msg);
        }

        [MessageHandlerAttribut(typeof(GameActionFightSlideMessage))]
        private void HandleGameActionFightSlideMessage(GameActionFightSlideMessage msg)
        {
            Brain.FightManager.HandleSlide(msg);
        }

        [MessageHandlerAttribut(typeof(GameActionFightTeleportOnSameMapMessage))]
        private void HandleGameActionFightTeleportOnSameMapMessage(GameActionFightTeleportOnSameMapMessage msg)
        {
            Brain.FightManager.HandleTeleport(msg);
        }

        [MessageHandlerAttribut(typeof(GameFightShowFighterMessage))]
        private void HandleGameFightShowFighterMessage(GameFightShowFighterMessage msg)
        {
            Brain.FightManager.AddFighter(msg.Informations);
        }

        [MessageHandlerAttribut(typeof(GameEntitiesDispositionMessage))]
        private void HandleGameEntitiesDispositionMessage(GameEntitiesDispositionMessage msg)
        {
            Brain.FightManager.UpdateFightersDisposition(msg.Dispositions);
        }

        [MessageHandlerAttribut(typeof(GameFightSynchronizeMessage))]
        private void HandleGameFightSynchronizeMessage(GameFightSynchronizeMessage msg)
        {
            Brain.FightManager.Synchronize(msg);
        }

        #endregion

        #region Infos

        [MessageHandlerAttribut(typeof(GameActionFightPointsVariationMessage))]
        private void HandleGameActionFightPointsVariationMessage(GameActionFightPointsVariationMessage msg)
        {
            if (Brain.FightManager.Player == null || msg.TargetId != Brain.FightManager.Player.Fighter.ContextualId)
                return;
            switch (msg.ActionId)
            {
                case ActionIdENum.ACTION_CHARACTER_MOVEMENT_POINTS_LOST:
                case ActionIdENum.ACTION_CHARACTER_MOVEMENT_POINTS_USE:
                    Brain.FightManager.Player.Fighter.Stats.MovementPoints += msg.Delta;
                    break;
                case ActionIdENum.ACTION_CHARACTER_ACTION_POINTS_LOST:
                case ActionIdENum.ACTION_CHARACTER_ACTION_POINTS_USE:
                    Brain.FightManager.Player.Fighter.Stats.ActionPoints += msg.Delta;
                    break;

            }
        }

        [MessageHandlerAttribut(typeof(FighterStatsListMessage))]
        private void HandleFighterStatsListMessage(FighterStatsListMessage msg)
        {
            Brain.FightManager.FighterStats = msg.Stats;
        }

        #endregion

        #region Turn

        [MessageHandlerAttribut(typeof(GameFightTurnStartMessage))]
        private void HandleGameFightTurnStartMessage(GameFightTurnStartMessage msg)
        {
            Sequences.Clear();
            Log("Turn start of {0}", msg.Id_);
            //if (Brain.State.Player.BaseInformation.Get() != null && msg.Id_ != Brain.State.Player.BaseInformation.Get().Id_)
           //     Brain.FightManager.Turn.Set(TurnState.Other);
        }

        [MessageHandlerAttribut(typeof(GameFightTurnStartPlayingMessage))]
        private void HandleGameFightTurnStartPlayingMessage(GameFightTurnStartPlayingMessage msg)
        {
            Log("It's our turn !");
            Brain.FightManager.Turn.Set(TurnState.Self);
        }

        [MessageHandlerAttribut(typeof(GameFightTurnResumeMessage))]
        private void HandleGameFightTurnResumeMessage(GameFightTurnResumeMessage msg)
        {
            Log("Resuming turn ...");
            Brain.FightManager.Turn.Set(TurnState.Self);
        }

        [MessageHandlerAttribut(typeof(GameFightTurnReadyRequestMessage))]
        private void HandleGameFightTurnReadyRequestMessage(GameFightTurnReadyRequestMessage msg)
        {
            if (Sequences.Count > 0)
            {
                Log("Waiting for sequence to end before sending ack");
                ToSendAfterSequence.Enqueue(new GameFightTurnReadyMessage().InitGameFightTurnReadyMessage(true));
            }
            else
            {
                Brain.SendMessage(new GameFightTurnReadyMessage().InitGameFightTurnReadyMessage(true));
            }
        }

        [MessageHandlerAttribut(typeof(GameFightNewRoundMessage))]
        private void HandleGameFightNewRoundMessage(GameFightNewRoundMessage round)
        {
            Log("Round number {0}", round.RoundNumber);
        }

        [MessageHandlerAttribut(typeof(GameActionFightDeathMessage))]
        private void HandleGameActionFightDeathMessage(GameActionFightDeathMessage msg)
        {
            if (msg.TargetId == Brain.FightManager.Player.Fighter.ContextualId)
            {
                Log("###@@@@@ We're die");
                Brain.FightManager.State.Set(FightState.Die);
            } else
            {
                Brain.FightManager.RemoveFighter(msg.TargetId);
            }
        }

        #endregion

        #region Cast

        public event Action<bool> CastResult;
        private void OnCastResult(bool result)
        {
            if (CastResult != null)
                CastResult(result);
        }

        [MessageHandlerAttribut(typeof(GameActionFightNoSpellCastMessage))]
        private void HandleGameActionFightNoSpellCastMessage(GameActionFightNoSpellCastMessage msg)
        {
            Error("Spell cast failed ...");
            OnCastResult(false);
        }

        [MessageHandlerAttribut(typeof(GameActionFightSpellCastMessage))]
        private void HandleGameActionFightSpellCastMessage(GameActionFightSpellCastMessage msg)
        {
            if (msg.SourceId == Brain.FightManager.Player.Fighter.ContextualId)
            {
                Log("Spell cast success !");
                OnCastResult(true);
            }
        }

        #endregion

        #region Sequence

        public List<SequenceStartMessage> Sequences;
        public Queue<NetworkMessage> ToSendAfterSequence;
        public event Action SequenceDone;
        private void OnSequenceDone()
        {
            if (SequenceDone != null)
                SequenceDone();
        }

        [MessageHandlerAttribut(typeof(SequenceStartMessage))]
        private void HandleSequenceStartMessage(SequenceStartMessage msg)
        {
            Sequences.Add(msg);
            //Log("Begin sequence by {0} type {1}", msg.AuthorId, msg.SequenceType);
        }

        public int AckResponsDellay { get; set; }

        [MessageHandlerAttribut(typeof(SequenceEndMessage))]
        private async void HandleSequenceEndMessage(SequenceEndMessage msg)
        {
            foreach (SequenceStartMessage seq in Sequences)
            {
                if (msg.SequenceType == seq.SequenceType && msg.AuthorId == seq.AuthorId)
                {
                    if (seq.AuthorId == Brain.State.Player.BaseInformation.Get().Id_)
                    {
                        if (AckResponsDellay > 0)
                        {
                            await Task.Delay(AckResponsDellay);
                            AckResponsDellay = 0;
                        }
                        //Log("Send acknowlegment ...");
                        Brain.SendMessage(new GameActionAcknowledgementMessage().InitGameActionAcknowledgementMessage(true, (byte)msg.ActionId));
                    }
                    Sequences.Remove(seq);
                    //Log("Sequence ended by {0} of type {1}", msg.AuthorId, msg.SequenceType);
                    break;
                }
            }
            if (ToSendAfterSequence.Count > 0)
            {
                while (true)
                {
                    NetworkMessage m = ToSendAfterSequence.Dequeue();
                    //Log("Sending pending message ({0})", m.GetType().Name);
                    if (ToSendAfterSequence.Count == 0)
                        break;
                    Brain.SendMessage(m);
                }
            }
            OnSequenceDone();
        }

        #endregion
    }
}
