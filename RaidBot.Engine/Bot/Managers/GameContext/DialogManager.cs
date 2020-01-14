using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RaidBot.Protocol.Messages;
namespace RaidBot.Engine.Bot.Managers.GameContext
{
    public class DialogManager : Manager
    {
        public event Action<NpcDialogQuestionMessage> NpcDialogQuestion;
        public void OnNpcDialogQuestion(NpcDialogQuestionMessage msg)
        {
            if (NpcDialogQuestion != null) NpcDialogQuestion(msg);
        }
        public DialogManager(Brain brain) : base(brain)
        {
        }
    }
}
