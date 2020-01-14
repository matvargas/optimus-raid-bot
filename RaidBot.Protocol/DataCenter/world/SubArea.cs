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
    public class SubArea : IData
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
        
        public virtual int AreaId
        {
            get
            {
                return mAreaId;
            }
            set
            {
                mAreaId = value;
            }
        }
        
        private int mAreaId;
        
        public virtual List<AmbientSound> AmbientSounds
        {
            get
            {
                return mAmbientSounds;
            }
            set
            {
                mAmbientSounds = value;
            }
        }
        
        private List<AmbientSound> mAmbientSounds = new List<AmbientSound>();
        
        public virtual List<uint> MapIds
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
        
        private List<uint> mMapIds = new List<uint>();
        
        public virtual Rectangle Bounds
        {
            get
            {
                return mBounds;
            }
            set
            {
                mBounds = value;
            }
        }
        
        private Rectangle mBounds;
        
        public virtual List<int> Shape
        {
            get
            {
                return mShape;
            }
            set
            {
                mShape = value;
            }
        }
        
        private List<int> mShape = new List<int>();
        
        public virtual List<uint> CustomWorldMap
        {
            get
            {
                return mCustomWorldMap;
            }
            set
            {
                mCustomWorldMap = value;
            }
        }
        
        private List<uint> mCustomWorldMap = new List<uint>();
        
        public virtual int PackId
        {
            get
            {
                return mPackId;
            }
            set
            {
                mPackId = value;
            }
        }
        
        private int mPackId;
        
        public virtual uint Level
        {
            get
            {
                return mLevel;
            }
            set
            {
                mLevel = value;
            }
        }
        
        private uint mLevel;
        
        public virtual bool IsConquestVillage
        {
            get
            {
                return mIsConquestVillage;
            }
            set
            {
                mIsConquestVillage = value;
            }
        }
        
        private bool mIsConquestVillage;
        
        public virtual bool BasicAccountAllowed
        {
            get
            {
                return mBasicAccountAllowed;
            }
            set
            {
                mBasicAccountAllowed = value;
            }
        }
        
        private bool mBasicAccountAllowed;
        
        public virtual bool DisplayOnWorldMap
        {
            get
            {
                return mDisplayOnWorldMap;
            }
            set
            {
                mDisplayOnWorldMap = value;
            }
        }
        
        private bool mDisplayOnWorldMap;
        
        public virtual List<uint> Monsters
        {
            get
            {
                return mMonsters;
            }
            set
            {
                mMonsters = value;
            }
        }
        
        private List<uint> mMonsters = new List<uint>();
        
        public virtual List<uint> EntranceMapIds
        {
            get
            {
                return mEntranceMapIds;
            }
            set
            {
                mEntranceMapIds = value;
            }
        }
        
        private List<uint> mEntranceMapIds = new List<uint>();
        
        public virtual List<uint> ExitMapIds
        {
            get
            {
                return mExitMapIds;
            }
            set
            {
                mExitMapIds = value;
            }
        }
        
        private List<uint> mExitMapIds = new List<uint>();
        
        public virtual bool Capturable
        {
            get
            {
                return mCapturable;
            }
            set
            {
                mCapturable = value;
            }
        }
        
        private bool mCapturable;
        
        public SubArea()
        {
        }
    }
}
