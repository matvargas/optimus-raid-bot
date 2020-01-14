using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidBot.UI
{
    public class LogState
    {
        private LogState instance;

        public LogState GetInstance()
        {
            if (instance == null)
                instance = new LogState();
            return instance;
        }

        public LogState()
        {

        }
    }
}
