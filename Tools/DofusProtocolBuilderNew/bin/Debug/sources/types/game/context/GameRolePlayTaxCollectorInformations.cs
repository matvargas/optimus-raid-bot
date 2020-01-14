using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class GameRolePlayTaxCollectorInformations : GameRolePlayActorInformations
{

	public const uint Id = 148;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public TaxCollectorStaticInformations Identification { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte GuildLevel { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int TaxCollectorAttack { get; set; }

	public GameRolePlayTaxCollectorInformations() {}


	public GameRolePlayTaxCollectorInformations InitGameRolePlayTaxCollectorInformations(TaxCollectorStaticInformations Identification, byte GuildLevel, int TaxCollectorAttack)
	{
		this.Identification = Identification;
		this.GuildLevel = GuildLevel;
		this.TaxCollectorAttack = TaxCollectorAttack;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
writer.WriteShort(Identification.MessageId);
		Identification.Serialize(writer);
		writer.WriteByte(this.GuildLevel);
		writer.WriteInt(this.TaxCollectorAttack);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
this.Identification = ProtocolTypeManager.GetInstance<TaxCollectorStaticInformations>(reader.ReadShort());
		this.Identification.Deserialize(reader);
		this.GuildLevel = reader.ReadByte();
		this.TaxCollectorAttack = reader.ReadInt();
	}
}
}
