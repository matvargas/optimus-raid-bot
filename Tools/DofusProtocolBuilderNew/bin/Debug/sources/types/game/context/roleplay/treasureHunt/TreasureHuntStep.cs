using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class TreasureHuntStep : NetworkType
{

	public const uint Id = 463;
	public override uint MessageId { get { return Id; } }


	public TreasureHuntStep() {}

	public override void Serialize(ICustomDataWriter writer)
	{
	}

	public override void Deserialize(ICustomDataReader reader)
	{
	}
}
}
