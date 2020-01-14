using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class ShortcutSpell : Shortcut
{

	public const uint Id = 368;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short SpellId { get; set; }

	public ShortcutSpell() {}


	public ShortcutSpell InitShortcutSpell(short SpellId)
	{
		this.SpellId = SpellId;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteVarShort(this.SpellId);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		this.SpellId = reader.ReadVarShort();
	}
}
}
