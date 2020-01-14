using RaidBot.Data.IO.D2I;
using RaidBot.Data.IO.D2O;
using RaidBot.Engine.Bot;
using RaidBot.Engine.Bot.Data;
using RaidBot.Protocol.DataCenter;
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
    public partial class CharacterCreationForm : Form
    {
        Brain Brain;
        Dictionary<String, int> classes;

        public CharacterCreationForm()
        {
            InitializeComponent();
        }

        public CharacterCreationForm(Brain bot)
        {
            InitializeComponent();
            Brain = bot;
            Brain.ServerManager.CaracterCreationResult += ServerManager_CaracterCreationResult;
        }

        private void ServerManager_CaracterCreationResult(Engine.Bot.Managers.ServerSelectionManager.CharacterCreationResultEnum obj)
        {
            this.BeginInvoke(new Action(() =>
            {
                if (obj == Engine.Bot.Managers.ServerSelectionManager.CharacterCreationResultEnum.Success)
                    this.Close();
                else
                    this.label1.Text = "Invalide name";
            }));
        }

        private void CharacterCreationForm_Load(object sender, EventArgs e)
        {
            lstClass.Items.Clear();
            classes = new Dictionary<string, int>();
            List<Breed> breeds = GameDataManager.SafeGetAllObject<Breed>();
            foreach (Breed b in breeds)
            {
                String text = I18nFileAccessor.SafeGetText((int)b.ShortNameId);
                lstClass.Items.Add(text);
                classes[text] = b.Id;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (txtName.Text == String.Empty || lstClass.SelectedItem == null)
            {
                label1.Text = "Pleas fill all fields";
                return;
            }
            Brain.ServerManager.RequestCharacterCreation(classes[(String)lstClass.SelectedItem], txtName.Text, !radioMr.Checked);
        }

        private void CharacterCreationForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Brain.ServerManager.CaracterCreationResult -= ServerManager_CaracterCreationResult;

        }
    }
}
