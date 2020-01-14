using RaidBot.Engine.Dispatcher;
using RaidBot.Engine.Utility.Pathfinding;
using RaidBot.Protocol.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RaidBot.Engine.Bot.Managers.FightContext.FightManager;

namespace RaidBot.Engine.Bot.Frames.GameContext
{
    public enum MovementResultEnum
    {
        Error,
        Success,
        ForceWalk,
    }

    public class MovementFrame : Frame
    {
        public event Action<MovementResultEnum> MovementResult;
        private void OnMovementResult(MovementResultEnum result)
        {
            if (MovementResult != null)
                MovementResult(result);
        }

        public MovementFrame(Brain brain) : base(brain)
        {
        }

        [MessageHandlerAttribut(typeof(GameMapNoMovementMessage))]
        private void HandleGameMapNoMovementMessage(GameMapNoMovementMessage msg)
        {
            Error("Can't performe movement (NoMovementMessage)!");
            if (Brain.FightManager.State != FightState.None)
            {
                Brain.FightManager.Player.CellId = (short)PathingUtils.CoordToCellId(msg.CellX, msg.CellY);
                Brain.FightManager.EntitiesDispositons.Get()[Brain.FightManager.Player.Fighter.ContextualId] = (short)PathingUtils.CoordToCellId(msg.CellX, msg.CellY);
                Brain.FightManager.Fighters.OnChanged();
            }
            else
            {
                Brain.State.Player.RolePlayInformations.Get().Disposition.CellId = PathingUtils.CoordToCellId(msg.CellX, msg.CellY);
                Brain.State.Player.RolePlayInformations.OnChanged();
            }
            OnMovementResult(MovementResultEnum.Error);
        }

        [MessageHandlerAttribut(typeof(GameCautiousMapMovementMessage))]
        private void HandleGameCautiousMapMovementMessage(GameCautiousMapMovementMessage msg)
        {
            if (msg.ActorId == Brain.State.Player.BaseInformation.Get().Id_)
            {
                Warn("Movement force walk");
                Brain.State.Player.RolePlayInformations.Get().Disposition.CellId = msg.KeyMovements[msg.KeyMovements.Length - 1];
                Brain.State.Player.RolePlayInformations.OnChanged();
                OnMovementResult(MovementResultEnum.ForceWalk);
            }
        }

        [MessageHandlerAttribut(typeof(GameMapMovementMessage))]
        private void HandleGameMapMovementMessage(GameMapMovementMessage msg)
        {
            if (msg.ActorId == Brain.State.Player.BaseInformation.Get().Id_ && Brain.CurrentState.Get() != Brain.BrainState.Fight)
            {
                Brain.State.Player.RolePlayInformations.Get().Disposition.CellId = msg.KeyMovements[msg.KeyMovements.Length - 1];
                Brain.State.Player.RolePlayInformations.OnChanged();
                OnMovementResult(MovementResultEnum.Success);
            }
            else if (Brain.CurrentState.Get() == Brain.BrainState.Fight)
            {
                if (msg.ActorId == Brain.FightManager.Player.Fighter.ContextualId)
                    OnMovementResult(MovementResultEnum.Success);
                Brain.FightManager.HandleMovement(msg);
            }
            else
                Brain.ElementsManager.HandleMovement(msg);
        }
    }
}
