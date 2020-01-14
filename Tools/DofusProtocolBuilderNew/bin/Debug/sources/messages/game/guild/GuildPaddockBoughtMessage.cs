using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class GuildPaddockBoughtMessage : NetworkMessage
{

	public const uint Id = 5952;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public PaddockContentInformations PaddockInfo { get; set; }

	public GuildPaddockBoughtMessage() {}


	public GuildPaddockBoughtMessage InitGuildPaddockBoughtMessage(PaddockContentInformations PaddockInfo)
	{
		this.PaddockInfo = PaddockInfo;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		this.PaddockInfo.Serialize(writer);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.PaddockInfo = new PaddockContentInformations();
		this.PaddockInfo.Deserialize(reader);
	}
}
}
