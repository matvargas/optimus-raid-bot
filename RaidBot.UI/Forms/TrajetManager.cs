using ComponentFactory.Krypton.Toolkit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RaidBot.UI.Forms
{
    public partial class TrajetManager : KryptonForm
    {


        public TrajetManager()
        {
            InitializeComponent();
        }

        private void ajouterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            if (!Directory.Exists("trajets"))
                Directory.CreateDirectory("trajets");
            foreach (string name in openFileDialog1.FileNames)
            {
                File.Copy(name, Path.Combine("trajets", Path.GetFileName(name)));
            }
        }

        private void TrajetManager_Load(object sender, EventArgs e)
        {
            ScriptsManager.SafeWith((m) =>
            {
                m.TrajetsUpdated += M_TrajetsUpdated;
                M_TrajetsUpdated(m.Trajets);
            });
        }

        private void M_TrajetsUpdated(Dictionary<string, string> obj)
        {
            this.BeginInvoke(new Action(() =>
            {
                lstTrajet.Items.Clear();
                foreach (string name in obj.Keys)
                {
                    lstTrajet.Items.Add(name);
                }
            }));
        }

        private void chargerDansLideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Ide.Ide((string)lstTrajet.SelectedItem).Show();
            this.Close();
        }

        private void TrajetManager_FormClosed(object sender, FormClosedEventArgs e)
        {
            ScriptsManager.SafeWith((m) =>
            {
                m.TrajetsUpdated -= M_TrajetsUpdated;
            });
        }
    }
}
