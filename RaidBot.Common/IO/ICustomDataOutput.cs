using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidBot.Common.IO
{
    public interface ICustomDataWriter:IDataWriter
    {
        void WriteVarInt(int value);
        void WriteVarShort(short value);
        void WriteVarLong(double value);
        void WriteVaruhint(uint value);
        void WriteVaruhshort(ushort value);
        void WriteVaruhlong(ulong value);
    }
}
