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
    public class QuestStepRewards : IData
    {
        
        public virtual uint Id
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
        
        private uint mId;
        
        public virtual uint StepId
        {
            get
            {
                return mStepId;
            }
            set
            {
                mStepId = value;
            }
        }
        
        private uint mStepId;
        
        public virtual int LevelMin
        {
            get
            {
                return mLevelMin;
            }
            set
            {
                mLevelMin = value;
            }
        }
        
        private int mLevelMin;
        
        public virtual int LevelMax
        {
            get
            {
                return mLevelMax;
            }
            set
            {
                mLevelMax = value;
            }
        }
        
        private int mLevelMax;
        
        public virtual List<List<uint>> ItemsReward
        {
            get
            {
                return mItemsReward;
            }
            set
            {
                mItemsReward = value;
            }
        }
        
        private List<List<uint>> mItemsReward = new List<List<uint>>();
        
        public virtual List<uint> EmotesReward
        {
            get
            {
                return mEmotesReward;
            }
            set
            {
                mEmotesReward = value;
            }
        }
        
        private List<uint> mEmotesReward = new List<uint>();
        
        public virtual List<uint> JobsReward
        {
            get
            {
                return mJobsReward;
            }
            set
            {
                mJobsReward = value;
            }
        }
        
        private List<uint> mJobsReward = new List<uint>();
        
        public virtual List<uint> SpellsReward
        {
            get
            {
                return mSpellsReward;
            }
            set
            {
                mSpellsReward = value;
            }
        }
        
        private List<uint> mSpellsReward = new List<uint>();
        
        public QuestStepRewards()
        {
        }
    }
}
