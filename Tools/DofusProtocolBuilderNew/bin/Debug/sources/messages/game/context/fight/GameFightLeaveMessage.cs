using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class GameFightLeaveMessage : NetworkMessage
{

	public const uint Id = 721;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public double CharId { get; set; }

	public GameFightLeaveMessage() {}


	public GameFightLeaveMessage InitGameFightLeaveMessage(double CharId)
	{
		this.CharId = CharId;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteDouble(this.CharId);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.CharId = reader.ReadDouble();
	}
}
}
