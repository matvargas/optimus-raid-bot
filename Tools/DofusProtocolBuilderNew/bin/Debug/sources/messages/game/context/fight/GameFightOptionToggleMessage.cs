using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class GameFightOptionToggleMessage : NetworkMessage
{

	public const uint Id = 707;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte Option { get; set; }

	public GameFightOptionToggleMessage() {}


	public GameFightOptionToggleMessage InitGameFightOptionToggleMessage(byte Option)
	{
		this.Option = Option;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteByte(this.Option);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.Option = reader.ReadByte();
	}
}
}
