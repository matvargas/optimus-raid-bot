using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class PrismFightJoinLeaveRequestMessage : NetworkMessage
{

	public const uint Id = 5843;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short SubAreaId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool Join { get; set; }

	public PrismFightJoinLeaveRequestMessage() {}


	public PrismFightJoinLeaveRequestMessage InitPrismFightJoinLeaveRequestMessage(short SubAreaId, bool Join)
	{
		this.SubAreaId = SubAreaId;
		this.Join = Join;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteVarShort(this.SubAreaId);
		writer.WriteBoolean(this.Join);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.SubAreaId = reader.ReadVarShort();
		this.Join = reader.ReadBoolean();
	}
}
}
