using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RaidBot.Data.IO.D2I;
using RaidBot.Data.IO.D2O;
using RaidBot.Protocol.DataCenter;
using RaidBot.Protocol.Types;
namespace RaidBot.Engine.Bot.Data.Context
{
    public class NpcDetails
    {

        public GameRolePlayNpcInformations Informations { get; set; }
        public Npc Static { get; set; }
        public string Name { get; set; }
        public Dictionary<String, NpcAction> Actions { get; set; }

        public NpcDetails(GameRolePlayNpcInformations npc)
        {
            Informations = npc;
            Static = GameDataManager.SafeGetObject<Npc>(npc.NpcId);
            Name = I18nFileAccessor.SafeGetText((int)Static.NameId);
            Actions = new Dictionary<string, NpcAction>();
            foreach(uint item in Static.Actions)
            {
                NpcAction a = GameDataManager.SafeGetObject<NpcAction>((int)item);
                Actions.Add(I18nFileAccessor.SafeGetText((int)a.NameId), a);
            }
        }
    }
}
