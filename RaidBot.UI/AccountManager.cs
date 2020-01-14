using RaidBot.Engine.Bot;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace RaidBot.UI
{
    public class AccountManager
    {
        static AccountManager instance = new AccountManager();
        public static AccountManager GetInstance() { return instance; }

        public event EventHandler<EventArgs> Refresh;
        private void OnRefresh()
        {
            if (Refresh !=null)
                Refresh(this, new EventArgs());
        }

        const string ACCOUNT_FILE = "./userdata";
        public Dictionary<String, BotConfig> Accounts { get; set; }

        private AccountManager()
        {
            LoadList();
        }

        public void LoadList()
        {
            try
            {
                FileStream stream = File.Open(ACCOUNT_FILE, FileMode.Open);
                var formatter = new BinaryFormatter();
                Accounts = (Dictionary<String, BotConfig>)formatter.Deserialize(stream);
            }
            catch
            {
                Accounts = new Dictionary<string, BotConfig>();
                SaveList();
            }
            OnRefresh();
        }

        public void AddAccount(String username, String password)
        {
            this.Accounts[username] = new BotConfig(username, password);
            SaveList();
            OnRefresh();
        }

        public void RemoveAccount(String username)
        {
            this.Accounts.Remove(username);
            this.SaveList();
            OnRefresh();
        }

        public void SaveList()
        {
            try
            {
                FileStream stream = File.Create(ACCOUNT_FILE);
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, Accounts);
                stream.Close();
            } catch
            {
                //TODO say something to the user
            }
        }
    }
}
