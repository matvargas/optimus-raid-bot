using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RaidBot.Engine.Manager;
using RaidBot.Engine.Dispatcher;
using RaidBot.Protocol.Messages;
using RaidBot.Protocol.Types;
namespace RaidBot.Engine.Model.Game.Player.Spells
{
    public class PlayerSpellsModel : ModelBase
    {
        public PlayerSpellsModel(ConnectedHost host)
        {
            host.Dispatcher.Register(this);
        }
        public List<SpellData> Spells = new List<SpellData>();
        [MessageHandlerAttribut(typeof(SpellListMessage))]
        private void HandleCharacterCharacteristicsInformations(SpellListMessage message, ConnectedHost source)
        {
            this.Spells.Clear();
            foreach (SpellItem spell in message.spells)
                this.Spells.Add(new SpellData(spell));
            OnUpdated();
        }
        [MessageHandlerAttribut(typeof(SpellUpgradeSuccessMessage))]
        private void HandleCharacterCharacteristicsInformations(SpellUpgradeSuccessMessage message, ConnectedHost source)
        {
            foreach (SpellData spellData in this.Spells)
            {
                if (spellData.SpellId == message.spellId)
                {
                    spellData.SpellLevelId = message.spellLevel;
                    break;
                }
            }
            OnUpdated();
        }
    }
}
