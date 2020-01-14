using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class GameFightNewWaveMessage : NetworkMessage
{

	public const uint Id = 6490;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte Id_ { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte TeamId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short NbTurnBeforeNextWave { get; set; }

	public GameFightNewWaveMessage() {}


	public GameFightNewWaveMessage InitGameFightNewWaveMessage(byte Id_, byte TeamId, short NbTurnBeforeNextWave)
	{
		this.Id_ = Id_;
		this.TeamId = TeamId;
		this.NbTurnBeforeNextWave = NbTurnBeforeNextWave;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteByte(this.Id_);
		writer.WriteByte(this.TeamId);
		writer.WriteShort(this.NbTurnBeforeNextWave);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.Id_ = reader.ReadByte();
		this.TeamId = reader.ReadByte();
		this.NbTurnBeforeNextWave = reader.ReadShort();
	}
}
}
