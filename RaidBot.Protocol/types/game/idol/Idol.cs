using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class Idol : NetworkType
{

	public const uint Id = 489;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short Id_ { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short XpBonusPercent { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short DropBonusPercent { get; set; }

	public Idol() {}


	public Idol InitIdol(short Id_, short XpBonusPercent, short DropBonusPercent)
	{
		this.Id_ = Id_;
		this.XpBonusPercent = XpBonusPercent;
		this.DropBonusPercent = DropBonusPercent;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteVarShort(this.Id_);
		writer.WriteVarShort(this.XpBonusPercent);
		writer.WriteVarShort(this.DropBonusPercent);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.Id_ = reader.ReadVarShort();
		this.XpBonusPercent = reader.ReadVarShort();
		this.DropBonusPercent = reader.ReadVarShort();
	}
}
}
