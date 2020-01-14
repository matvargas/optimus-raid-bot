using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class GameActionFightSpellCastMessage : AbstractGameActionFightTargetedAbilityMessage
{

	public const uint Id = 1010;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short SpellId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short SpellLevel { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short[] PortalsIds { get; set; }

	public GameActionFightSpellCastMessage() {}


	public GameActionFightSpellCastMessage InitGameActionFightSpellCastMessage(short SpellId, short SpellLevel, short[] PortalsIds)
	{
		this.SpellId = SpellId;
		this.SpellLevel = SpellLevel;
		this.PortalsIds = PortalsIds;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteVarShort(this.SpellId);
		writer.WriteShort(this.SpellLevel);
		writer.WriteShort(this.PortalsIds.Length);
		foreach (short item in this.PortalsIds)
		{
			writer.WriteShort(item);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		this.SpellId = reader.ReadVarShort();
		this.SpellLevel = reader.ReadShort();
		int PortalsIdsLen = reader.ReadShort();
		PortalsIds = new short[PortalsIdsLen];
		for (int i = 0; i < PortalsIdsLen; i++)
		{
			this.PortalsIds[i] = reader.ReadShort();
		}
	}
}
}
