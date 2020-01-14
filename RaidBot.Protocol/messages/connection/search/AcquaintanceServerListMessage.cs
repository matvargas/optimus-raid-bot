using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class AcquaintanceServerListMessage : NetworkMessage
{

	public const uint Id = 6142;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short[] Servers { get; set; }

	public AcquaintanceServerListMessage() {}


	public AcquaintanceServerListMessage InitAcquaintanceServerListMessage(short[] Servers)
	{
		this.Servers = Servers;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteShort(this.Servers.Length);
		foreach (short item in this.Servers)
		{
			writer.WriteVarShort(item);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		int ServersLen = reader.ReadShort();
		Servers = new short[ServersLen];
		for (int i = 0; i < ServersLen; i++)
		{
			this.Servers[i] = reader.ReadVarShort();
		}
	}
}
}
