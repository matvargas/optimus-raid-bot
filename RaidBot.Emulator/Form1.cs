using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RaidBot.Emulator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void axShockwaveFlash1_Enter(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            axShockwaveFlash1.FlashCall += AxShockwaveFlash1_FlashCall;
            axShockwaveFlash1.Movie = @"C:\Users\acorbeau\AppData\Local\Ankama\Dofus\app\DofusInvockercc.swf";
            axShockwaveFlash1.Play();
        }

        private void AxShockwaveFlash1_FlashCall(object sender, AxShockwaveFlashObjects._IShockwaveFlashEvents_FlashCallEvent e)
        {
            throw new NotImplementedException();
        }
    }
}
