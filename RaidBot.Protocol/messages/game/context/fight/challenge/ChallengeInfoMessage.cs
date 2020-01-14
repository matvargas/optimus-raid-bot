using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class ChallengeInfoMessage : NetworkMessage
{

	public const uint Id = 6022;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short ChallengeId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public double TargetId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int XpBonus { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int DropBonus { get; set; }

	public ChallengeInfoMessage() {}


	public ChallengeInfoMessage InitChallengeInfoMessage(short ChallengeId, double TargetId, int XpBonus, int DropBonus)
	{
		this.ChallengeId = ChallengeId;
		this.TargetId = TargetId;
		this.XpBonus = XpBonus;
		this.DropBonus = DropBonus;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteVarShort(this.ChallengeId);
		writer.WriteDouble(this.TargetId);
		writer.WriteVarInt(this.XpBonus);
		writer.WriteVarInt(this.DropBonus);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.ChallengeId = reader.ReadVarShort();
		this.TargetId = reader.ReadDouble();
		this.XpBonus = reader.ReadVarInt();
		this.DropBonus = reader.ReadVarInt();
	}
}
}
