using RaidBot.Protocol.Messages;
using RaidBot.Protocol.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RaidBot.Protocol.DataCenter;
using RaidBot.Data.IO.D2O;
using RaidBot.Data.IO.D2I;
using RaidBot.Engine.Utility.Pathfinding;
using System.Drawing;
using RaidBot.Data.IO.D2P;

namespace RaidBot.Engine.Bot.Data
{
    public class Player
    {
        public ObservableProperty<CharacterBaseInformations> BaseInformation { get; }
        public ObservableProperty<GameRolePlayCharacterInformations> RolePlayInformations { get; }
        public ObservableProperty<CharacterCharacteristicsInformations> Characteristics { get; }
        public ObservableProperty<Dictionary<int, SpellDetail>> Spells { get; }

        public ObservableProperty<List<InventoryItemDetail>> InventoryContent { get; set; }
        public ObservableProperty<InventoryWeightMessage> InventoryWeight { get; set; }
        public ObservableProperty<long> Kamas { get; set; }
        public Breed Breed { get; set; }


        public int InvockPoint
        {
            get
            {
                // TODO maybe compute invock point from items ?
                return Characteristics.Get().SummonableCreaturesBoost.Base + Characteristics.Get().SummonableCreaturesBoost.Additionnal;
            }
        }

        public Player()
        {
            Kamas = new ObservableProperty<long>(0);
            BaseInformation = new ObservableProperty<CharacterBaseInformations>(null);
            BaseInformation.changed += BaseInformation_changed;
            RolePlayInformations = new ObservableProperty<GameRolePlayCharacterInformations>((GameRolePlayCharacterInformations)new GameRolePlayCharacterInformations().InitGameContextActorInformations(0, new EntityLook(), new EntityDispositionInformations()));
            Spells = new ObservableProperty<Dictionary<int, SpellDetail>>(null);
            Characteristics = new ObservableProperty<CharacterCharacteristicsInformations>(null);
            InventoryContent = new ObservableProperty<List<InventoryItemDetail>>(new List<InventoryItemDetail>());
            InventoryWeight = new ObservableProperty<InventoryWeightMessage>(null);
        }

        private void BaseInformation_changed(CharacterBaseInformations data)
        {
            Breed = GameDataManager.SafeGetObject<Breed>(data.Breed);
        }
    }

    public class InventoryItemDetail
    {
        public ObjectItem Item { get; set; }

        public Protocol.DataCenter.Item StaticItem { get; }

        public Image Image { get; private set; }

        public String Name { get; private set; }


        public InventoryItemDetail(ObjectItem item)
        {
            Item = item;
            StaticItem = GameDataManager.SafeGetObject<Protocol.DataCenter.Item>(item.ObjectGID);
            Name = I18nFileAccessor.SafeGetText((int)StaticItem.NameId);
            Image = GfxManager.SafeGetItem(StaticItem.IconId.ToString());
        }
    }

    [Serializable]
    public class SpellDetail
    {
        public SpellItem Item { get; set; }
        public Spell StaticItem { get; set; }
        public SpellLevel CurrentLevel { get; set; }

        public String Name { get; set; }

        public SpellDetail() { }
        public SpellDetail(SpellItem spell)
        {
            Item = spell;
            StaticItem = GameDataManager.SafeGetObject<Spell>(spell.SpellId);
            Name = I18nFileAccessor.SafeGetText((int)StaticItem.NameId);
            CurrentLevel = GameDataManager.SafeGetObject<SpellLevel>((int)StaticItem.SpellLevels[Item.SpellLevel - 1]);
        }
    }
}
