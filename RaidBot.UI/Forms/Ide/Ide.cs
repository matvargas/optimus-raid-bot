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
using MoonSharp.Interpreter;
using RaidBot.Engine.Bot;
using RaidBot.Engine.Bot.Managers;
using ScintillaNET;

namespace RaidBot.UI.Forms.Ide
{
    public partial class Ide : Form
    {
        Scintilla textArea;
        String preload = null;

        #region Ctor

        public Ide()
        {
            InitializeComponent();
        }

        public Ide(String preload)
        {
            InitializeComponent();
            this.preload = preload;
            
        }

        #endregion

        #region Hightlight and auto complete

        private void Ide_Load(object sender, EventArgs e)
        {
            textArea = new Scintilla();
            this.Controls.Add(textArea);
            textArea.Dock = DockStyle.Fill;
            toolStrip1.SendToBack();
            ScintillaLua.InitSyntax(textArea);
            textArea.CharAdded += TextArea_CharAdded;
            textArea.TextChanged += TextArea_TextChanged;
            Brain.ActiveInstances.changed += ActiveInstances_changed;
            ActiveInstances_changed(Brain.ActiveInstances);

            if (preload != null)
            {
                textArea.Text = ScriptsManager.Load(preload);
                txtName.Text = preload;
            }
        }

        private int maxLineNumberCharLength = 9;
        private void TextArea_TextChanged(object sender, EventArgs e)
        {
            var maxLineNumberCharLength = textArea.Lines.Count.ToString().Length;
            if (maxLineNumberCharLength == this.maxLineNumberCharLength)
                return;

            // Calculate the width required to display the last line number
            // and include some padding for good measure.
            const int padding = 2;
            textArea.Margins[0].Width = textArea.TextWidth(Style.LineNumber, new string('9', maxLineNumberCharLength + 1)) + padding;
            this.maxLineNumberCharLength = maxLineNumberCharLength;

        }

        private void TextArea_CharAdded(object sender, CharAddedEventArgs e)
        {
            InsertMatchedChars(e);
        }


        static void SetAutoComplete(string[] exportFunctions, string[] exportVars)
        {

        }


        private void InsertMatchedChars(CharAddedEventArgs e)
        {
            // Find the word start
            var currentPos = textArea.CurrentPosition;
            var wordStartPos = textArea.WordStartPosition(currentPos, true);

            // Display the autocompletion list
            var lenEntered = currentPos - wordStartPos;
            if (lenEntered > 0 && !textArea.AutoCActive)
            {
                string curr = textArea.Lines[textArea.LineFromPosition(textArea.CurrentPosition)].Text;
                if (Regex.IsMatch(curr, @"^[\s]*function[\s]+([A-z]*)"))
                    textArea.AutoCShow(lenEntered, "bank() lost() move() msg_recv(msg) msg_send(msg) phoenix(map)");
                else if (Regex.IsMatch(curr, @"^[\s]*{.*}[\s]*,*$"))
                    textArea.AutoCShow(lenEntered, "door= fight= gather= map= path=");
                else
                    textArea.AutoCShow(lenEntered, "AUTO_DELETE DISPLAY_FIGHT_COUNT MAX_MONSTERS MAX_PODS MIN_MONSTERS and do else elseif end end false for function if in in local nil not or repeat return then true until while");
            }
        }

        #endregion

        #region Menu

        private void ActiveInstances_changed(Dictionary<string, Brain> data)
        {
            try
            {
                cmbActive.Items.Clear();
                foreach (string user in data.Keys)
                {
                    cmbActive.Items.Add(user);
                }
            }
            catch { }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (txtName.Text == "")
                return;
            ScriptsManager.Save(txtName.Text, textArea.Text);
        }

        #endregion

        #region Debugger

        Brain currentTarget = null;

        private void HandleErrMsg(String str)
        {
            for (int i = 0; i < textArea.Lines.Count; i++)
            {
                textArea.Lines[i].MarkerDelete(-1);
            }
            Match m = Regex.Match(str, @"^[A-z]*[0-9]*:\(([0-9]*),");
            if (m.Success)
            {
                textArea.Lines[int.Parse(m.Groups[1].Value) - 1].MarkerAdd(0);
            }
            this.lblError.Text = str.Substring(8);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.lblError.Text = null;
            if (cmbActive.SelectedItem != null)
            {
                DebugExtensionHost host = new DebugExtensionHost(textArea.Text, currentTarget);
                host.SyntaxError += (line, str) =>
                {
                    HandleErrMsg(String.Format("{0} (syntax error)", str));
                };
                host.RunTimeError += (error) =>
                {
                    HandleErrMsg(String.Format("{0} (runtime error)", error.DecoratedMessage));
                };
                host.ParseContent();
                currentTarget = Brain.ActiveInstances.Get()[(string)cmbActive.SelectedItem];
                currentTarget.ExtManager.RegisterExtension(host);
                btnRecorde.Enabled = true;
            } else
            {
                currentTarget = null;
                btnRecorde.Enabled = false;
            }
        }

        bool macroRecording = false;

        private void btnRecorde_Click(object sender, EventArgs e)
        {
            if (macroRecording)
            {
                macroRecording = false;
                btnRecorde.Image = Properties.Resources.cgreen;
            }
            else
            {
                macroRecording = true;
                btnRecorde.Image = Properties.Resources.cred;
            }
        }

        #endregion
    }
}
