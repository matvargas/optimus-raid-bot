using ComponentFactory.Krypton.Toolkit;
using RaidBot.Engine.Bot;
using RaidBot.Engine.Bot.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RaidBot.UI.Forms.Bot
{
    public partial class ServerSelectionForm : KryptonForm
    {
        public ServerSelectionForm()
        {
            InitializeComponent();
        }
        private Dictionary<String, int> servers;

        Brain Brain;

        public ServerSelectionForm(Brain bot)
        {
            InitializeComponent();
            Brain = bot;
            this.Text = String.Format("Select a server ({0})", bot.Config.Username);
            Brain.ServerManager.ServerSelectionResult += ServerManager_ServerSelectionResult;
        }

        private void ServerManager_ServerSelectionResult(bool obj)
        {
            this.BeginInvoke(new Action(() =>
            {
                if (obj)
                    this.Close();
                else
                    label1.Text = "Unable to select server";
            }));
        }

        private void ServerSelectionForm_Load(object sender, EventArgs e)
        {
            lstServer.Items.Clear();
            servers = new Dictionary<string, int>();
            foreach (GameServerDetail detail in Brain.State.AvaiableServers.Get())
            {
                servers[detail.Name] = detail.Informations.Id_;
                if (detail.Informations.IsSelectable && !detail.Informations.IsMonoAccount)
                    lstServer.Items.Add(String.Format("{0}", detail.Name, detail.Informations.CharactersCount));
            }
        }

        private void ServerSelectionForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Brain.ServerManager.ServerSelectionResult -= ServerManager_ServerSelectionResult;
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            if (lstServer.SelectedItem == null)
            {
                label1.Text = "Pleas select a server";
                return;
            }
            Brain.ServerManager.RequestServerSelection(servers[(String)lstServer.SelectedItem]);

        }
    }
}
