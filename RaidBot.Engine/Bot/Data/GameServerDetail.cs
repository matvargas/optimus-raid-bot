using RaidBot.Data.IO.D2I;
using RaidBot.Data.IO.D2O;
using RaidBot.Protocol.DataCenter;
using RaidBot.Protocol.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidBot.Engine.Bot.Data
{
    public class GameServerDetail
    {
        public GameServerInformations Informations { get; set; }
        public Server StaticInformations { get; set; }

        public string Name
        {
            get
            {
                return I18nFileAccessor.SafeGetText((int)StaticInformations.NameId);
            }
        }

        public GameServerDetail(GameServerInformations infos)
        {
            Informations = infos;
            StaticInformations = GameDataManager.SafeGetObject<Server>(infos.Id_);
        }
    }
}
