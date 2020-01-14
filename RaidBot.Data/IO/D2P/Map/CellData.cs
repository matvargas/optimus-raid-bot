
using RaidBot.Common.IO;
using System;
namespace RaidBot.Data.IO.D2P.Map
{
    [Serializable()]
    public class CellData
    {

        #region Declarations
        public int floor { get; set; }
        public int losmov { get; set; }
        public int speed { get; set; }
        public uint mapChangeData { get; set; }
        public uint moveZone { get; set; }
        public int arrow { get; set; }
        public bool los { get; set; }

        public bool mov { get; set; }
        public bool visible { get; set; }
        public bool farmCell { get; set; }
        public bool blue { get; set; }
        public bool red { get; set; }
        public bool allowWalkRP { get; set; }
        public bool allowWalkFight { get; set; }
        public bool havenBagCell { get; set; }
        public byte linkedZone { get; set; }

        public bool topArrow { get { return (arrow & 1) != 0; } }
        public bool bottomArrow { get { return (arrow & 2) != 0; } }
        public bool rightArrow { get { return (arrow & 4) != 0; } }
        public bool leftArrow { get { return (arrow & 8) != 0; } }

        #endregion

        #region Constructeur

        public bool HasLinkedZoneFight()
        {
            return this.mov && this.allowWalkFight && !this.farmCell && !this.havenBagCell;
        }

        public bool HasLinkedZoneRP()
        {
            return this.mov && !this.farmCell;
        }


        public CellData(BigEndianReader raw, sbyte mapVersion, int id, Map parent)
        {
            bool topArrow, bottomArrow, rightArrow, leftArrow;
            if ((raw.ReadByte() * 10) != -1280)
            {
                if (mapVersion >= 9)
                {
                    ushort tmpbyte = raw.ReadUShort();
                    mov = (tmpbyte & 0x1) == 0;
                    allowWalkFight = (tmpbyte & 2) == 0;
                    allowWalkRP = (tmpbyte & 4) == 0;
                    los = (tmpbyte & 8) == 0;
                    blue = (tmpbyte & 16) == 0;
                    red = (tmpbyte & 32) == 0;
                    visible = (tmpbyte & 64) == 0;
                    farmCell = (tmpbyte & 128) != 0;
                    if (mapVersion >= 10)
                    {
                        havenBagCell = (tmpbyte & 256) != 0;
                        topArrow = (tmpbyte & 512) != 0;
                        bottomArrow = (tmpbyte & 1024) != 0;
                        rightArrow = (tmpbyte & 2048) != 0;
                        leftArrow = (tmpbyte & 4096) != 0;
                    }
                    else
                    {
                        topArrow = (tmpbyte & 256) != 0;
                        bottomArrow = (tmpbyte & 512) != 0;
                        rightArrow = (tmpbyte & 1024) != 0;
                        leftArrow = (tmpbyte & 2048) != 0;
                    }
                    // Todo do something with this
                    if (leftArrow)
                        parent.ArrowsCells[id] = 1;
                    if (rightArrow)
                        parent.ArrowsCells[id] = 2;
                    if (bottomArrow)
                        parent.ArrowsCells[id] = 4;
                    if (topArrow)
                        parent.ArrowsCells[id] = 3;

                }
                else
                {
                    losmov = raw.ReadSByte();
                    los = (losmov & 2) >> 1 == 1;
                    mov = (losmov & 1) == 1;
                    visible = (losmov & 64) >> 6 == 1;
                    farmCell = (losmov & 32) >> 5 == 1;
                    blue = (losmov & 16) >> 4 == 1;
                    red = (losmov & 8) >> 3 == 1;
                    allowWalkRP = (losmov & 128) >> 7 == 1;
                    allowWalkFight = (losmov & 4) >> 2 == 1;
                }
                speed = raw.ReadByte();
                mapChangeData = (uint)raw.ReadSByte();

                //Console.WriteLine("  (CellData) LOS : " + this.los);
                //Console.WriteLine("  (CellData) id : " + id);
                //Console.WriteLine("  (CellData) MapChangeData : " + this.mapChangeData);
                //Console.WriteLine("  (CellData) Mov : " + this.mov);
                if (mapVersion > 5)
                    moveZone = raw.ReadByte();
                //Console.WriteLine("  (CellData) MoveZone : " + this.moveZone);

                if (mapVersion > 10 && (HasLinkedZoneFight() || HasLinkedZoneRP()))
                {
                    linkedZone = raw.ReadByte();
                    //Console.WriteLine("  (CellData) LinkedZoneRP : " + this.HasLinkedZoneRP());
                    //Console.WriteLine("  (CellData) LinkedZoneFight: " + this.HasLinkedZoneFight());
                }
                if (mapVersion > 7 && mapVersion < 9)
                {
                    int tmpBits = 0;
                    tmpBits = raw.ReadByte();
                    arrow = 15 & tmpBits;
                    //Todo do someting with this
                }
            }
            #endregion
        }
    }
}
