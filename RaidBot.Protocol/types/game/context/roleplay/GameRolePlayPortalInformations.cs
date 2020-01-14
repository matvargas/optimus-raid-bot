using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class GameRolePlayPortalInformations : GameRolePlayActorInformations
{

	public const uint Id = 467;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public PortalInformation Portal { get; set; }

	public GameRolePlayPortalInformations() {}


	public GameRolePlayPortalInformations InitGameRolePlayPortalInformations(PortalInformation Portal)
	{
		this.Portal = Portal;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
writer.WriteShort(Portal.MessageId);
		Portal.Serialize(writer);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
this.Portal = ProtocolTypeManager.GetInstance<PortalInformation>(reader.ReadShort());
		this.Portal.Deserialize(reader);
	}
}
}
