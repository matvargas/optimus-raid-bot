using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using RaidBot.Tools.CodeTraductor.Template;
using RaidBot.Tools.CodeTraductor.Parsing;
using RaidBot.Tools.CodeTraductor.Generator;
using RaidBot.Tools.CodeTraductor.BulkGenerator;
using RaidBot.Tools.CodeTraductor.Identification;
using System.Threading;


namespace RaidBot.Tools.CodeTraductor.Interface
{
    public partial class FrmMain : Form
    {
        #region Constructeur

        public FrmMain()
        {
            InitializeComponent();
        }

        #endregion

        #region Interface

        private void collerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtInput.Paste();
        }

        private void copierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtInput.SelectAll();
            txtInput.Copy();
        }

        private void collerToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            txtInput.SelectAll();
            txtInput.Cut();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            txtOutput.Paste();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            txtOutput.SelectAll();
            txtOutput.Copy();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            txtOutput.SelectAll();
            txtOutput.Cut();
        }

        private void btnTranslat_Click(object sender, EventArgs e)
        {
            new Thread(BulkTranslat).Start();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Translat();
        }

        #endregion

        #region Methode

        private void Translat()
        {
            ClassIdent indentificator = new ClassIdent();

            switch (indentificator.GetClassType(txtInput.Text))
            {
                case ClassTypeEnum.MessageOrType:
                    MessageParser parser = new MessageParser(txtInput.Text);
                    MessageGenerator generator = new MessageGenerator(parser.GetClass(), @"C:\Users\Home\Desktop\class");
                 //   try
                  //  {
                        txtOutput.Text = File.ReadAllText(@"C:\Users\Home\Desktop\class\" + parser.GetClass().Name + ".cs", new UTF8Encoding());
                 //   }
                 //   catch { }
                    break;
                case ClassTypeEnum.GameData:

                 
                    break;
            }
        

        }

        private void BulkTranslat()
        {
            if (Directory.Exists(txtNulkInput.Text) && Directory.Exists(txtBulkOutput.Text))
            {
                BulkGenerator.BulkGenerator.FileTranslated += gen_FileTranslated;
                BulkGenerator.BulkGenerator.LoadInfo += gen_LoadInfo;
                BulkGenerator.BulkGenerator.StatsChang += BulkGenerator_StatsChang;
                BulkGenerator.BulkGenerator.GenerateDirectory(txtNulkInput.Text, txtBulkOutput.Text);
            }
            else
                MessageBox.Show("no valid path");
        }

        void BulkGenerator_StatsChang(string obj)
        {
            this.BeginInvoke((MethodInvoker)delegate()
            {
                lblStats.Text = obj;
            });
        }

        private void gen_FileTranslated(object sender, EventArgs e)
        {
            this.BeginInvoke((MethodInvoker)delegate()
            {
                pbMain.Value += 1;
            });
        }

        void gen_LoadInfo(object sender, LoadInfoEventArgs e)
        {
            this.BeginInvoke((MethodInvoker)delegate()
            {
                pbMain.Maximum = e.FilesCount;
                pbMain.Value = 0;
            });
        }

        #endregion


    }
}
