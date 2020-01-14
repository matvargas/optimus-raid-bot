using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class BasicStatWithDataMessage : BasicStatMessage
{

	public const uint Id = 6573;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public StatisticData[] Datas { get; set; }

	public BasicStatWithDataMessage() {}


	public BasicStatWithDataMessage InitBasicStatWithDataMessage(StatisticData[] Datas)
	{
		this.Datas = Datas;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteShort(this.Datas.Length);
		foreach (StatisticData item in this.Datas)
		{
			writer.WriteShort(item.MessageId);
			item.Serialize(writer);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		int DatasLen = reader.ReadShort();
		Datas = new StatisticData[DatasLen];
		for (int i = 0; i < DatasLen; i++)
		{
			this.Datas[i] = ProtocolTypeManager.GetInstance<StatisticData>(reader.ReadShort());
			this.Datas[i].Deserialize(reader);
		}
	}
}
}
