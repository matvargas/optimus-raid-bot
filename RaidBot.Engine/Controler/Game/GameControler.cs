using RaidBot.Common.Default.Loging;
using RaidBot.Engine.Controler.Game.Player;
using RaidBot.Engine.Controler.Game.World;
using RaidBot.Engine.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidBot.Engine.Controler.Game
{
    public class GameControler
    {
        #region Properity

        private ConnectedHost mHost;
        public PlayerControler Player { get; private set; }
        public WorldControler World { get; private set; }
        private Logger mLogg;

        #endregion

        #region Constructor

        public GameControler(ConnectedHost host,Logger logg)
        {
            mHost = host;
            mLogg = logg;
            World = new WorldControler(host);
            Player = new PlayerControler(host, World);

        }

        #endregion

        #region Privat method


        #endregion

        #region Public method

        #endregion
    }
}
