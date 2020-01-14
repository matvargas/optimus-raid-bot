using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RaidBot.Data
{
    public class GeneratingStats : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        protected void Notify([CallerMemberName] string propertyName = null)
        {
            var deleg = PropertyChanged;
            if (deleg != null)
            {
                deleg(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        public GeneratingStats()
        {
            Generating = true;
        }

        private string mMainStats;
        private string mSubStats;
        private int mMainValue;
        private int mSubValue;
        private int mMainMaximum;
        private int mSubMaximum;
        private bool mGenerating;

        public string MainStats
        {
            get { return mMainStats; }
            set
            {
                mMainStats = value;
                Notify();
            }
        }

        public int GenericMainValue
        {
            get
            {
                if (mMainMaximum == 0)
                {
                    return mMainValue * 1000;
                }
                return mMainValue * 100 / mMainMaximum;
            }
        }

        public int GenericSubValue
        {
            get
            {
                if (mSubMaximum != 0)
                    return mSubValue * 100 / mSubMaximum;
                else
                    return 0;
            }
        }

        public string SubStats
        {
            get { return mSubStats; }
            set
            {
                mSubStats = value;
                Notify();
            }
        }

        public int MainValue
        {
            get { return mMainValue; }
            set
            {
                mMainValue = value;
                Notify();
                Notify("GenericMainValue");
            }
        }

        public int SubValue
        {
            get { return mSubValue; }
            set
            {
                mSubValue = value;
                Notify();
                Notify("GenericSubValue");
            }
        }

        public int MainMaximum
        {
            get { return mMainMaximum; }
            set
            {
                mMainMaximum = value;
                Notify();
            }
        }

        public int SubMaximum
        {
            get { return mSubMaximum; }
            set
            {
                mSubMaximum = value;
                Notify();
            }
        }


        public bool Generating
        {
            get { return mGenerating; }
            set
            {
                mGenerating = value;
                Notify();
            }
        }
    }
}