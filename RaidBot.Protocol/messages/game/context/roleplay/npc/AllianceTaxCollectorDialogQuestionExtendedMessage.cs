using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class AllianceTaxCollectorDialogQuestionExtendedMessage : TaxCollectorDialogQuestionExtendedMessage
{

	public const uint Id = 6445;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public BasicNamedAllianceInformations Alliance { get; set; }

	public AllianceTaxCollectorDialogQuestionExtendedMessage() {}


	public AllianceTaxCollectorDialogQuestionExtendedMessage InitAllianceTaxCollectorDialogQuestionExtendedMessage(BasicNamedAllianceInformations Alliance)
	{
		this.Alliance = Alliance;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		this.Alliance.Serialize(writer);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		this.Alliance = new BasicNamedAllianceInformations();
		this.Alliance.Deserialize(reader);
	}
}
}
