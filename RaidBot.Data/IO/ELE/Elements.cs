using RaidBot.Common.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidBot.Data.IO.ELE
{
    public class Elements
    {
        public uint FileVersion { get; private set; }
        public uint ElementsCount { get; private set; }

        public Dictionary<int, long> ElementsIndex;
        private Dictionary<int, bool> JpegMap;


        public Elements(BigEndianReader reader)
        {
            int header = 0;
            uint skypLen = 0;
            int i  = 0;
            int edId = 0;
            int gfxCount = 0;
            int gfxId = 0;
            FileVersion = reader.ReadByte();
            ElementsCount = reader.ReadUnsignedInt();
            ElementsIndex = new Dictionary<int, long>();
            skypLen = 0;
            for (i = 0; i < ElementsCount; i++)
            {
                if (FileVersion >= 9)
                    skypLen = reader.ReadUShort();
                edId = reader.ReadInt();
                if (FileVersion <= 8)
                {
                    this.ElementsIndex[edId] = reader.Position;
                    ReadElement(edId);
                }
                else
                {
                    this.ElementsIndex[edId] = reader.Position;
                    reader.Seek((int)skypLen - 4, SeekOrigin.Current);
                }
            }
            if (FileVersion >= 8)
            {
                JpegMap = new Dictionary<int, bool>();
                gfxCount = reader.ReadInt();
                for (i = 0; i < gfxCount; i++)
                {
                    JpegMap[reader.ReadInt()] = true;
                }
            }
        }

        private void ReadElement(int id)
        {

        }
    }
}
