using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class FightDispellableEffectExtendedInformations : NetworkType
{

	public const uint Id = 208;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short ActionId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public double SourceId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public AbstractFightDispellableEffect Effect { get; set; }

	public FightDispellableEffectExtendedInformations() {}


	public FightDispellableEffectExtendedInformations InitFightDispellableEffectExtendedInformations(short ActionId, double SourceId, AbstractFightDispellableEffect Effect)
	{
		this.ActionId = ActionId;
		this.SourceId = SourceId;
		this.Effect = Effect;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteVarShort(this.ActionId);
		writer.WriteDouble(this.SourceId);
writer.WriteShort(Effect.MessageId);
		Effect.Serialize(writer);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.ActionId = reader.ReadVarShort();
		this.SourceId = reader.ReadDouble();
this.Effect = ProtocolTypeManager.GetInstance<AbstractFightDispellableEffect>(reader.ReadShort());
		this.Effect.Deserialize(reader);
	}
}
}
