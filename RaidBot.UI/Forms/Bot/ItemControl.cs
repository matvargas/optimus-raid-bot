using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RaidBot.Engine.Bot.Data;

namespace RaidBot.UI.Forms.Bot
{
    public partial class ItemControl : UserControl
    {
        public InventoryItemDetail Item { get; set; }
        public ItemControl(InventoryItemDetail detail)
        {
            InitializeComponent();
            Item = detail;
            lblQtty.Text = detail.Item.Quantity.ToString();
            pictureBox1.Image = detail.Image;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            ToolTip tp = new ToolTip();
            tp.SetToolTip(pictureBox1, detail.Name);
            this.Width = 50;
            this.Height = 50;

        }

        private void ItemControl_MouseHover(object sender, EventArgs e)
        {
            this.pictureBox1.BackColor = Color.Red;
        }
    }
}
