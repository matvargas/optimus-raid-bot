using RaidBot.Common.Default.Loging;
using RaidBot.Common.Network.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RaidBot.Engine.Bot
{
    public class Group
    {
        public event EventHandler<UserEventArgs> UserConnected;
        private void OnUserConnected(Brain brain) { if (UserConnected != null) UserConnected(this, new UserEventArgs(brain)); }
        public event EventHandler<UserEventArgs> UserDisconnected;
        private void OnUserDisconnected(Brain brain) { if (UserDisconnected != null) UserDisconnected(this, new UserEventArgs(brain)); }

        public Dictionary<String, BotConfig> Configs { get; set; }
        public Dictionary<String, Brain> Bots { get; set; }
        public Brain Leader { get; set; }
        public String Name { get; set; }

        public Group(String name, List<BotConfig> configs, String leader)
        {
            Name = name;
            Configs = new Dictionary<string, BotConfig>();
            Bots = new Dictionary<string, Brain>();
            foreach (BotConfig config in configs)
            {
                Configs.Add(config.Username, config);
                Brain n = new Brain(config, this);
                if (config.Username == leader)
                    Leader = n;
                Bots.Add(config.Username, n);
            }
        }

        public void LoiginAll()
        {
            foreach (String user in this.Bots.Keys)
                Login(user);
        }

        public Brain Login(String username)
        {
            Brain brain = this.Bots[username];
            brain.UpdateClient(new Client(IPAddress.Parse("34.252.21.81"), 5555, new Logger()));
            return brain;
        }


        public void Logout(String username)
        {
            Brain brain = this.Bots[username];
            brain.WillReconnect = false;
            brain.Connection.Stop();
        }

        public class UserEventArgs : EventArgs
        {
            public Brain Brain;

            public UserEventArgs(Brain brain)
            {
                Brain = brain;
            }
        }

    }
}
