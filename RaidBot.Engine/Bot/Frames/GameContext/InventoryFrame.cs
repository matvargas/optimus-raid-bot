using RaidBot.Engine.Bot.Data;
using RaidBot.Engine.Dispatcher;
using RaidBot.Protocol.Messages;
using RaidBot.Protocol.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidBot.Engine.Bot.Frames.GameContext
{
    public class InventoryFrame : Frame
    {
        public event Action<bool> ObjectDeleteResult;
        private void OnObjectDeleteResult(bool result)
        {
            if (ObjectDeleteResult != null)
                ObjectDeleteResult(result);
        }

        public InventoryFrame(Brain brain) : base(brain)
        {
        }

        [MessageHandlerAttribut(typeof(InventoryWeightMessage))]
        private void HandleInventoryWeightMessage(InventoryWeightMessage msg)
        {
            Brain.State.Player.InventoryWeight.Set(msg);
        }

        [MessageHandlerAttribut(typeof(InventoryContentMessage))]
        private void HandleInventoryContentMessage(InventoryContentMessage msg)
        {
            List<InventoryItemDetail> items = new List<InventoryItemDetail>();
            foreach (ObjectItem item in msg.Objects)
                items.Add(new InventoryItemDetail(item));
            Brain.State.Player.InventoryContent.Set(items);
            Brain.State.Player.Kamas.Set(msg.Kamas);
        }

        [MessageHandlerAttribut(typeof(ObjectAddedMessage))]
        private void HandleObjectAddedMessage(ObjectAddedMessage msg)
        {
            foreach (InventoryItemDetail item in Brain.State.Player.InventoryContent.Get())
            {
                if (item.Item.ObjectGID == msg.Object.ObjectGID && item.Item.ObjectUID == msg.Object.ObjectUID)
                {
                    item.Item.Quantity += msg.Object.Quantity;
                    Brain.State.Player.InventoryContent.OnChanged();
                    return;
                }
            }
            Brain.State.Player.InventoryContent.Get().Add(new InventoryItemDetail(msg.Object));
            Brain.State.Player.InventoryContent.OnChanged();
        }

        [MessageHandlerAttribut(typeof(KamasUpdateMessage))]
        private void HandleKamasUpdateMessage(KamasUpdateMessage msg)
        {
            Brain.State.Player.Kamas.Set(msg.KamasTotal);
        }
    }
}
