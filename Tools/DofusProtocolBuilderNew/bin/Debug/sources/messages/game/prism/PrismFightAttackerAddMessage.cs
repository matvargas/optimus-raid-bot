using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class PrismFightAttackerAddMessage : NetworkMessage
{

	public const uint Id = 5893;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short SubAreaId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short FightId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public CharacterMinimalPlusLookInformations Attacker { get; set; }

	public PrismFightAttackerAddMessage() {}


	public PrismFightAttackerAddMessage InitPrismFightAttackerAddMessage(short SubAreaId, short FightId, CharacterMinimalPlusLookInformations Attacker)
	{
		this.SubAreaId = SubAreaId;
		this.FightId = FightId;
		this.Attacker = Attacker;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteVarShort(this.SubAreaId);
		writer.WriteVarShort(this.FightId);
writer.WriteShort(Attacker.MessageId);
		Attacker.Serialize(writer);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.SubAreaId = reader.ReadVarShort();
		this.FightId = reader.ReadVarShort();
this.Attacker = ProtocolTypeManager.GetInstance<CharacterMinimalPlusLookInformations>(reader.ReadShort());
		this.Attacker.Deserialize(reader);
	}
}
}
