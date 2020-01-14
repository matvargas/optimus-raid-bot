using RaidBot.Common.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidBot.Protocol.Messages
{
    public abstract class NetworkMessage : MarshalByRefObject
    {
        public byte[] Data { get; set; }
  
        public abstract uint MessageId { get; }

        public abstract void Serialize(ICustomDataWriter writer);
        public abstract void Deserialize(ICustomDataReader reader);

        public void Pack(ICustomDataWriter writerOut, uint globalInstanceId)
        {
            CustomDataWriter writer = new CustomDataWriter();
            Serialize(writer);//write all param of the packet
            byte[] data = writer.Data;//get the packet in array
     
            byte typeLen;//get the size of the size
            if (data.Length > 65535)
                typeLen = 3;
            else if (data.Length > 255)
                typeLen = 2;
            else if (data.Length > 0)
                typeLen = 1;
            else
                typeLen = 0;

            writerOut.WriteShort((short)(MessageId << 2 | typeLen)); //write id and size of size
            writerOut.WriteUInt(globalInstanceId);
            switch (typeLen)//write the size
            {
                case 0:
                    break;
                case 1:
                    writerOut.WriteByte((byte)data.Length);
                    break;
                case 2:
                    writerOut.WriteShort((short)data.Length);
                    break;
                case 3:
                    writerOut.WriteByte((byte)(data.Length >> 16 & 255));
                    writerOut.WriteShort((short)(data.Length & 65535));
                    break;
            }
            writerOut.WriteBytes(data);//write the packet after write the header
        }

        public void PackOld(ICustomDataWriter writerOut)
        {
            CustomDataWriter writer = new CustomDataWriter();
            Serialize(writer);//write all param of the packet
            byte[] data = writer.Data;//get the packet in array

            byte typeLen;//get the size of the size
            if (data.Length > 65535)
                typeLen = 3;
            else if (data.Length > 255)
                typeLen = 2;
            else if (data.Length > 0)
                typeLen = 1;
            else
                typeLen = 0;

            writerOut.WriteShort((short)(MessageId << 2 | typeLen)); //write id and size of size
            switch (typeLen)//write the size
            {
                case 0:
                    break;
                case 1:
                    writerOut.WriteByte((byte)data.Length);
                    break;
                case 2:
                    writerOut.WriteShort((short)data.Length);
                    break;
                case 3:
                    writerOut.WriteByte((byte)(data.Length >> 16 & 255));
                    writerOut.WriteShort((short)(data.Length & 65535));
                    break;
            }
            writerOut.WriteBytes(data);//write the packet after write the header

        }
    }
}
