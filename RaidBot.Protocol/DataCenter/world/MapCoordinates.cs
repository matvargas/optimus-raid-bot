//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.18408
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RaidBot.Protocol.DataCenter
{
    using System.Collections.Generic;
    using RaidBot.Common.IO;
    using System;
    
    
    [Serializable()]
    public class MapCoordinates : IData
    {
        
        public virtual uint CompressedCoords
        {
            get
            {
                return mCompressedCoords;
            }
            set
            {
                mCompressedCoords = value;
            }
        }
        
        private uint mCompressedCoords;
        
        public virtual List<int> MapIds
        {
            get
            {
                return mMapIds;
            }
            set
            {
                mMapIds = value;
            }
        }
        
        private List<int> mMapIds = new List<int>();
        
        public MapCoordinates()
        {
        }
    }
}
