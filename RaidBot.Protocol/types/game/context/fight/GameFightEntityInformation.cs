using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class GameFightEntityInformation : GameFightFighterInformations
{

	public const uint Id = 551;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte EntityModelId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short Level { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public double MasterId { get; set; }

	public GameFightEntityInformation() {}


	public GameFightEntityInformation InitGameFightEntityInformation(byte EntityModelId, short Level, double MasterId)
	{
		this.EntityModelId = EntityModelId;
		this.Level = Level;
		this.MasterId = MasterId;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteByte(this.EntityModelId);
		writer.WriteVarShort(this.Level);
		writer.WriteDouble(this.MasterId);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		this.EntityModelId = reader.ReadByte();
		this.Level = reader.ReadVarShort();
		this.MasterId = reader.ReadDouble();
	}
}
}
