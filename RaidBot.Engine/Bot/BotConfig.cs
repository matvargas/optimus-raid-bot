using RaidBot.Common.Network.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidBot.Engine.Bot
{

    [Serializable]
    public class BotConfig
    {
        public String Username { get; set; }
        public String Password { get; set; }
        public ProxySetting Proxy { get; set; }

        [NonSerialized]
        private bool mUsed;
        public bool Used { get { return mUsed; } set { mUsed = value; } }
        
        public BotConfig(String username, string password)
        {
            Proxy = new ProxySetting("94.23.220.7", 35156, "zarandifar", "rjx3jd85");
            Username = username;
            Password = password;
            mUsed = false;
        }
    }


}
