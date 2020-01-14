using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class GameActionFightActivateGlyphTrapMessage : AbstractGameActionMessage
{

	public const uint Id = 6545;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short MarkId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool Active { get; set; }

	public GameActionFightActivateGlyphTrapMessage() {}


	public GameActionFightActivateGlyphTrapMessage InitGameActionFightActivateGlyphTrapMessage(short MarkId, bool Active)
	{
		this.MarkId = MarkId;
		this.Active = Active;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteShort(this.MarkId);
		writer.WriteBoolean(this.Active);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		this.MarkId = reader.ReadShort();
		this.Active = reader.ReadBoolean();
	}
}
}
