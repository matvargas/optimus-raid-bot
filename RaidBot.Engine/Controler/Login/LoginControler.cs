using RaidBot.Engine.Frames.Connection;
using RaidBot.Engine.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidBot.Engine.Controler.Login
{
    public class LoginControler
    {
        public AutentificationFrame mAutentificationFrame;
        private ConnectedHost mHost;

        public LoginControler(ConnectedHost host)
        {
            mHost = host;
            mAutentificationFrame = new AutentificationFrame();
            mAutentificationFrame.ConnectedToGame += mAutentificationFrame_ConnectedToGame;
            mAutentificationFrame.Init(mHost);
        }

        void mAutentificationFrame_ConnectedToGame(object sender, EventArgs e)
        {
         //mAutentificationFrame.UnInit();
           // mAutentificationFrame = null;
            mHost.Bot.BotState = Enums.BotStatsEnum.INACTIF;
        }
    }
}
