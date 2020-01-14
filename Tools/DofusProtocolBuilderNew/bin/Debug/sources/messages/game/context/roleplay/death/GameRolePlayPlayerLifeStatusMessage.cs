using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class GameRolePlayPlayerLifeStatusMessage : NetworkMessage
{

	public const uint Id = 5996;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte State { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public double PhenixMapId { get; set; }

	public GameRolePlayPlayerLifeStatusMessage() {}


	public GameRolePlayPlayerLifeStatusMessage InitGameRolePlayPlayerLifeStatusMessage(byte State, double PhenixMapId)
	{
		this.State = State;
		this.PhenixMapId = PhenixMapId;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteByte(this.State);
		writer.WriteDouble(this.PhenixMapId);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.State = reader.ReadByte();
		this.PhenixMapId = reader.ReadDouble();
	}
}
}
