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
    public class AlignmentRank : IData
    {
        
        public virtual int Id
        {
            get
            {
                return mId;
            }
            set
            {
                mId = value;
            }
        }
        
        private int mId;
        
        public virtual uint OrderId
        {
            get
            {
                return mOrderId;
            }
            set
            {
                mOrderId = value;
            }
        }
        
        private uint mOrderId;
        
        public virtual uint NameId
        {
            get
            {
                return mNameId;
            }
            set
            {
                mNameId = value;
            }
        }
        
        private uint mNameId;
        
        public virtual uint DescriptionId
        {
            get
            {
                return mDescriptionId;
            }
            set
            {
                mDescriptionId = value;
            }
        }
        
        private uint mDescriptionId;
        
        public virtual int MinimumAlignment
        {
            get
            {
                return mMinimumAlignment;
            }
            set
            {
                mMinimumAlignment = value;
            }
        }
        
        private int mMinimumAlignment;
        
        public virtual int ObjectsStolen
        {
            get
            {
                return mObjectsStolen;
            }
            set
            {
                mObjectsStolen = value;
            }
        }
        
        private int mObjectsStolen;
        
        public virtual List<int> Gifts
        {
            get
            {
                return mGifts;
            }
            set
            {
                mGifts = value;
            }
        }
        
        private List<int> mGifts = new List<int>();
        
        public AlignmentRank()
        {
        }
    }
}
