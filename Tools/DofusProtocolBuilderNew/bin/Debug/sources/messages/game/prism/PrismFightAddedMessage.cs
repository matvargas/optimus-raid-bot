using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class PrismFightAddedMessage : NetworkMessage
{

	public const uint Id = 6452;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public PrismFightersInformation Fight { get; set; }

	public PrismFightAddedMessage() {}


	public PrismFightAddedMessage InitPrismFightAddedMessage(PrismFightersInformation Fight)
	{
		this.Fight = Fight;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		this.Fight.Serialize(writer);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.Fight = new PrismFightersInformation();
		this.Fight.Deserialize(reader);
	}
}
}
