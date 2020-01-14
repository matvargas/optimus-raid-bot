using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class PlayerStatus : NetworkType
{

	public const uint Id = 415;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte StatusId { get; set; }

	public PlayerStatus() {}


	public PlayerStatus InitPlayerStatus(byte StatusId)
	{
		this.StatusId = StatusId;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteByte(this.StatusId);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.StatusId = reader.ReadByte();
	}
}
}
