using ComponentFactory.Krypton.Navigator;
using EasyTabs;
using RaidBot.Engine.Bot;
using RaidBot.Engine.Bot.Managers;
using RaidBot.Engine.Bot.Managers.Extension;
using RaidBot.UI.Forms;
using RaidBot.UI.Forms.Bot;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static RaidBot.UI.AccountManager;

namespace RaidBot.UI
{
    public partial class GroupControl : UserControl
    {
        public List<BotConfig> Accounts { get; set; }
        public Group Group { get; set; }
        protected TitleBarTabs ParentTabs { get { return (ParentForm as TitleBarTabs); } }

        public GroupControl(List<BotConfig> accounts)
        {
            InitializeComponent();
            Accounts = accounts;
        }

        public GroupControl()
        {
            InitializeComponent();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (Group.Leader.ExtManager.Trajet.Get() != null)
            {
                Group.Leader.ExtManager.Trajet.Set(null);
                btnSet.Image = Properties.Resources.cgreen;
                cmbbChef.Enabled = true;
                cmbbTrajets.Enabled = true;
            }
            else if (cmbbTrajets.SelectedItem != null)
            {
                Group.Leader.ExtManager.RegisterExtension(new BasicExtensionHost(ScriptsManager.Load((string)cmbbTrajets.SelectedItem), Group.Leader));
                cmbbTrajets.Enabled = false;
                cmbbChef.Enabled = false;
                btnSet.Image = Properties.Resources.cred;
            }
        }

        private void GroupeWindow_Load(object sender, EventArgs e)
        {
            Group = new Group(Text, Accounts, Accounts[0].Username);
            tabBot.Pages.Clear();
            foreach (Brain bot in Group.Bots.Values)
            {
                KryptonPage page = new KryptonPage();
                page.Name = bot.Config.Username + "Tab";
                page.Text = bot.Config.Username;
                var control = new BotControl(bot);
                page.Disposed += (s, ele) =>
                {
                    control.Dispose();
                    if (AccountManager.GetInstance().Accounts.ContainsKey(bot.Config.Username))
                        AccountManager.GetInstance().Accounts[bot.Config.Username].Used = false;
                };
                control.Dock = DockStyle.Fill;
                page.Controls.Add(control);
                tabBot.Pages.Add(page);
            }

            ScriptsManager.SafeWith((manager) =>
            {
                manager.TrajetsUpdated += Manager_TrajetsUpdated;
                Manager_TrajetsUpdated(manager.Trajets);
            });
            MembersUpdated();
        }

        private void MembersUpdated()
        {
            this.BeginInvoke(new Action(() =>
            {
                cmbbChef.Items.Clear();
                foreach (String cfg in Group.Configs.Keys)
                {
                    cmbbChef.Items.Add(cfg);
                }
                cmbbChef.SelectedItem = Group.Leader.Config.Username;
            }));
        }

        private void Manager_TrajetsUpdated(Dictionary<string, string> obj)
        {
            this.BeginInvoke(new Action(() =>
            {
                object oldSelected = cmbbTrajets.SelectedItem;
                cmbbTrajets.Items.Clear();
                foreach (string k in obj.Keys)
                    cmbbTrajets.Items.Add(k);
                cmbbTrajets.SelectedItem = oldSelected;
            }));
        }

        private void connectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Group.LoiginAll();
        }

    }
}
