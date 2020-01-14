using RaidBot.Engine.Dispatcher;
using RaidBot.Engine.Manager;
using RaidBot.Protocol.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidBot.Engine.Model.Game.Player.Map
{
    class PlayerMapModel: ModelBase
    {
          public PlayerMapModel(ConnectedHost host)
        {
            host.Dispatcher.Register(this);
        }
          [MessageHandlerAttribut(typeof(GameMapMovementRequestMessage))]
          private void HandleCharacterCharacteristicsInformations(GameMapMovementRequestMessage message, ConnectedHost source)
          {
            //  source.logger.Log("moving");
              source.Bot.BotState = Enums.BotStatsEnum.MOVING;
          }
          [MessageHandlerAttribut(typeof(GameMapMovementConfirmMessage))]
          private void HandleCharacterCharacteristicsInformations(GameMapMovementConfirmMessage message, ConnectedHost source)
          {
              source.Bot.BotState = Enums.BotStatsEnum.INACTIF;
          }

    }
}
