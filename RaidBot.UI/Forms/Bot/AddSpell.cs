using ComponentFactory.Krypton.Toolkit;
using MoonSharp.Interpreter;
using RaidBot.Engine.Bot;
using RaidBot.Engine.Bot.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RaidBot.UI.Forms.Bot
{
    public partial class AddSpell : KryptonForm
    {
        Brain Brain;

        public AddSpell()
        {
            InitializeComponent();
        }

        public AddSpell(Brain bot)
        {
            InitializeComponent();
            lblError.SelectedIndex = 0;
            Brain = bot;
            foreach (SpellDetail spell in Brain.State.Player.Spells.Get().Values)
            {
                lstSpell.Items.Add(new ListItem(spell));
            }
            Priority.Value = bot.State.UserConfig.Get().SpellStack.Count;
            ScintillaLua.InitSyntax(scintilla);
        }

        class ListItem
        {
            public SpellDetail Spell { get;  }
            public ListItem(SpellDetail spell)
            {
                Spell = spell;
            }

            public override string ToString()
            {
                return String.Format("{0} (Niveau {1})", Spell.Name, Spell.Item.SpellLevel);
            }
        }

        private void AddSpell_Load(object sender, EventArgs e)
        {

        }

        private bool CheckSyntax()
        {

            Script scr = new Script();
            try
            {
                scr.DoString(scintilla.Text);
                scr.Call(scr.Globals["check"], 100, 1, 100);
                scr.Call(scr.Globals["check"], 0, 0, 0);
                scr.Call(scr.Globals["check"], -100, 1000, -100);
                scr.Call(scr.Globals["check"], 545454554, 3005045, 545100);
            }
            catch (MoonSharp.Interpreter.SyntaxErrorException e)
            {
                Match m = Regex.Match(e.DecoratedMessage, @"^[A-z]*[0-9]*:\(([0-9]*),");
                if (m.Success)
                {
                    scintilla.Lines[int.Parse(m.Groups[1].Value) - 1].MarkerAdd(0);
                }
                this.label1.Text = "(syntax error)" + e.DecoratedMessage.Substring(8);
                return false;
            }
            catch (ScriptRuntimeException e)
            {
                Match m = Regex.Match(e.DecoratedMessage, @"^[A-z]*[0-9]*:\(([0-9]*),");
                if (m.Success)
                {
                    scintilla.Lines[int.Parse(m.Groups[1].Value) - 1].MarkerAdd(0);
                }
                this.label1.Text = "(runtime error)" + e.DecoratedMessage.Substring(8);
                return false;
            }
            catch
            {
                this.label1.Text = "function check(currentLifePer, currentTurnNumber, targetLifePer) is required";
                return false;
            }
            label1.Text = "No error found";
            return true;
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            if (!CheckSyntax())
                return;
            if (lstSpell.SelectedItem == null)
            {
                label1.Text = "Please select a spell";
                return;
            }
            foreach (SpellStackItem item in Brain.State.UserConfig.Get().SpellStack)
            {
                if (item.Priority == Priority.Value)
                {
                   label1.Text = "Please select a uniq priority";
                    return;
                }
            }
            SpellCastType castType;
            switch (lblError.SelectedIndex)
            {
                case 0:
                    castType = SpellCastType.Ennemies;
                    break;
                case 1:
                    castType = SpellCastType.Allies;
                    break;
                case 2:
                    castType = SpellCastType.Self;
                    break;
                case 3:
                    castType = SpellCastType.AroundEnnemies;
                    break;
                case 4:
                    castType = SpellCastType.AroundAllies;
                    break;
                default:
                    castType = SpellCastType.AroundSelf;
                    break;
            }
            Brain.State.UserConfig.Get().SpellStack.Add(new SpellStackItem(((ListItem)lstSpell.SelectedItem).Spell, (int)Priority.Value, castType, scintilla.Text));
            Brain.State.UserConfig.OnChanged();
            this.Close();
        }
    }
}
