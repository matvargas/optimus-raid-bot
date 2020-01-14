using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RaidBot.UI.Forms.Account
{
    public partial class AccountCreator : Form
    {
        public AccountCreator()
        {
            InitializeComponent();
        }

        private void AccountCreator_Load(object sender, EventArgs e)
        {
            this.web.Navigate(new Uri("https://account.ankama.com/fr/creer-un-compte"));
        }

        private void LoadAccountPage()
        {
        }
    }
}
