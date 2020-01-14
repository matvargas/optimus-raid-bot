using RaidBot.Common.Default.Loging;
using RaidBot.Engine.Controler.Game;
using RaidBot.Engine.Controler.Login;
using RaidBot.Engine.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RaidBot.Engine.Enums;

namespace RaidBot.Engine.Controler
{
    public class BotControler
    {
        #region Properity
        public event EventHandler<BotStateChangedEventArgs> BotStateChanged;
        private ConnectedHost mHost;
        public LoginControler Login { get; private set; }
        public GameControler Game { get; private set; }
        private Logger mLogg;
        private BotStatsEnum mBotState;
        public BotStatsEnum BotState
        {
            get
            {
                return mBotState;
            }
            set
           {
               mBotState = value;
               if (BotStateChanged != null)
                   BotStateChanged(this, new BotStateChangedEventArgs { NewState = mBotState });
            }
        }

        #endregion

        #region Constructor

        public BotControler(ConnectedHost host,Logger logg)
        {
            mLogg = logg;
            mHost = host;
            Login = new LoginControler(mHost);
            Game = new GameControler(mHost,logg);
        }

        #endregion

        #region Privat method


        #endregion

        #region Public method

        #endregion

        public class BotStateChangedEventArgs : EventArgs
        {
            public BotStatsEnum NewState;
        }
    }
}
