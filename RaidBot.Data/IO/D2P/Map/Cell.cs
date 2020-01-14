using System.Collections.Generic;
using RaidBot.Data.IO.D2P.Map.Elements;
using RaidBot.Common.IO;
using System;


namespace RaidBot.Data.IO.D2P.Map
{
        [Serializable()]
    public class Cell
    {

        #region Declarations
        public int cellId { get; set; }
        public int elementsCount { get; set; }
        public List<BasicElement> elements { get; set; }

        #endregion

        #region Constructeur

        public Cell(BigEndianReader raw, sbyte mapVersion)
        {
            cellId = raw.ReadShort();
            elementsCount = raw.ReadShort();
            elements = new List<BasicElement>();

            for (int i = 0; i < elementsCount; i++)
            {
                elements.Add(BasicElement.GetElementFromType(raw.ReadByte(), raw, mapVersion));
            }
        }

        #endregion

    }
}
