using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class MountRenameRequestMessage : NetworkMessage
{

	public const uint Id = 5987;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public String Name { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int MountId { get; set; }

	public MountRenameRequestMessage() {}


	public MountRenameRequestMessage InitMountRenameRequestMessage(String Name, int MountId)
	{
		this.Name = Name;
		this.MountId = MountId;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteUTF(this.Name);
		writer.WriteVarInt(this.MountId);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.Name = reader.ReadUTF();
		this.MountId = reader.ReadVarInt();
	}
}
}
