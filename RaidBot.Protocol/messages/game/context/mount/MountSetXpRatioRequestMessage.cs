using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class MountSetXpRatioRequestMessage : NetworkMessage
{

	public const uint Id = 5989;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte XpRatio { get; set; }

	public MountSetXpRatioRequestMessage() {}


	public MountSetXpRatioRequestMessage InitMountSetXpRatioRequestMessage(byte XpRatio)
	{
		this.XpRatio = XpRatio;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteByte(this.XpRatio);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.XpRatio = reader.ReadByte();
	}
}
}
