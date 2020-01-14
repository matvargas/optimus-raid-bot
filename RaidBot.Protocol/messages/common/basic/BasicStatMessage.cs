using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class BasicStatMessage : NetworkMessage
{

	public const uint Id = 6530;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public double TimeSpent { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short StatId { get; set; }

	public BasicStatMessage() {}


	public BasicStatMessage InitBasicStatMessage(double TimeSpent, short StatId)
	{
		this.TimeSpent = TimeSpent;
		this.StatId = StatId;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteDouble(this.TimeSpent);
		writer.WriteVarShort(this.StatId);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.TimeSpent = reader.ReadDouble();
		this.StatId = reader.ReadVarShort();
	}
}
}
