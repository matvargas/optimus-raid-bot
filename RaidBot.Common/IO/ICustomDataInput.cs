using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidBot.Common.IO
{
    public interface ICustomDataReader:IDataReader
    {
        int ReadVarInt();
        uint ReadVaruhint();
        short ReadVarShort();
        ushort ReadVaruhshort();
        Types.Int64 ReadVarLong();
        Types.UInt64 ReadVaruhlong();
    }
}