using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class MonsterBoosts : NetworkType
{

	public const uint Id = 497;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int Id_ { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short XpBoost { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short DropBoost { get; set; }

	public MonsterBoosts() {}


	public MonsterBoosts InitMonsterBoosts(int Id_, short XpBoost, short DropBoost)
	{
		this.Id_ = Id_;
		this.XpBoost = XpBoost;
		this.DropBoost = DropBoost;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteVarInt(this.Id_);
		writer.WriteVarShort(this.XpBoost);
		writer.WriteVarShort(this.DropBoost);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.Id_ = reader.ReadVarInt();
		this.XpBoost = reader.ReadVarShort();
		this.DropBoost = reader.ReadVarShort();
	}
}
}
