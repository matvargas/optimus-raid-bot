using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class ShortcutSmiley : Shortcut
{

	public const uint Id = 388;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short SmileyId { get; set; }

	public ShortcutSmiley() {}


	public ShortcutSmiley InitShortcutSmiley(short SmileyId)
	{
		this.SmileyId = SmileyId;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteVarShort(this.SmileyId);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		this.SmileyId = reader.ReadVarShort();
	}
}
}
