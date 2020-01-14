using RaidBot.Engine.Dispatcher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RaidBot.Protocol.Messages;
using RaidBot.Engine.Bot.Managers;

namespace RaidBot.Engine.Bot.Frames
{
    public class PartyFrame : Frame
    {
        public PartyFrame(Brain brain) : base(brain)
        {
        }

        [MessageHandlerAttribut(typeof(PartyInvitationMessage))]
        private async void HandlePartyInvitationMessage(PartyInvitationMessage msg)
        {
            foreach (Brain other in Brain.Group.Bots.Values)
            {
                if (other.State.Player.BaseInformation.Get() != null && other.State.Player.BaseInformation.Get().Id_ == msg.FromId)
                {
                    Log("Receive invitiation request from group member, send ok after dellay ...");
                    await Task.Delay(DellayManager.GetInstance().Get(DellayManager.DellayType.PartyInvitAccept));
                    Brain.SendMessage(new PartyAcceptInvitationMessage().InitAbstractPartyMessage(msg.PartyId));
                    Brain.PartyManager.IsInGroup = true;
                    return;
                }
            }
            Error("Receive invitation request from {0}, send no after dellay ...", msg.FromName);
            await Task.Delay(DellayManager.GetInstance().Get(DellayManager.DellayType.PartyInvitAccept));
            Brain.SendMessage(new PartyRefuseInvitationMessage().InitAbstractPartyMessage(msg.PartyId));
        }

        [MessageHandlerAttribut(typeof(PartyMemberRemoveMessage))]
        private async void PartyMemberRemoveMessage(PartyMemberRemoveMessage msg)
        {
            Error("{0} leav group !", msg.LeavingPlayerId);
        }
    }
}
