using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RaidBot.Interface
{
    public partial class ConsoleForm : Form
    {
        public ConsoleForm()
        {
            InitializeComponent();
        }

        private void ConsoleForm_FormClosing(object sender, FormClosingEventArgs e)
        {
              this.Hide();
              e.Cancel = true; 
        }
        #region Fonctions publiques
        public void Debug(string text)
        {
            AppendText(Color.Black, text);
        }
        public void Info(string text)
        {
            AppendText(Color.Gray, text);
        }
        public void Error(string text)
        {
            AppendText(Color.Red, text);
        }
        public void Succes(string text)
        {
            AppendText(Color.Green, text);
        }
        #endregion

        #region Fonctions privées
        private delegate void AppendTextDelegate(Color color ,string text);
        private void AppendText(Color color, string text)
        {
            if (this.LogTextBox.InvokeRequired)
            {
                this.Invoke(new AppendTextDelegate(AppendText), new object[] {color, text });
                return;
            }
            LogTextBox.SelectionStart = LogTextBox.TextLength;
            LogTextBox.SelectionLength = 0;
            LogTextBox.SelectionColor = color;
            text = "[ "  + DateTime.Now.ToString("HH:mm:ss tt") +"] " + text + "\r\n";
            LogTextBox.AppendText(text);
            LogTextBox.SelectionColor = LogTextBox.ForeColor;
        }
        #endregion
     }
}

