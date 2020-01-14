using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class TaxCollectorDialogQuestionBasicMessage : NetworkMessage
{

	public const uint Id = 5619;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public BasicGuildInformations GuildInfo { get; set; }

	public TaxCollectorDialogQuestionBasicMessage() {}


	public TaxCollectorDialogQuestionBasicMessage InitTaxCollectorDialogQuestionBasicMessage(BasicGuildInformations GuildInfo)
	{
		this.GuildInfo = GuildInfo;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		this.GuildInfo.Serialize(writer);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.GuildInfo = new BasicGuildInformations();
		this.GuildInfo.Deserialize(reader);
	}
}
}
