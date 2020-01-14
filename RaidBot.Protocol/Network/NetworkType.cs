using RaidBot.Common.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidBot.Protocol.Types
{
    public abstract class NetworkType : MarshalByRefObject
    {
        public abstract uint MessageId { get; }

        public abstract void Serialize(ICustomDataWriter writer);
        public abstract void Deserialize(ICustomDataReader reader);
    }
}
