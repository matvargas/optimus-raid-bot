using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class GameRolePlayPlayerFightFriendlyAnsweredMessage : NetworkMessage
{

	public const uint Id = 5733;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short FightId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public long SourceId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public long TargetId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool Accept { get; set; }

	public GameRolePlayPlayerFightFriendlyAnsweredMessage() {}


	public GameRolePlayPlayerFightFriendlyAnsweredMessage InitGameRolePlayPlayerFightFriendlyAnsweredMessage(short FightId, long SourceId, long TargetId, bool Accept)
	{
		this.FightId = FightId;
		this.SourceId = SourceId;
		this.TargetId = TargetId;
		this.Accept = Accept;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteVarShort(this.FightId);
		writer.WriteVarLong(this.SourceId);
		writer.WriteVarLong(this.TargetId);
		writer.WriteBoolean(this.Accept);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.FightId = reader.ReadVarShort();
		this.SourceId = reader.ReadVarLong();
		this.TargetId = reader.ReadVarLong();
		this.Accept = reader.ReadBoolean();
	}
}
}
