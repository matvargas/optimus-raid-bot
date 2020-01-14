using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RaidBot.Engine.Manager;
using RaidBot.Engine.Enums;
using RaidBot.Engine.Dispatcher;
using RaidBot.Protocol.Messages;

namespace RaidBot.Engine.Model.Game.Player.Inventory
{
   public class PlayerInventoryModel : ModelBase
    {
        public PlayerInventoryModel(ConnectedHost host)
        {
            host.Dispatcher.Register(this);
        }
        public EventHandler<FullPodsEventArgs> FullPods;
        private uint m_weight;

        public virtual uint Weight
        {
            get
            {
                return m_weight;
            }
            set
            {
                m_weight = value;
               
            }
        }

        private uint m_weightMax;

        public virtual uint WeightMax
        {
            get
            {
                return m_weightMax;
            }
            set
            {
                m_weightMax = value;
            }
        }
        [MessageHandlerAttribut(typeof(InventoryWeightMessage))]
        private void HandleServerSelectionMessage(InventoryWeightMessage message, ConnectedHost source)
        {
            this.Weight = message.weight;
         this.WeightMax = message.weightMax;
            if (Weight > WeightMax)
            {
                if (FullPods != null)
                    FullPods(this, new FullPodsEventArgs());
            }
            OnUpdated();
        }
        public class FullPodsEventArgs : EventArgs
        {
        }
    }
}
