using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class LivingObjectDissociateMessage : NetworkMessage
{

	public const uint Id = 5723;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int LivingUID { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte LivingPosition { get; set; }

	public LivingObjectDissociateMessage() {}


	public LivingObjectDissociateMessage InitLivingObjectDissociateMessage(int LivingUID, byte LivingPosition)
	{
		this.LivingUID = LivingUID;
		this.LivingPosition = LivingPosition;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteVarInt(this.LivingUID);
		writer.WriteByte(this.LivingPosition);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.LivingUID = reader.ReadVarInt();
		this.LivingPosition = reader.ReadByte();
	}
}
}
