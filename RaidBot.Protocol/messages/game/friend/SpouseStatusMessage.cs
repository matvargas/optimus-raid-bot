using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class SpouseStatusMessage : NetworkMessage
{

	public const uint Id = 6265;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool HasSpouse { get; set; }

	public SpouseStatusMessage() {}


	public SpouseStatusMessage InitSpouseStatusMessage(bool HasSpouse)
	{
		this.HasSpouse = HasSpouse;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteBoolean(this.HasSpouse);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.HasSpouse = reader.ReadBoolean();
	}
}
}
