using MoonSharp.Interpreter;
using RaidBot.Engine.Bot.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RaidBot.Engine.Bot.Managers.DellayManager;

namespace RaidBot.Engine.Bot.Managers.Extension.Extensions
{
    public class InventoryExtension : Extension
    {
        Queue<int> ItemsToDelete;

        public InventoryExtension(Brain brain) : base(brain)
        {
            Brain.State.Player.InventoryContent.changed += InventoryContent_changedAsync;
            ItemsToDelete = new Queue<int>();
        }

        [ExtensionHandler(ExtensionHandlerAttribute.PriorityEnum.VeryHigh)]
        public override async Task RpLoaded()
        {
            if (ItemsToDelete.Count > 0)
            {
                Log("Processing items deletion queue ... ({0} lot to delete)", ItemsToDelete.Count);
                while (ItemsToDelete.Count > 0)
                {
                    int id = ItemsToDelete.Dequeue();
                    foreach (InventoryItemDetail item in Brain.State.Player.InventoryContent.Get())
                    {
                        if (item.Item.ObjectGID == id)
                        {
                            await Task.Delay(DellayManager.GetInstance().Get(DellayType.DeleteObjectInterval));
                            Brain.InventoryManager.DeleteObject(item, item.Item.Quantity);
                            break;
                        }
                    }
                }
            }
            if (Brain.State.Player.Characteristics.Get().StatsPoints > 0)
            {
                await Task.Delay(1200 + Brain.ExtManager.Rnd.Next(200, 500));
                Log("Stats point avaiable ...");
                Brain.ExtManager.CallContextualTrajetEvent(ContextualTrajetEvents.CharactPointAvail);
            }
        }

        private void InventoryContent_changedAsync(List<InventoryItemDetail> data)
        {
            if (data == null || data.Count == 0)
                Brain.ExtManager.CallContextualTrajetEvent(ContextualTrajetEvents.InventoryRefresh);
            if (Brain.ExtManager.Trajet.Get() == null || Brain.ExtManager.Trajet.Get().Globals.Get("AUTO_DELETE") == null || Brain.ExtManager.Trajet.Get().Globals.Get("AUTO_DELETE").Type != DataType.Table)
                return;
            foreach (DynValue val in Brain.ExtManager.Trajet.Get().Globals.Get("AUTO_DELETE").Table.Values)
            {
                if (val.Type == DataType.Number && !ItemsToDelete.Contains((int)val.Number))
                {
                    foreach (InventoryItemDetail detail in Brain.State.Player.InventoryContent.Get())
                    {
                        if (detail.Item.ObjectGID == val.Number)
                        {
                            ItemsToDelete.Enqueue((int)val.Number);
                            Log("{0} added to item deletion queue", val.Number);
                        }
                    }
                }
            }
        }
    }
}
