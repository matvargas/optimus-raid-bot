using RaidBot.Engine.Dispatcher;
using RaidBot.Engine.Frames.Game.World.Fight;
using RaidBot.Engine.Manager;
using RaidBot.Engine.Model.Game.Player.Spells;
using RaidBot.Engine.Model.Game.World.Fight.Enums;
using RaidBot.Protocol.Enums;
using RaidBot.Protocol.Messages;
using RaidBot.Protocol.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidBot.Engine.Model.Game.Player.Fight
{
       public class PlayerFightInformationsModel : ModelBase
    {
           public PlayerFightInformationsModel(ConnectedHost host)
           {
               host.Dispatcher.Register(this);
               UsableSpells = new List<UsableSpellModel>();
           }
        public TeamEnum TeamSide;
        public IAEnum IA = IAEnum.Fuyard;
        public GameFightMinimalStats Stats;
        public List<UsableSpellModel> UsableSpells;
        [MessageHandlerAttribut(typeof(GameFightShowFighterMessage))]
        private void HandleCharacterSelectedSuccessMessages(GameFightShowFighterMessage message, ConnectedHost source)
        {
           if (source.Bot.Game.Player.PlayerBaseInformations.Id == message.informations.contextualId)
           {
               Stats = message.informations.stats;
           }
            OnUpdated();
        }
        [MessageHandlerAttribut(typeof(GameFightSynchronizeMessage))]
        private void HandleCharacterSelectedSuccessMessages(GameFightSynchronizeMessage message, ConnectedHost source)
        {
           foreach(GameFightFighterInformations infos in message.fighters)
           {
               if (infos.contextualId == source.Bot.Game.Player.PlayerBaseInformations.Id)
               {
                   Stats = infos.stats;
               }
           }
            OnUpdated();
        }
        [MessageHandlerAttribut(typeof(GameActionFightPointsVariationMessage))]
        private void HandleCharacterSelectedSuccessMessages(GameActionFightPointsVariationMessage message, ConnectedHost source)
        {
            if (message.actionId == 129 || message.actionId == 127)
                Stats.movementPoints += message.delta;
            else if (message.actionId == 102)
                Stats.actionPoints += message.delta;
            OnUpdated();
        }
       
    }
}
