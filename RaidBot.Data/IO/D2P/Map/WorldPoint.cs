using System;
namespace RaidBot.Data.IO.D2P.Map
{
        [Serializable()]
    public class WorldPoint
    {

        #region Declarations

        public uint mapId { get; set; }
        public uint worldId { get; set; }
        public int x { get; set; }
        public int y { get; set; }

        #endregion

        #region Constructeur

        public WorldPoint(uint _mapId)
        {
            mapId = _mapId;
            worldId = (mapId & 1073479680) >> 18;

            x = (int)(mapId >> 9 & 511);
            y = (int)(mapId & 511);

            if ((x & 256) == 256)
            {
                x = -(x & 255);
            }

            if ((y & 256) == 256)
            {
                y = -(y & 255);
            }
        }

        #endregion

    }
}
