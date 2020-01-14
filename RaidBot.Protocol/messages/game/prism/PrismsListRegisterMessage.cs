using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class PrismsListRegisterMessage : NetworkMessage
{

	public const uint Id = 6441;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte Listen { get; set; }

	public PrismsListRegisterMessage() {}


	public PrismsListRegisterMessage InitPrismsListRegisterMessage(byte Listen)
	{
		this.Listen = Listen;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteByte(this.Listen);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.Listen = reader.ReadByte();
	}
}
}
