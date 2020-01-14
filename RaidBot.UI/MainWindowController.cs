using RaidBot.Engine.Bot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RaidBot.UI.AccountManager;

namespace RaidBot.UI
{
    public class MainWindowController
    {
        private static MainWindowController mInstance = new MainWindowController();
        public MainWindowController GetInstance() { return mInstance; }
        private MainWindowController() {}

        public Dictionary<String, Brain> Workers { get; set; }

        public void LoadWorker(BotConfig account)
        {

        }
    }
}
