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
    public class Server : IData
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
        
        public virtual uint CommentId
        {
            get
            {
                return mCommentId;
            }
            set
            {
                mCommentId = value;
            }
        }
        
        private uint mCommentId;
        
        public virtual double OpeningDate
        {
            get
            {
                return mOpeningDate;
            }
            set
            {
                mOpeningDate = value;
            }
        }
        
        private double mOpeningDate;
        
        public virtual string Language
        {
            get
            {
                return mLanguage;
            }
            set
            {
                mLanguage = value;
            }
        }
        
        private string mLanguage;
        
        public virtual int PopulationId
        {
            get
            {
                return mPopulationId;
            }
            set
            {
                mPopulationId = value;
            }
        }
        
        private int mPopulationId;
        
        public virtual uint GameTypeId
        {
            get
            {
                return mGameTypeId;
            }
            set
            {
                mGameTypeId = value;
            }
        }
        
        private uint mGameTypeId;
        
        public virtual int CommunityId
        {
            get
            {
                return mCommunityId;
            }
            set
            {
                mCommunityId = value;
            }
        }
        
        private int mCommunityId;
        
        public virtual List<String> RestrictedToLanguages
        {
            get
            {
                return mRestrictedToLanguages;
            }
            set
            {
                mRestrictedToLanguages = value;
            }
        }
        
        private List<String> mRestrictedToLanguages = new List<String>();
        
        public Server()
        {
        }
    }
}
