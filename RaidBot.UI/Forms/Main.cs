using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ComponentFactory.Krypton.Navigator;
using ComponentFactory.Krypton.Ribbon;
using ComponentFactory.Krypton.Toolkit;
using RaidBot.Engine.Bot;
using RaidBot.UI.Forms.Account;
using RaidBot.UI.Forms.Bot;

namespace RaidBot.UI.Forms
{
    public partial class RaidBot : KryptonForm
    {
        public RaidBot()
        {
            InitializeComponent();
        }


        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            LoadAccount load = new LoadAccount();
            load.LoadAccounts += (accounts) =>
            {
                GroupControl grp = new GroupControl(accounts);
                grp.Dock = DockStyle.Fill;
                KryptonPage page = new KryptonPage();
                page.Controls.Add(grp);
                page.AutoHiddenSlideSize = new System.Drawing.Size(200, 200);
                page.LastVisibleSet = true;
                page.MinimumSize = new System.Drawing.Size(50, 50);
                page.Text = load.Name;
                page.ToolTipTitle = load.Name;

                this.botTabs.Pages.Add(page);
            };
            load.ShowDialog();
        }

        private void KryptonGroupPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            new TrajetManager().Show();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            new Ide.Ide().Show();
        }

        private void RaidBot_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
                toolStripButton1_Click(null, null);
        }

        private void gereLesComptesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
