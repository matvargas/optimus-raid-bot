using EasyTabs;
using RaidBot.Data.IO.D2O;
using RaidBot.Data.IO.D2P;
using RaidBot.Data.IO.D2P.File;
using RaidBot.Data.IO.ELE;
using RaidBot.Engine.Bot;
using RaidBot.Protocol.DataCenter;
using RaidBot.UI.Forms;
using RaidBot.UI.Forms.Account;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Permissions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RaidBot.UI
{
    static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread, PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        static void Main()
        {
            //  GameDataManager inst = GameDataManager.GetInstance();
            //  inst.GetObject<Ele> 75312

            ElementsAdapter.GetInstance();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Forms.RaidBot container = new Forms.RaidBot();
            ApplicationContext applicationContext = new ApplicationContext(container);
            Application.Run(applicationContext);
        }
    }
}
