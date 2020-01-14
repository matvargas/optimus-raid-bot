using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class SpellVariantActivationMessage : NetworkMessage
{

	public const uint Id = 6705;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short SpellId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool Result { get; set; }

	public SpellVariantActivationMessage() {}


	public SpellVariantActivationMessage InitSpellVariantActivationMessage(short SpellId, bool Result)
	{
		this.SpellId = SpellId;
		this.Result = Result;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteVarShort(this.SpellId);
		writer.WriteBoolean(this.Result);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.SpellId = reader.ReadVarShort();
		this.Result = reader.ReadBoolean();
	}
}
}
