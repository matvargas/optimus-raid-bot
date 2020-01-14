using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class WrapperObjectDissociateRequestMessage : NetworkMessage
{

	public const uint Id = 6524;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int HostUID { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte HostPos { get; set; }

	public WrapperObjectDissociateRequestMessage() {}


	public WrapperObjectDissociateRequestMessage InitWrapperObjectDissociateRequestMessage(int HostUID, byte HostPos)
	{
		this.HostUID = HostUID;
		this.HostPos = HostPos;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteVarInt(this.HostUID);
		writer.WriteByte(this.HostPos);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.HostUID = reader.ReadVarInt();
		this.HostPos = reader.ReadByte();
	}
}
}
