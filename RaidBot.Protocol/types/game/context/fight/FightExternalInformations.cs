using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class FightExternalInformations : NetworkType
{

	public const uint Id = 117;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short FightId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte FightType { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int FightStart { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool FightSpectatorLocked { get; set; }

	public FightExternalInformations() {}


	public FightExternalInformations InitFightExternalInformations(short FightId, byte FightType, int FightStart, bool FightSpectatorLocked)
	{
		this.FightId = FightId;
		this.FightType = FightType;
		this.FightStart = FightStart;
		this.FightSpectatorLocked = FightSpectatorLocked;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteVarShort(this.FightId);
		writer.WriteByte(this.FightType);
		writer.WriteInt(this.FightStart);
		writer.WriteBoolean(this.FightSpectatorLocked);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.FightId = reader.ReadVarShort();
		this.FightType = reader.ReadByte();
		this.FightStart = reader.ReadInt();
		this.FightSpectatorLocked = reader.ReadBoolean();
	}
}
}
