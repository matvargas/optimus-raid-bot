using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class CharacterReplayWithRemodelRequestMessage : CharacterReplayRequestMessage
{

	public const uint Id = 6551;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public RemodelingInformation Remodel { get; set; }

	public CharacterReplayWithRemodelRequestMessage() {}


	public CharacterReplayWithRemodelRequestMessage InitCharacterReplayWithRemodelRequestMessage(RemodelingInformation Remodel)
	{
		this.Remodel = Remodel;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		this.Remodel.Serialize(writer);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		this.Remodel = new RemodelingInformation();
		this.Remodel.Deserialize(reader);
	}
}
}
