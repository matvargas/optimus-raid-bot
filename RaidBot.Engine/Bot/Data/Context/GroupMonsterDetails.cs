using RaidBot.Data.IO.D2I;
using RaidBot.Data.IO.D2O;
using RaidBot.Protocol.DataCenter;
using RaidBot.Protocol.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidBot.Engine.Bot.Data.Context
{
    public class GroupMonsterDetails
    {
        public GameRolePlayGroupMonsterInformations Informatons { get; }
        public Monster StaticInformations { get; }

        public int CellId
        {
            get
            {
                return Informatons.Disposition.CellId;
            }
        }

        public string LeaderName
        {
            get
            {
                return I18nFileAccessor.SafeGetText((int)StaticInformations.NameId);
            }
        }

        public GroupMonsterDetails(GameRolePlayGroupMonsterInformations msg)
        {
            Informatons = msg;
            StaticInformations = GameDataManager.SafeGetObject<Monster>(msg.StaticInfos.MainCreatureLightInfos.CreatureGenericId);
        }
    }
}
