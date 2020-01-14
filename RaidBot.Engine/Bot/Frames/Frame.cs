using RaidBot.Common.Default.Loging;
using RaidBot.Engine.Dispatcher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidBot.Engine.Bot.Frames
{
    public abstract class Frame: MessagesHandler
    {
        public Brain Brain { get; }

        public Frame(Brain brain)
        {
            Brain = brain;
            brain.Dispatcher.Register(this);
        }

        private void SendLog(string text, LogLevelEnum level, params object[] parameters)
        {
            Brain.Logger.Log(String.Format("[{0}]{1}", this.GetType().Name, String.Format(text, parameters)), level);
        }

        public void Error(string text, params object[] parameters)
        {
            SendLog(text, LogLevelEnum.Error, parameters);
        }


        public void Log(string text, params object[] parameters)
        {
            SendLog(text, LogLevelEnum.Info, parameters);
        }

        public void Warn(string text, params object[] parameters)
        {
            SendLog(text, LogLevelEnum.Warning, parameters);
        }

        ~Frame()
        {
            this.Brain.Dispatcher.UnRegister(this);
        }
    }
}
