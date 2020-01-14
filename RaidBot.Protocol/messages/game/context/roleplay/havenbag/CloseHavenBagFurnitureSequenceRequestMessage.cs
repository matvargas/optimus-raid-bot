using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class CloseHavenBagFurnitureSequenceRequestMessage : NetworkMessage
{

	public const uint Id = 6621;
	public override uint MessageId { get { return Id; } }


	public CloseHavenBagFurnitureSequenceRequestMessage() {}

	public override void Serialize(ICustomDataWriter writer)
	{
	}

	public override void Deserialize(ICustomDataReader reader)
	{
	}
}
}
