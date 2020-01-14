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
    public partial class EmptyWindow : Form
    {
        public EmptyWindow()
        {
            InitializeComponent();
        }

        private async Task CloseDellay()
        {
            Task.Delay(500);
            this.Close();
        }

        private void EmptyWindow_Load(object sender, EventArgs e)
        {
            CloseDellay();
        }
    }
}
