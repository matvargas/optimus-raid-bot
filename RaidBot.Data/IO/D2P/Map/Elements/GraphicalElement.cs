
using RaidBot.Data.IO.D2P.Atouin;
using RaidBot.Common.IO;
using RaidBot.Data.IO.D2P.Map.Elements.Color;
using System.Drawing;
using System;
namespace RaidBot.Data.IO.D2P.Map.Elements
{
        [Serializable()]
    public class GraphicalElement : BasicElement
    {

        #region Declarations

        public uint elementId { get; set; }
        public ColorMultiplicator hue { get; set; }
        public ColorMultiplicator shadow { get; set; }
        public ColorMultiplicator finalTeint { get; set; }
        public Point offset { get; set; }
        public Point pixelOffset { get; set; }
        public int altitude { get; set; }
        public uint identifier { get; set; }

        #endregion

        #region Constructeur
        public GraphicalElement(BigEndianReader raw, sbyte mapVersion)
        {
            elementId = raw.ReadUInt();
            hue = new ColorMultiplicator(raw.ReadByte(), raw.ReadByte(), raw.ReadByte());
            shadow = new ColorMultiplicator(raw.ReadByte(), raw.ReadByte(), raw.ReadByte());

            if (mapVersion <= 4)
            {
                offset = new Point(raw.ReadByte(), raw.ReadByte());
                pixelOffset = new Point((int) (offset.X * AtouinConstants.CELL_HALF_WIDTH), (int) (offset.Y * AtouinConstants.CELL_HALF_HEIGHT));
            }

            else
            {
                pixelOffset = new Point(raw.ReadShort(), raw.ReadShort());
                offset = new Point((int)(pixelOffset.X / AtouinConstants.CELL_HALF_WIDTH), (int)(pixelOffset.Y / AtouinConstants.CELL_HALF_HEIGHT));
            }

            altitude = raw.ReadByte();
            identifier = raw.ReadUInt();
        }

        #endregion

    }
}
