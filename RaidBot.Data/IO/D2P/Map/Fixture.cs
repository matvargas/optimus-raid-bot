
using RaidBot.Common.IO;
using System;
using System.Drawing;
namespace RaidBot.Data.IO.D2P.Map
{
        [Serializable()]
    public class Fixture
    {

        #region Declarations

        public int fixtureId { get; set; }
        public Point offset { get; set; }
        public int hue { get; set; }
        public int redMultiplier { get; set; }
        public int greenMultiplier { get; set; }
        public int blueMultiplier { get; set; }
        public uint alpha { get; set; }
        public int xScale { get; set; }
        public int yScale { get; set; }
        public int rotation { get; set; }

        #endregion

        #region Constructeur

        public Fixture(BigEndianReader raw)
        {
            fixtureId = raw.ReadInt();

            offset = new Point(raw.ReadShort(), raw.ReadShort());

            rotation = raw.ReadShort();
            xScale = raw.ReadShort();
            yScale = raw.ReadShort();
            redMultiplier = raw.ReadByte();
            greenMultiplier = raw.ReadByte();
            blueMultiplier = raw.ReadByte();
            hue = redMultiplier | greenMultiplier | blueMultiplier;
            alpha = raw.ReadByte();
        }

        #endregion

    }
}
