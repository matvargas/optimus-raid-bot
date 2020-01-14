using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class PrismInfoJoinLeaveRequestMessage : NetworkMessage
{

	public const uint Id = 5844;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool Join { get; set; }

	public PrismInfoJoinLeaveRequestMessage() {}


	public PrismInfoJoinLeaveRequestMessage InitPrismInfoJoinLeaveRequestMessage(bool Join)
	{
		this.Join = Join;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteBoolean(this.Join);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.Join = reader.ReadBoolean();
	}
}
}
