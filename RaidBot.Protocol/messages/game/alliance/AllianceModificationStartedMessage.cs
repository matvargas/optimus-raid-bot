using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class AllianceModificationStartedMessage : NetworkMessage
{

	public const uint Id = 6444;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool CanChangeName { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool CanChangeTag { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool CanChangeEmblem { get; set; }

	public AllianceModificationStartedMessage() {}


	public AllianceModificationStartedMessage InitAllianceModificationStartedMessage(bool CanChangeName, bool CanChangeTag, bool CanChangeEmblem)
	{
		this.CanChangeName = CanChangeName;
		this.CanChangeTag = CanChangeTag;
		this.CanChangeEmblem = CanChangeEmblem;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		byte box = 0;
		box = BooleanByteWrapper.SetFlag(box, 0, CanChangeName);
		box = BooleanByteWrapper.SetFlag(box, 1, CanChangeTag);
		box = BooleanByteWrapper.SetFlag(box, 2, CanChangeEmblem);
		writer.WriteByte(box);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		byte box = reader.ReadByte();
		this.CanChangeName = BooleanByteWrapper.GetFlag(box, 0);
		this.CanChangeTag = BooleanByteWrapper.GetFlag(box, 1);
		this.CanChangeEmblem = BooleanByteWrapper.GetFlag(box, 2);
	}
}
}
