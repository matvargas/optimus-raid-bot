using RaidBot.Engine.Dispatcher;
using RaidBot.Protocol.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RaidBot.Engine.Bot.Brain;

namespace RaidBot.Engine.Bot.Frames.GameContext
{
    public class DialogFrame : Frame
    {
        public DialogFrame(Brain brain) : base(brain)
        {
        }

        [MessageHandlerAttribut(typeof(NpcDialogCreationMessage))]
        private void HandleNpcDialogCreationMessage(NpcDialogCreationMessage msg)
        {
            Brain.CurrentState.Set(BrainState.Dialog);
        }

        [MessageHandlerAttribut(typeof(LeaveDialogMessage))]
        private void HandleLeaveDialogMessage(LeaveDialogMessage msg)
        {
            Brain.CurrentState.Set(BrainState.Idle);
        }

        [MessageHandlerAttribut(typeof(NpcDialogQuestionMessage))]
        private void HandleNpcDialogQuestionMessage(NpcDialogQuestionMessage msg)
        {
            Brain.DialogManager.OnNpcDialogQuestion(msg);
        }
    }
}
