using RaidBot.Common.Default.Loging;
using RaidBot.Engine.Dispatcher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidBot.Engine.Bot.Managers
{
    public abstract class Manager: MarshalByRefObject
    {
        public Brain Brain { get; }

        public void PostInit() { }

        public Manager(Brain brain)
        {
            Brain = brain;
        }

        private void SendLog(string text, LogLevelEnum level, params object[] parameters)
        {
            Brain.Logger.Log(String.Format("[{0}]{1}", this.GetType().Name, String.Format(text, parameters)), level);
        }

        public void Error(string text, params object[] parameters)
        {
            SendLog(text, LogLevelEnum.Error, parameters);
        }

        public void Warn(string text, params object[] parameters)
        {
            SendLog(text, LogLevelEnum.Warning, parameters);
        }

        public void Log(string text, params object[] parameters)
        {
            SendLog(text, LogLevelEnum.Info, parameters);
        }
    }
}
