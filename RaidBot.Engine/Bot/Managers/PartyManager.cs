using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoonSharp.Interpreter;
using RaidBot.Engine.Bot.Managers.Extension;
using RaidBot.Protocol.Enums;
using RaidBot.Protocol.Messages;

namespace RaidBot.Engine.Bot.Managers
{
    /// <summary>
    /// PartyManager is just an utility for the ExtensionManager.
    /// For example it's only role is to provide some informations like all member of the group is on the same map
    /// or who is lost, it's also help to recover lost group member (by loading the lost trajet from group leader)
    /// </summary>
    public class PartyManager : Manager
    {

        #region Properties

        public bool IsFollower
        {
            get
            {
                return (Brain.Group.Leader != Brain);
            }
        }

        public bool IsReady
        {
            get
            {
                return Brain.State.CurrentMap.Get() != null && Brain.Group.Leader.State.CurrentMap != null && Brain.Group.Leader.State.CurrentMap.Get().Id == Brain.Group.Leader.State.CurrentMap.Get().Id;
            }
        }

        public bool AllFollowerCanFollow
        {
            get
            {
                foreach (Brain b in Brain.Group.Bots.Values)
                {
                    if (!b.PartyManager.CanFollow)
                        return false;
                }
                return true;
            }
        }

        public bool CanFollow
        {
            get
            {
                if (Brain.CurrentState.Get() != Brain.BrainState.Fight && Brain.State.CurrentMap.Get() != null && Brain.Group.Leader.State.CurrentMap.Get() != null && Brain.State.CurrentMap.Get().Id == Brain.Group.Leader.State.CurrentMap.Get().Id)
                    return true;
                return false;
            }
        }

        public bool IsLost { get; private set; }
        public bool CheckPoint { get; set; }

        public bool AllFollowerCanBeReady
        {
            get
            {
                foreach (Brain bot in Brain.Group.Bots.Values)
                {
                    if (bot.State.SelectedServer.Get() == null || Brain.State.SelectedServer.Get() == null)
                        return false;
                    if (bot.State.SelectedServer.Get().ServerId != Brain.State.SelectedServer.Get().ServerId)
                    {
                        Error("All followers are not in the same server !");
                        return false;
                    }
                }
                return true;
            }
        }

        public bool AllFollowerReady
        {
            get
            {
                foreach (Brain bot in Brain.Group.Bots.Values)
                {
                    if (!bot.PartyManager.IsReady || !bot.PartyManager.CanFollow)
                        return false;
                }
                return true;
            }
        }

        public bool IsInGroup { get; set; }

        #endregion

        #region Ctor

        public PartyManager(Brain brain) : base(brain)
        {
            brain.State.Player.RolePlayInformations.changed += RolePlayInformations_changed;
            brain.CurrentState.changed += CurrentState_changed; ;
        }

        #endregion

        #region Group management

        public void YourLost()
        {
            IsLost = true;
            CheckPoint = false;
            if (IsFollower)
            {
                Warn("We're lost, trying to join group leader");
                Brain.ExtManager.Trajet.Set(Brain.Group.Leader.ExtManager.Trajet);
            }
        }

        public void Recovered()
        {
            IsLost = false;
            CheckPoint = false;
            Log("Recovered !");
            if (IsFollower)
                Brain.ExtManager.Trajet.Set(new Script());
        }

        #region Roleplay group

        Queue<Brain> InvitationRequest = new Queue<Brain>();
        /// <summary>
        /// Invit @self in my group
        /// </summary>
        /// <param name="self"></param>
        public async Task AskForPartyInvitation(Brain self)
        {
            if (!AllFollowerCanBeReady)
            {
                InvitationRequest.Enqueue(self);
                return;
            }
            if (Brain.CurrentState != Brain.BrainState.Disconnected && Brain.State.Player.RolePlayInformations.Get() != null)
            {
                Log("Sending party invitation to {0} after dellay ...", self.State.Player.RolePlayInformations.Get().Name);
                await Task.Delay(DellayManager.GetInstance().Get(DellayManager.DellayType.PartyInvitAccept));
                Brain.SendMessage(new PartyInvitationRequestMessage().InitPartyInvitationRequestMessage(self.State.Player.RolePlayInformations.Get().Name));
            }
            else
            {
                InvitationRequest.Enqueue(self);
                Warn("Not ready, invitation request pushed to the queue");
            }
        }

        /// <summary>
        /// Ask for party invitiation to leader
        /// </summary>
        /// <param name="data"></param>
        private async void RolePlayInformations_changed(Protocol.Types.GameRolePlayCharacterInformations data)
        {
            if (!IsInGroup && IsFollower)
            {
                await Task.Delay(DellayManager.GetInstance().Get(DellayManager.DellayType.PartyLoadDellay));
                await Brain.Group.Leader.PartyManager.AskForPartyInvitation(Brain);
            }
            else if (InvitationRequest.Count > 0)
            {
                while (InvitationRequest.Count > 0)
                {
                    await AskForPartyInvitation(InvitationRequest.Dequeue());
                }
            }
        }

        #endregion

        #endregion

        #region Event propagation

        private void CurrentState_changed(Brain.BrainState data)
        {
            if (data == Brain.BrainState.Disconnected)
                IsInGroup = false;
        }

        public async Task<bool> RecvChangeMap(DirectionsEnum direction)
        {
            if (!IsReady)
            {
                Error("Recv change map but not ready !");
                return false;
            }
            await Brain.PlayerManager.ChangeMap(direction);
            return true;
        }

        public async Task<int> PropageChangeMap(DirectionsEnum direction)
        {
            if (IsLost)
                return 0;
            if (IsFollower || !AllFollowerReady)
                return -1;
            int ret = 0;
            foreach (Brain bot in Brain.Group.Bots.Values)
            {
                if (bot.PartyManager.IsFollower)
                    if (!await bot.PartyManager.RecvChangeMap(direction))
                        ret++;
            }
            return ret;
        }

        #endregion

    }
}
