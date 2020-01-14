using System;
using System.Collections.Generic;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;

namespace Raidbot.Protocol.Messages
{
public class NetworkDataContainerMessage : NetworkMessage
{

	public const uint Id = 2;
	public override uint MessageId { get { return Id; } }


	public override void Serialize(ICustomDataWriter writer)
	{
		//this.Content.Serialize(writer);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		//this.Content.Deserialize(reader);
	}
}
}
