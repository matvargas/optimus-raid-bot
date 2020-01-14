using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class InteractiveUseEndedMessage : NetworkMessage
{

	public const uint Id = 6112;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int ElemId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short SkillId { get; set; }

	public InteractiveUseEndedMessage() {}


	public InteractiveUseEndedMessage InitInteractiveUseEndedMessage(int ElemId, short SkillId)
	{
		this.ElemId = ElemId;
		this.SkillId = SkillId;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteVarInt(this.ElemId);
		writer.WriteVarShort(this.SkillId);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.ElemId = reader.ReadVarInt();
		this.SkillId = reader.ReadVarShort();
	}
}
}
