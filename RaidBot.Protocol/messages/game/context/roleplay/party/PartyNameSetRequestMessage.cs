using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class PartyNameSetRequestMessage : AbstractPartyMessage
{

	public const uint Id = 6503;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public String PartyName { get; set; }

	public PartyNameSetRequestMessage() {}


	public PartyNameSetRequestMessage InitPartyNameSetRequestMessage(String PartyName)
	{
		this.PartyName = PartyName;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteUTF(this.PartyName);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		this.PartyName = reader.ReadUTF();
	}
}
}
