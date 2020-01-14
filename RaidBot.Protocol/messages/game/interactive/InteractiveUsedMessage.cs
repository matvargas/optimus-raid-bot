using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class InteractiveUsedMessage : NetworkMessage
{

	public const uint Id = 5745;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public long EntityId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int ElemId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short SkillId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short Duration { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool CanMove { get; set; }

	public InteractiveUsedMessage() {}


	public InteractiveUsedMessage InitInteractiveUsedMessage(long EntityId, int ElemId, short SkillId, short Duration, bool CanMove)
	{
		this.EntityId = EntityId;
		this.ElemId = ElemId;
		this.SkillId = SkillId;
		this.Duration = Duration;
		this.CanMove = CanMove;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteVarLong(this.EntityId);
		writer.WriteVarInt(this.ElemId);
		writer.WriteVarShort(this.SkillId);
		writer.WriteVarShort(this.Duration);
		writer.WriteBoolean(this.CanMove);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.EntityId = reader.ReadVarLong();
		this.ElemId = reader.ReadVarInt();
		this.SkillId = reader.ReadVarShort();
		this.Duration = reader.ReadVarShort();
		this.CanMove = reader.ReadBoolean();
	}
}
}
