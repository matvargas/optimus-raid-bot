using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class MountEmoteIconUsedOkMessage : NetworkMessage
{

	public const uint Id = 5978;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int MountId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte ReactionType { get; set; }

	public MountEmoteIconUsedOkMessage() {}


	public MountEmoteIconUsedOkMessage InitMountEmoteIconUsedOkMessage(int MountId, byte ReactionType)
	{
		this.MountId = MountId;
		this.ReactionType = ReactionType;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteVarInt(this.MountId);
		writer.WriteByte(this.ReactionType);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.MountId = reader.ReadVarInt();
		this.ReactionType = reader.ReadByte();
	}
}
}
