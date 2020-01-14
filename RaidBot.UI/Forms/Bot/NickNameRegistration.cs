using ComponentFactory.Krypton.Toolkit;
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

    public partial class NickNameRegistration : KryptonForm
    {
        public string AccountName { get; set; }
        public event Action<NickNameRegistration, String> NickNameSelected;

        public NickNameRegistration()
        {
            InitializeComponent();
        }

        private void NickNameRegistration_Load(object sender, EventArgs e)
        {
            this.Text = "Select a nick name for " + AccountName;
        }

        public void Error()
        {
            this.BeginInvoke(new Action(() =>
            {
                this.labelFailReason.Text = "Already used !";
            }));
        }

        public void Done()
        {
            this.BeginInvoke(new Action(() =>
            {
                try
                {
                    this.Close();
                }
                catch
                {

                }
            }));
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
                return;
            NickNameSelected(this, textBox1.Text);
        }
    }
}
