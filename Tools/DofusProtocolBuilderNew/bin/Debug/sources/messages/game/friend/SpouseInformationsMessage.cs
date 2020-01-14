using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class SpouseInformationsMessage : NetworkMessage
{

	public const uint Id = 6356;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public FriendSpouseInformations Spouse { get; set; }

	public SpouseInformationsMessage() {}


	public SpouseInformationsMessage InitSpouseInformationsMessage(FriendSpouseInformations Spouse)
	{
		this.Spouse = Spouse;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
writer.WriteShort(Spouse.MessageId);
		Spouse.Serialize(writer);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
this.Spouse = ProtocolTypeManager.GetInstance<FriendSpouseInformations>(reader.ReadShort());
		this.Spouse.Deserialize(reader);
	}
}
}
