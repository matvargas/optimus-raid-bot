using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class GameFightFighterNamedInformations : GameFightFighterInformations
{

	public const uint Id = 158;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public String Name { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public PlayerStatus Status { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short LeagueId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int LadderPosition { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool HiddenInPrefight { get; set; }

	public GameFightFighterNamedInformations() {}


	public GameFightFighterNamedInformations InitGameFightFighterNamedInformations(String Name, PlayerStatus Status, short LeagueId, int LadderPosition, bool HiddenInPrefight)
	{
		this.Name = Name;
		this.Status = Status;
		this.LeagueId = LeagueId;
		this.LadderPosition = LadderPosition;
		this.HiddenInPrefight = HiddenInPrefight;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteUTF(this.Name);
		this.Status.Serialize(writer);
		writer.WriteVarShort(this.LeagueId);
		writer.WriteInt(this.LadderPosition);
		writer.WriteBoolean(this.HiddenInPrefight);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		this.Name = reader.ReadUTF();
		this.Status = new PlayerStatus();
		this.Status.Deserialize(reader);
		this.LeagueId = reader.ReadVarShort();
		this.LadderPosition = reader.ReadInt();
		this.HiddenInPrefight = reader.ReadBoolean();
	}
}
}
