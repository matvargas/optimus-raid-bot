using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class GameRolePlayPrismInformations : GameRolePlayActorInformations
{

	public const uint Id = 161;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public PrismInformation Prism { get; set; }

	public GameRolePlayPrismInformations() {}


	public GameRolePlayPrismInformations InitGameRolePlayPrismInformations(PrismInformation Prism)
	{
		this.Prism = Prism;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
writer.WriteShort(Prism.MessageId);
		Prism.Serialize(writer);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
this.Prism = ProtocolTypeManager.GetInstance<PrismInformation>(reader.ReadShort());
		this.Prism.Deserialize(reader);
	}
}
}
