using ComponentFactory.Krypton.Toolkit;
using RaidBot.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RaidBot.UI.Forms.Accounts
{
    public partial class AddAccount : KryptonForm
    {
        public AddAccount()
        {
            InitializeComponent();
        }


        private void BtnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void Clear()
        {
            TxtPassword.Text = "";
            TxtUsername.Text = "";
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            if (TxtUsername.Text != "" && TxtPassword.Text != "")
                AccountManager.GetInstance().AddAccount(TxtUsername.Text, TxtPassword.Text);
            this.Close();
        }

        private void TxtPassword_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                kryptonButton1_Click(null, null);
            }
        }
    }
}
