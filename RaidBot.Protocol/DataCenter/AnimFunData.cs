using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidBot.Protocol.DataCenter
{
    [Serializable()]
    public abstract class AnimFunData:IData
    {
        private string mAnimName;
        public virtual string AnimName
        {
            get 
            { 
                return mAnimName; 
            }
            set
            {
                mAnimName = value;
            }
        }

        private int mAnimWeight;
        public virtual int AnimWeight
        {
            get
            {
                return mAnimWeight;
            }
            set
            {
                mAnimWeight = value;
            }
        }
    }
}
