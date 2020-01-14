using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class GameRolePlayDelayedObjectUseMessage : GameRolePlayDelayedActionMessage
{

	public const uint Id = 6425;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short ObjectGID { get; set; }

	public GameRolePlayDelayedObjectUseMessage() {}


	public GameRolePlayDelayedObjectUseMessage InitGameRolePlayDelayedObjectUseMessage(short ObjectGID)
	{
		this.ObjectGID = ObjectGID;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteVarShort(this.ObjectGID);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		this.ObjectGID = reader.ReadVarShort();
	}
}
}
