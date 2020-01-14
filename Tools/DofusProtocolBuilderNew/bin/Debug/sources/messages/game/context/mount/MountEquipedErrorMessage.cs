using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class MountEquipedErrorMessage : NetworkMessage
{

	public const uint Id = 5963;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte ErrorType { get; set; }

	public MountEquipedErrorMessage() {}


	public MountEquipedErrorMessage InitMountEquipedErrorMessage(byte ErrorType)
	{
		this.ErrorType = ErrorType;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteByte(this.ErrorType);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.ErrorType = reader.ReadByte();
	}
}
}
