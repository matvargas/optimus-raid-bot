using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class ServersListMessage : NetworkMessage
{

	public const uint Id = 30;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public GameServerInformations[] Servers { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short AlreadyConnectedToServerId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool CanCreateNewCharacter { get; set; }

	public ServersListMessage() {}


	public ServersListMessage InitServersListMessage(GameServerInformations[] Servers, short AlreadyConnectedToServerId, bool CanCreateNewCharacter)
	{
		this.Servers = Servers;
		this.AlreadyConnectedToServerId = AlreadyConnectedToServerId;
		this.CanCreateNewCharacter = CanCreateNewCharacter;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteShort(this.Servers.Length);
		foreach (GameServerInformations item in this.Servers)
		{
			item.Serialize(writer);
		}
		writer.WriteVarShort(this.AlreadyConnectedToServerId);
		writer.WriteBoolean(this.CanCreateNewCharacter);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		int ServersLen = reader.ReadShort();
		Servers = new GameServerInformations[ServersLen];
		for (int i = 0; i < ServersLen; i++)
		{
			this.Servers[i] = new GameServerInformations();
			this.Servers[i].Deserialize(reader);
		}
		this.AlreadyConnectedToServerId = reader.ReadVarShort();
		this.CanCreateNewCharacter = reader.ReadBoolean();
	}
}
}
