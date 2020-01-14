using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidBot.Common.IO.Types
{
    public class Binary64
    {

        public static uint CHAR_CODE_0 = (uint)'0';
        public static uint CHAR_CODE_9 = (uint)'9';
        public static uint CHAR_CODE_A = (uint)'a';
        public static uint CHAR_CODE_Z = (uint)'z';
        public uint low;
        public uint high;
        public Binary64(uint low, uint high)
        {
            this.low = low;
            this.high = high;
        }

        public uint div(uint n)
        {
            uint modHigh = 0;
            modHigh = this.high % n;
            uint mod = (this.low % n + modHigh * 6) % n;
            this.high = this.high / n;
            float newLow = (modHigh * 4294967296 + this.low) / n;
            this.high = this.high + (uint)(newLow / 4294967296);
            this.low = (uint)newLow;
            return mod;
        }

        public void mul(uint n)
        {
            double newLow = (double)this.low * n;
            this.high = this.high * n;
            this.high = this.high + (uint)(newLow / 4294967296);
            this.low = this.low * n;
        }

        public void add(uint n)
        {
            double newLow = (double)this.low + n;
            this.high = this.high + (uint)(newLow / 4294967296);
            this.low = (uint)newLow;
        }

        public void bitwiseNot()
        {
            this.low = ~this.low;
            this.high = ~this.high;
        }

    }
}