using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class PaddockToSellListMessage : NetworkMessage
{

	public const uint Id = 6138;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short PageIndex { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short TotalPage { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public PaddockInformationsForSell[] PaddockList { get; set; }

	public PaddockToSellListMessage() {}


	public PaddockToSellListMessage InitPaddockToSellListMessage(short PageIndex, short TotalPage, PaddockInformationsForSell[] PaddockList)
	{
		this.PageIndex = PageIndex;
		this.TotalPage = TotalPage;
		this.PaddockList = PaddockList;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteVarShort(this.PageIndex);
		writer.WriteVarShort(this.TotalPage);
		writer.WriteShort(this.PaddockList.Length);
		foreach (PaddockInformationsForSell item in this.PaddockList)
		{
			item.Serialize(writer);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.PageIndex = reader.ReadVarShort();
		this.TotalPage = reader.ReadVarShort();
		int PaddockListLen = reader.ReadShort();
		PaddockList = new PaddockInformationsForSell[PaddockListLen];
		for (int i = 0; i < PaddockListLen; i++)
		{
			this.PaddockList[i] = new PaddockInformationsForSell();
			this.PaddockList[i].Deserialize(reader);
		}
	}
}
}
