using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class SelectedServerDataExtendedMessage : SelectedServerDataMessage
{

	public const uint Id = 6469;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public GameServerInformations[] Servers { get; set; }

	public SelectedServerDataExtendedMessage() {}


	public SelectedServerDataExtendedMessage InitSelectedServerDataExtendedMessage(GameServerInformations[] Servers)
	{
		this.Servers = Servers;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteShort(this.Servers.Length);
		foreach (GameServerInformations item in this.Servers)
		{
			item.Serialize(writer);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		int ServersLen = reader.ReadShort();
		Servers = new GameServerInformations[ServersLen];
		for (int i = 0; i < ServersLen; i++)
		{
			this.Servers[i] = new GameServerInformations();
			this.Servers[i].Deserialize(reader);
		}
	}
}
}
