using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class LockableStateUpdateAbstractMessage : NetworkMessage
{

	public const uint Id = 5671;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool Locked { get; set; }

	public LockableStateUpdateAbstractMessage() {}


	public LockableStateUpdateAbstractMessage InitLockableStateUpdateAbstractMessage(bool Locked)
	{
		this.Locked = Locked;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteBoolean(this.Locked);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.Locked = reader.ReadBoolean();
	}
}
}
