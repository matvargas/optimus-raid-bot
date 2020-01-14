using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidBot.Engine.Bot.Data
{
    public class BotStats
    {
        public uint Reconnect { get; set; }
        public uint FightWin { get; set; }
        public uint FightLost { get; set; }
        public uint ErrorsReported { get; set; }
    }
}
