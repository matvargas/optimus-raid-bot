using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class HouseToSellListMessage : NetworkMessage
{

	public const uint Id = 6140;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short PageIndex { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short TotalPage { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public HouseInformationsForSell[] HouseList { get; set; }

	public HouseToSellListMessage() {}


	public HouseToSellListMessage InitHouseToSellListMessage(short PageIndex, short TotalPage, HouseInformationsForSell[] HouseList)
	{
		this.PageIndex = PageIndex;
		this.TotalPage = TotalPage;
		this.HouseList = HouseList;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteVarShort(this.PageIndex);
		writer.WriteVarShort(this.TotalPage);
		writer.WriteShort(this.HouseList.Length);
		foreach (HouseInformationsForSell item in this.HouseList)
		{
			item.Serialize(writer);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.PageIndex = reader.ReadVarShort();
		this.TotalPage = reader.ReadVarShort();
		int HouseListLen = reader.ReadShort();
		HouseList = new HouseInformationsForSell[HouseListLen];
		for (int i = 0; i < HouseListLen; i++)
		{
			this.HouseList[i] = new HouseInformationsForSell();
			this.HouseList[i].Deserialize(reader);
		}
	}
}
}
