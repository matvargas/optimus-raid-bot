using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class AbstractGameActionFightTargetedAbilityMessage : AbstractGameActionMessage
{

	public const uint Id = 6118;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool SilentCast { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool VerboseCast { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public double TargetId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short DestinationCellId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte Critical { get; set; }

	public AbstractGameActionFightTargetedAbilityMessage() {}


	public AbstractGameActionFightTargetedAbilityMessage InitAbstractGameActionFightTargetedAbilityMessage(bool SilentCast, bool VerboseCast, double TargetId, short DestinationCellId, byte Critical)
	{
		this.SilentCast = SilentCast;
		this.VerboseCast = VerboseCast;
		this.TargetId = TargetId;
		this.DestinationCellId = DestinationCellId;
		this.Critical = Critical;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		byte box = 0;
		box = BooleanByteWrapper.SetFlag(box, 0, SilentCast);
		box = BooleanByteWrapper.SetFlag(box, 1, VerboseCast);
		writer.WriteByte(box);
		writer.WriteDouble(this.TargetId);
		writer.WriteShort(this.DestinationCellId);
		writer.WriteByte(this.Critical);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		byte box = reader.ReadByte();
		this.SilentCast = BooleanByteWrapper.GetFlag(box, 0);
		this.VerboseCast = BooleanByteWrapper.GetFlag(box, 1);
		this.TargetId = reader.ReadDouble();
		this.DestinationCellId = reader.ReadShort();
		this.Critical = reader.ReadByte();
	}
}
}
