
using RaidBot.Common.IO;
using System;
using System.Collections.Generic;

namespace RaidBot.Data.IO.D2P.Map
{
    [Serializable()]
    public class Layer
    {

        #region Declarations

        public int layerId { get; set; }
        public int cellsCount { get; set; }
        public List<Cell> cells { get; set; }

        #endregion

        #region Constructeur

        public Layer(BigEndianReader raw, sbyte mapVersion)
        {
            layerId = mapVersion >= 9 ? raw.ReadByte() : raw.ReadInt();
            cellsCount = raw.ReadShort();


            cells = new List<Cell>();

            for (int i = 0; i < cellsCount; i++)
            {
                cells.Add(new Cell(raw, mapVersion));
            }
        }

        #endregion

    }
}
