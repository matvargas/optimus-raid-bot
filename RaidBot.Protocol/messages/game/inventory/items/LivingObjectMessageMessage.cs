using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class LivingObjectMessageMessage : NetworkMessage
{

	public const uint Id = 6065;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short MsgId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int TimeStamp { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public String Owner { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short ObjectGenericId { get; set; }

	public LivingObjectMessageMessage() {}


	public LivingObjectMessageMessage InitLivingObjectMessageMessage(short MsgId, int TimeStamp, String Owner, short ObjectGenericId)
	{
		this.MsgId = MsgId;
		this.TimeStamp = TimeStamp;
		this.Owner = Owner;
		this.ObjectGenericId = ObjectGenericId;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteVarShort(this.MsgId);
		writer.WriteInt(this.TimeStamp);
		writer.WriteUTF(this.Owner);
		writer.WriteVarShort(this.ObjectGenericId);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.MsgId = reader.ReadVarShort();
		this.TimeStamp = reader.ReadInt();
		this.Owner = reader.ReadUTF();
		this.ObjectGenericId = reader.ReadVarShort();
	}
}
}
