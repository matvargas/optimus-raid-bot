using RaidBot.Engine.Controler.Game.Player;
using RaidBot.Engine.Frames.Game.Player;
using RaidBot.Engine.Functionality.Fight;
using RaidBot.Engine.Manager;
using RaidBot.Engine.Model.Game.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidBot.Engine.Controler.Game.World
{
    public class WorldControler
    {
        #region Properity

        private ConnectedHost mHost;
        public MapInformations Map { get; private set; }
        public FightingFrame fightingFrame { get; private set; }

        #endregion

        #region Constructor

        public WorldControler(ConnectedHost host)
        {
            mHost = host;
            Map = new MapInformations(mHost);
            fightingFrame = new FightingFrame(mHost);
        }

        #endregion

        #region Privat method


        #endregion

        #region Public method


        #endregion
    }
}
