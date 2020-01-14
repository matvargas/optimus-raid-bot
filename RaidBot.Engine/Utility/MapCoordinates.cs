using RaidBot.Data.IO.D2O;
using RaidBot.Data.IO.D2P;
using RaidBot.Data.IO.D2P.File;
using RaidBot.Data.IO.D2P.Map;
using RaidBot.Protocol.DataCenter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidBot.Engine.Utility
{
    public class MapCoordinates
    {
        static Dictionary<int, Dictionary<int, List<int>>> MapCoords = null;

        public static System.Drawing.Point GetMapPosition(int mapId)
        {
            MapPosition c = GameDataManager.SafeGetObject<MapPosition>(mapId);
            return (new System.Drawing.Point(c.PosX, c.PosY));
        }

        private static uint getCompressedValue(int v)
        {
            return v < 0 ? (uint)(32768 | v & 32767) : (uint)(v & 32767);
        }

    }
}
