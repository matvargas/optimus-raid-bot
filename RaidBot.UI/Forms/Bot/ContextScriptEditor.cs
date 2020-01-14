using MoonSharp.Interpreter;
using RaidBot.Engine.Bot;
using RaidBot.Engine.Bot.Extension;
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
    public partial class ContextScriptEditor : Form
    {
        public ContextScriptEditor()
        {
            InitializeComponent();
        }

        Brain Brain;
        public ContextScriptEditor(Brain bot)
        {
            InitializeComponent();
            Brain = bot;
        }

        private bool CheckSyntax()
        {
            Script scr = new Script();
            try
            {
                scr.DoString(scintilla.Text);
            }
            catch (MoonSharp.Interpreter.SyntaxErrorException e)
            {
                Match m = Regex.Match(e.DecoratedMessage, @"^[A-z]*[0-9]*:\(([0-9]*),");
                if (m.Success)
                {
                    scintilla.Lines[int.Parse(m.Groups[1].Value) - 1].MarkerAdd(0);
                }
                toolStripStatusLabel1.Text = "(syntax error)" + e.DecoratedMessage.Substring(8);
                return false;
            }
            catch (ScriptRuntimeException e)
            {
                Match m = Regex.Match(e.DecoratedMessage, @"^[A-z]*[0-9]*:\(([0-9]*),");
                if (m.Success)
                {
                    scintilla.Lines[int.Parse(m.Groups[1].Value) - 1].MarkerAdd(0);
                }
                toolStripStatusLabel1.Text = "(runtime error)" + e.DecoratedMessage.Substring(8);
                return false;
            }
            catch
            {
                toolStripStatusLabel1.Text = "function check(currentLifePer, currentTurnNumber, targetLifePer) is required";
                return false;
            }
            toolStripStatusLabel1.Text = "No error found";
            return true;
        }


        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (!CheckSyntax())
                return;
            ExtensionDomain ext = new ExtensionDomain(ExtensionType.Trajet, Brain);
            ext.Load(scintilla.Text);
            Brain.State.UserConfig.Get().ContextualScript = scintilla.Text;
            Brain.State.UserConfig.Get().Save();
            Brain.ExtManager.NeedContextualTrajetRefresh = true;
        }

        private void ContextScriptEditor_Load(object sender, EventArgs e)
        {
            ScintillaLua.InitSyntax(scintilla);
            if (Brain.State.UserConfig.Get() == null || Brain.State.UserConfig.Get().ContextualScript == null)
            {
                MessageBox.Show("Please wait character loading !");
                this.Close();
                return;
            }
                scintilla.Text = Brain.State.UserConfig.Get().ContextualScript;
        }
    }
}
