using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RaidBot.Engine.Manager;
using RaidBot.Engine.Enums;
using RaidBot.Protocol.Messages;
namespace RaidBot.Engine.Frames.Game.Player
{
    class CharacteristicsFrame : Frame
    {
        private ConnectedHost mHost;
        public CharacteristicsFrame(ConnectedHost host) 
        {
            mHost = host;
        }
        public void UpgradetStats(BoostableCharacteristicsEnum stat, ushort PointToBoost)
        {
            if (mHost.Bot.BotState != BotStatsEnum.INACTIF || PointToBoost > mHost.Bot.Game.Player.PlayerCharacteristics.StatsPoints)
                return;
            BasicStatMessage message = new BasicStatMessage(151); 
            mHost.SendMessage(message);
            StatsUpgradeRequestMessage msg = new StatsUpgradeRequestMessage(false, (sbyte)stat, PointToBoost);
           mHost.SendMessage(msg);
          
        }
        
       
    }
} 

