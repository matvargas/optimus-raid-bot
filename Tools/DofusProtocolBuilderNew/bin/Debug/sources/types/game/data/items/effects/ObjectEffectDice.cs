using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class ObjectEffectDice : ObjectEffect
{

	public const uint Id = 73;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int DiceNum { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int DiceSide { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int DiceConst { get; set; }

	public ObjectEffectDice() {}


	public ObjectEffectDice InitObjectEffectDice(int DiceNum, int DiceSide, int DiceConst)
	{
		this.DiceNum = DiceNum;
		this.DiceSide = DiceSide;
		this.DiceConst = DiceConst;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteVarInt(this.DiceNum);
		writer.WriteVarInt(this.DiceSide);
		writer.WriteVarInt(this.DiceConst);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		this.DiceNum = reader.ReadVarInt();
		this.DiceSide = reader.ReadVarInt();
		this.DiceConst = reader.ReadVarInt();
	}
}
}
