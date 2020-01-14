using ComponentFactory.Krypton.Toolkit;
using RaidBot.Common.IO;
using RaidBot.Engine.Bot;
using RaidBot.UI.Forms.Accounts;
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
    public partial class LoadAccount : KryptonForm
    {
        private static LoadAccount instance;

        public static LoadAccount GetInstance()
        {
            if (instance == null)
                instance = new LoadAccount();
            return instance;
        }

        public event Action<List<BotConfig>> LoadAccounts;
        private void OnLoadAccounts(List<BotConfig> accounts)
        {
            if (LoadAccounts != null)
                LoadAccounts(accounts);
        }

        public List<BotConfig> AccountsToLoad { get; set; }
        public String Name { get { return txtNameGroup.Text; } }

        AddAccount addForm = new AddAccount();

        public LoadAccount()
        {
            AccountManager.GetInstance().Refresh += LoadAccount_Refresh;
            InitializeComponent();
        }

        private void LoadAccount_Load(object sender, EventArgs e)
        {
            lstAccounts.SelectionMode = SelectionMode.MultiExtended;
            RefrshAccountsList();
        }

        private void LoadAccount_Refresh(object sender, EventArgs e)
        {
            RefrshAccountsList();
        }

        private void RefrshAccountsList()
        {
            lstAccounts.Items.Clear();
            foreach (BotConfig account in AccountManager.GetInstance().Accounts.Values)
            {
                if (account.Used)
                    continue;
                lstAccounts.Items.Add(account.Username);
            }
        }

        private void lstAccounts_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            CheckedListBox items = (CheckedListBox)sender;
            if (items.CheckedItems.Count > 8)
            {
                e.NewValue = CheckState.Unchecked;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("Are you sure to delete selected account(s) ?",
                                                 "Delete account(s)",
                                                 MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                String[] outp = new String[lstAccounts.SelectedItems.Count];
                lstAccounts.SelectedItems.CopyTo(outp, 0);
                foreach (String obj in outp)
                {
                    AccountManager.GetInstance().RemoveAccount(obj);
                }
            }
        }

        private void kryptonButton3_Click(object sender, EventArgs e)
        {
            if (lstAccounts.SelectedItems.Count == 0)
                return;
            List<BotConfig> lst = new List<BotConfig>();
            foreach (String obj in lstAccounts.SelectedItems)
            {
                AccountManager.GetInstance().Accounts[obj].Used = true;
                lst.Add(AccountManager.GetInstance().Accounts[obj]);
            }
            AccountsToLoad = lst;
            OnLoadAccounts(lst);
            this.Close();
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            if (addForm.Visible)
            {
                addForm.Hide();
                addForm = new AddAccount();
            }
            else
            {
                addForm = new AddAccount();
                addForm.Show();
            }
        }

        private void LoadAccount_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.N)
                kryptonButton1_Click(null, null);
        }

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            List<string> lst = new List<string>();
            foreach (object item in lstAccounts.SelectedItems)
            {
                String str = (string)item;
                lst.Add(str);
            }
            foreach (string str in lst)
            {
                AccountManager.GetInstance().RemoveAccount(str);
            }
        }

        private void lstAccounts_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
    }
}
