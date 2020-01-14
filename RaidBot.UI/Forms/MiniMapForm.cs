using ComponentFactory.Krypton.Toolkit;
using RaidBot.Data.IO.D2P.File;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RaidBot.UI.Forms
{
    public partial class MiniMapForm : KryptonForm
    {
        public MiniMapForm()
        {
            InitializeComponent();
        }

        private void MiniMapForm_Load(object sender, EventArgs e)
        {
            File f = new File(@"C:\Users\acorbeau\AppData\Local\Ankama\Dofus\app\content\gfx\maps\worldmap0.d2p");
        }
    }
}
