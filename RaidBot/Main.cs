using RaidBot.Common.Default.Loging;
using RaidBot.Protocol.Messages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RaidBot
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }
        private void lstPackets_DrawItem_1(object sender, DrawItemEventArgs e)
        {
            if (!(e.Index >= 0 && e.Index < lstPackets.Items.Count))
                return;
            e.DrawBackground();
            Brush myBrush = Brushes.Black;
            var item = lstPackets.Items[e.Index];
            if (filter.Contains(item.ToString().Substring(1).Trim().ToLower()))
                e.Graphics.FillRectangle(new SolidBrush(Color.Yellow), e.Bounds);
            else if (item.ToString().Contains(">"))
                e.Graphics.FillRectangle(new SolidBrush(ColorTranslator.FromHtml("#7aadff")), e.Bounds);
            else
                e.Graphics.FillRectangle(new SolidBrush(ColorTranslator.FromHtml("#146eff")), e.Bounds);

            e.Graphics.DrawString(item.ToString(),
                e.Font, myBrush, e.Bounds, StringFormat.GenericDefault);
            e.DrawFocusRectangle();
        }


        public Logger log;

        private void Main_Load(object sender, EventArgs e)
        {
            log.OnLogObj += Log_OnLogObj;
        }

        private void Log_OnLogObj(object log, LogLevelEnum logLevel)
        {
            this.BeginInvoke(new Action(() =>
            {
                Log((PacketLogContainer)log);
            }));
        }

        public void Log(PacketLogContainer msg)
        {
            if (!checkBox1.Checked || checkBox2.Checked && msg.Received)
                return;
            if (lstPackets.Items.Count > 1000)
            {
                lstPackets.Items.RemoveAt(0);
            }
            lstPackets.Items.Add(msg);
            lstPackets.TopIndex = lstPackets.Items.Count - 1;
        }

        private void propertyGrid1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            lstPackets.Items.Clear();
        }

        private void lstPackets_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                propertyGrid1.SelectedObject = ((PacketLogContainer)lstPackets.SelectedItem).Message;
            }
            catch
            {

            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_Enter(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }


        string[] filter = new string[0];
        private void button2_Click(object sender, EventArgs e)
        {
            filter = (from item in textBox1.Text.Split(',') select item.Trim().ToLower()).ToArray(); 
        }
    }
}
