using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class GameActionFightDispellEffectMessage : GameActionFightDispellMessage
{

	public const uint Id = 6113;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int BoostUID { get; set; }

	public GameActionFightDispellEffectMessage() {}


	public GameActionFightDispellEffectMessage InitGameActionFightDispellEffectMessage(int BoostUID)
	{
		this.BoostUID = BoostUID;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteInt(this.BoostUID);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		this.BoostUID = reader.ReadInt();
	}
}
}
