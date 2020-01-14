using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class PartyEntityMemberInformation : PartyEntityBaseInformation
{

	public const uint Id = 550;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short Initiative { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int LifePoints { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int MaxLifePoints { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short Prospecting { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte RegenRate { get; set; }

	public PartyEntityMemberInformation() {}


	public PartyEntityMemberInformation InitPartyEntityMemberInformation(short Initiative, int LifePoints, int MaxLifePoints, short Prospecting, byte RegenRate)
	{
		this.Initiative = Initiative;
		this.LifePoints = LifePoints;
		this.MaxLifePoints = MaxLifePoints;
		this.Prospecting = Prospecting;
		this.RegenRate = RegenRate;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteVarShort(this.Initiative);
		writer.WriteVarInt(this.LifePoints);
		writer.WriteVarInt(this.MaxLifePoints);
		writer.WriteVarShort(this.Prospecting);
		writer.WriteByte(this.RegenRate);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		this.Initiative = reader.ReadVarShort();
		this.LifePoints = reader.ReadVarInt();
		this.MaxLifePoints = reader.ReadVarInt();
		this.Prospecting = reader.ReadVarShort();
		this.RegenRate = reader.ReadByte();
	}
}
}
