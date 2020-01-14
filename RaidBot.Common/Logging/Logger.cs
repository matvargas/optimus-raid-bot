using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace RaidBot.Common.Default.Loging
{
    public delegate void OnLogDelegate(string log,LogLevelEnum logLevel);
    public delegate void OnLogObjectDelegate(object log,LogLevelEnum logLevel);

    public class Logger

    {
        static Logger()
        {
            Default = new Logger();
        }
        public static Logger Default { get; private set; }

        #region Membres

        public event OnLogDelegate OnLog;
        private void OnOnLog(string log, LogLevelEnum logLevel)
        {
            if (OnLog != null)
                OnLog(String.Format("[{0:00}:{1:00}:{2:00}]{3}", DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second, log), logLevel);
        }

        public void Log(string info, LogLevelEnum LogLevel = LogLevelEnum.Info)
        {
            OnOnLog(info, LogLevel);
        }


        public event OnLogObjectDelegate OnLogObj;
        private void OnOnLogObj(object log, LogLevelEnum logLevel)
        {
            if (OnLogObj != null)
                OnLogObj(log, logLevel);
        }

        public void LogObj(object info, LogLevelEnum LogLevel = LogLevelEnum.Info)
        {
            OnOnLogObj(info, LogLevel);
        }


        #endregion
    }
}
