using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class StartupActionFinishedMessage : NetworkMessage
{

	public const uint Id = 1304;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool Success { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool AutomaticAction { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int ActionId { get; set; }

	public StartupActionFinishedMessage() {}


	public StartupActionFinishedMessage InitStartupActionFinishedMessage(bool Success, bool AutomaticAction, int ActionId)
	{
		this.Success = Success;
		this.AutomaticAction = AutomaticAction;
		this.ActionId = ActionId;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		byte box = 0;
		box = BooleanByteWrapper.SetFlag(box, 0, Success);
		box = BooleanByteWrapper.SetFlag(box, 1, AutomaticAction);
		writer.WriteByte(box);
		writer.WriteInt(this.ActionId);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		byte box = reader.ReadByte();
		this.Success = BooleanByteWrapper.GetFlag(box, 0);
		this.AutomaticAction = BooleanByteWrapper.GetFlag(box, 1);
		this.ActionId = reader.ReadInt();
	}
}
}
