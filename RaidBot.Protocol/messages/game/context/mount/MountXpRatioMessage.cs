using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class MountXpRatioMessage : NetworkMessage
{

	public const uint Id = 5970;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte Ratio { get; set; }

	public MountXpRatioMessage() {}


	public MountXpRatioMessage InitMountXpRatioMessage(byte Ratio)
	{
		this.Ratio = Ratio;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteByte(this.Ratio);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.Ratio = reader.ReadByte();
	}
}
}
