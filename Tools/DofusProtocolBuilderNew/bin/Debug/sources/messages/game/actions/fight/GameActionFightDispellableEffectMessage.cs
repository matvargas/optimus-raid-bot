using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class GameActionFightDispellableEffectMessage : AbstractGameActionMessage
{

	public const uint Id = 6070;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public AbstractFightDispellableEffect Effect { get; set; }

	public GameActionFightDispellableEffectMessage() {}


	public GameActionFightDispellableEffectMessage InitGameActionFightDispellableEffectMessage(AbstractFightDispellableEffect Effect)
	{
		this.Effect = Effect;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
writer.WriteShort(Effect.MessageId);
		Effect.Serialize(writer);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
this.Effect = ProtocolTypeManager.GetInstance<AbstractFightDispellableEffect>(reader.ReadShort());
		this.Effect.Deserialize(reader);
	}
}
}
