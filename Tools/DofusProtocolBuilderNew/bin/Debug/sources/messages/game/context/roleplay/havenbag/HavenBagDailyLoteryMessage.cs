using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class HavenBagDailyLoteryMessage : NetworkMessage
{

	public const uint Id = 6644;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte ReturnType { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public String GameActionId { get; set; }

	public HavenBagDailyLoteryMessage() {}


	public HavenBagDailyLoteryMessage InitHavenBagDailyLoteryMessage(byte ReturnType, String GameActionId)
	{
		this.ReturnType = ReturnType;
		this.GameActionId = GameActionId;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteByte(this.ReturnType);
		writer.WriteUTF(this.GameActionId);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.ReturnType = reader.ReadByte();
		this.GameActionId = reader.ReadUTF();
	}
}
}
