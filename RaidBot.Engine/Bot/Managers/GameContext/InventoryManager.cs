using RaidBot.Protocol.Messages;
using RaidBot.Engine.Bot.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidBot.Engine.Bot.Managers.GameContext
{
    public class InventoryManager : Manager
    {

        public InventoryManager(Brain brain) : base(brain)
        {
        }

        public void DeleteObject(InventoryItemDetail detail, int quantity)
        {
            Brain.SendMessage(new ObjectDeleteMessage().InitObjectDeleteMessage(detail.Item.ObjectUID, detail.Item.Quantity));
            if (detail.Item.Quantity <= quantity)
            {
                Brain.State.Player.InventoryContent.Get().Remove(detail);
                Brain.State.Player.InventoryContent.OnChanged();
            }
            else
                detail.Item.Quantity -= quantity;
        }
    }
}
