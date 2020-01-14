using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class GameFightFighterNamedLightInformations : GameFightFighterLightInformations
{

	public const uint Id = 456;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public String Name { get; set; }

	public GameFightFighterNamedLightInformations() {}


	public GameFightFighterNamedLightInformations InitGameFightFighterNamedLightInformations(String Name)
	{
		this.Name = Name;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteUTF(this.Name);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		this.Name = reader.ReadUTF();
	}
}
}
