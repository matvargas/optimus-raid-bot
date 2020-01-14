using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class IgnoredListMessage : NetworkMessage
{

	public const uint Id = 5674;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public IgnoredInformations[] IgnoredList { get; set; }

	public IgnoredListMessage() {}


	public IgnoredListMessage InitIgnoredListMessage(IgnoredInformations[] IgnoredList)
	{
		this.IgnoredList = IgnoredList;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteShort(this.IgnoredList.Length);
		foreach (IgnoredInformations item in this.IgnoredList)
		{
			writer.WriteShort(item.MessageId);
			item.Serialize(writer);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		int IgnoredListLen = reader.ReadShort();
		IgnoredList = new IgnoredInformations[IgnoredListLen];
		for (int i = 0; i < IgnoredListLen; i++)
		{
			this.IgnoredList[i] = ProtocolTypeManager.GetInstance<IgnoredInformations>(reader.ReadShort());
			this.IgnoredList[i].Deserialize(reader);
		}
	}
}
}
