using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class LockableShowCodeDialogMessage : NetworkMessage
{

	public const uint Id = 5740;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool ChangeOrUse { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte CodeSize { get; set; }

	public LockableShowCodeDialogMessage() {}


	public LockableShowCodeDialogMessage InitLockableShowCodeDialogMessage(bool ChangeOrUse, byte CodeSize)
	{
		this.ChangeOrUse = ChangeOrUse;
		this.CodeSize = CodeSize;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteBoolean(this.ChangeOrUse);
		writer.WriteByte(this.CodeSize);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.ChangeOrUse = reader.ReadBoolean();
		this.CodeSize = reader.ReadByte();
	}
}
}
