using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class ServerStatusUpdateMessage : NetworkMessage
{

	public const uint Id = 50;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public GameServerInformations Server { get; set; }

	public ServerStatusUpdateMessage() {}


	public ServerStatusUpdateMessage InitServerStatusUpdateMessage(GameServerInformations Server)
	{
		this.Server = Server;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		this.Server.Serialize(writer);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.Server = new GameServerInformations();
		this.Server.Deserialize(reader);
	}
}
}
