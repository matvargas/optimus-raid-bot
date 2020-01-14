using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidBot.Common.Network.Client
{
    [Serializable]
    public class ProxySetting
    {
        public string Addr { get; set; }
        public int Port { get; set; }
        public bool Auth { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public ProxySetting()
        {

        }

        public ProxySetting(string addr, int port)
        {
            this.Port = port;
            this.Addr = addr;
            this.Auth = false;
        }

        public ProxySetting(string addr, int port, string email, string password)
        {
            this.Port = port;
            this.Addr = addr;
            this.Auth = true;
            this.Email = email;
            this.Password = password;
        }
    }
}
