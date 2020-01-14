using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class PrismFightDefenderAddMessage : NetworkMessage
{

	public const uint Id = 5895;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short SubAreaId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short FightId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public CharacterMinimalPlusLookInformations Defender { get; set; }

	public PrismFightDefenderAddMessage() {}


	public PrismFightDefenderAddMessage InitPrismFightDefenderAddMessage(short SubAreaId, short FightId, CharacterMinimalPlusLookInformations Defender)
	{
		this.SubAreaId = SubAreaId;
		this.FightId = FightId;
		this.Defender = Defender;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteVarShort(this.SubAreaId);
		writer.WriteVarShort(this.FightId);
writer.WriteShort(Defender.MessageId);
		Defender.Serialize(writer);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.SubAreaId = reader.ReadVarShort();
		this.FightId = reader.ReadVarShort();
this.Defender = ProtocolTypeManager.GetInstance<CharacterMinimalPlusLookInformations>(reader.ReadShort());
		this.Defender.Deserialize(reader);
	}
}
}
