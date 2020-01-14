using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class ExchangeStartedWithPodsMessage : ExchangeStartedMessage
{

	public const uint Id = 6129;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public double FirstCharacterId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int FirstCharacterCurrentWeight { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int FirstCharacterMaxWeight { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public double SecondCharacterId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int SecondCharacterCurrentWeight { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int SecondCharacterMaxWeight { get; set; }

	public ExchangeStartedWithPodsMessage() {}


	public ExchangeStartedWithPodsMessage InitExchangeStartedWithPodsMessage(double FirstCharacterId, int FirstCharacterCurrentWeight, int FirstCharacterMaxWeight, double SecondCharacterId, int SecondCharacterCurrentWeight, int SecondCharacterMaxWeight)
	{
		this.FirstCharacterId = FirstCharacterId;
		this.FirstCharacterCurrentWeight = FirstCharacterCurrentWeight;
		this.FirstCharacterMaxWeight = FirstCharacterMaxWeight;
		this.SecondCharacterId = SecondCharacterId;
		this.SecondCharacterCurrentWeight = SecondCharacterCurrentWeight;
		this.SecondCharacterMaxWeight = SecondCharacterMaxWeight;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteDouble(this.FirstCharacterId);
		writer.WriteVarInt(this.FirstCharacterCurrentWeight);
		writer.WriteVarInt(this.FirstCharacterMaxWeight);
		writer.WriteDouble(this.SecondCharacterId);
		writer.WriteVarInt(this.SecondCharacterCurrentWeight);
		writer.WriteVarInt(this.SecondCharacterMaxWeight);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		this.FirstCharacterId = reader.ReadDouble();
		this.FirstCharacterCurrentWeight = reader.ReadVarInt();
		this.FirstCharacterMaxWeight = reader.ReadVarInt();
		this.SecondCharacterId = reader.ReadDouble();
		this.SecondCharacterCurrentWeight = reader.ReadVarInt();
		this.SecondCharacterMaxWeight = reader.ReadVarInt();
	}
}
}
