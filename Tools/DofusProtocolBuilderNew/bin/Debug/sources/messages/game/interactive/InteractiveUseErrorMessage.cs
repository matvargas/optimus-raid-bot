using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class InteractiveUseErrorMessage : NetworkMessage
{

	public const uint Id = 6384;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int ElemId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int SkillInstanceUid { get; set; }

	public InteractiveUseErrorMessage() {}


	public InteractiveUseErrorMessage InitInteractiveUseErrorMessage(int ElemId, int SkillInstanceUid)
	{
		this.ElemId = ElemId;
		this.SkillInstanceUid = SkillInstanceUid;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteVarInt(this.ElemId);
		writer.WriteVarInt(this.SkillInstanceUid);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.ElemId = reader.ReadVarInt();
		this.SkillInstanceUid = reader.ReadVarInt();
	}
}
}
