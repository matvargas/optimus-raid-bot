using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class IdolListMessage : NetworkMessage
{

	public const uint Id = 6585;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short[] ChosenIdols { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short[] PartyChosenIdols { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public PartyIdol[] PartyIdols { get; set; }

	public IdolListMessage() {}


	public IdolListMessage InitIdolListMessage(short[] ChosenIdols, short[] PartyChosenIdols, PartyIdol[] PartyIdols)
	{
		this.ChosenIdols = ChosenIdols;
		this.PartyChosenIdols = PartyChosenIdols;
		this.PartyIdols = PartyIdols;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteShort(this.ChosenIdols.Length);
		foreach (short item in this.ChosenIdols)
		{
			writer.WriteVarShort(item);
		}
		writer.WriteShort(this.PartyChosenIdols.Length);
		foreach (short item in this.PartyChosenIdols)
		{
			writer.WriteVarShort(item);
		}
		writer.WriteShort(this.PartyIdols.Length);
		foreach (PartyIdol item in this.PartyIdols)
		{
			writer.WriteShort(item.MessageId);
			item.Serialize(writer);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		int ChosenIdolsLen = reader.ReadShort();
		ChosenIdols = new short[ChosenIdolsLen];
		for (int i = 0; i < ChosenIdolsLen; i++)
		{
			this.ChosenIdols[i] = reader.ReadVarShort();
		}
		int PartyChosenIdolsLen = reader.ReadShort();
		PartyChosenIdols = new short[PartyChosenIdolsLen];
		for (int i = 0; i < PartyChosenIdolsLen; i++)
		{
			this.PartyChosenIdols[i] = reader.ReadVarShort();
		}
		int PartyIdolsLen = reader.ReadShort();
		PartyIdols = new PartyIdol[PartyIdolsLen];
		for (int i = 0; i < PartyIdolsLen; i++)
		{
			this.PartyIdols[i] = ProtocolTypeManager.GetInstance<PartyIdol>(reader.ReadShort());
			this.PartyIdols[i].Deserialize(reader);
		}
	}
}
}
