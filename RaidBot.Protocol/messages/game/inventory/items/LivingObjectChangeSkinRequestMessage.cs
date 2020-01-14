using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class LivingObjectChangeSkinRequestMessage : NetworkMessage
{

	public const uint Id = 5725;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int LivingUID { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte LivingPosition { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int SkinId { get; set; }

	public LivingObjectChangeSkinRequestMessage() {}


	public LivingObjectChangeSkinRequestMessage InitLivingObjectChangeSkinRequestMessage(int LivingUID, byte LivingPosition, int SkinId)
	{
		this.LivingUID = LivingUID;
		this.LivingPosition = LivingPosition;
		this.SkinId = SkinId;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteVarInt(this.LivingUID);
		writer.WriteByte(this.LivingPosition);
		writer.WriteVarInt(this.SkinId);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.LivingUID = reader.ReadVarInt();
		this.LivingPosition = reader.ReadByte();
		this.SkinId = reader.ReadVarInt();
	}
}
}
