using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class DecraftResultMessage : NetworkMessage
{

	public const uint Id = 6569;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public DecraftedItemStackInfo[] Results { get; set; }

	public DecraftResultMessage() {}


	public DecraftResultMessage InitDecraftResultMessage(DecraftedItemStackInfo[] Results)
	{
		this.Results = Results;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteShort(this.Results.Length);
		foreach (DecraftedItemStackInfo item in this.Results)
		{
			item.Serialize(writer);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		int ResultsLen = reader.ReadShort();
		Results = new DecraftedItemStackInfo[ResultsLen];
		for (int i = 0; i < ResultsLen; i++)
		{
			this.Results[i] = new DecraftedItemStackInfo();
			this.Results[i].Deserialize(reader);
		}
	}
}
}
