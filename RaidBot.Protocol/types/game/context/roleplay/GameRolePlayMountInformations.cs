using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class GameRolePlayMountInformations : GameRolePlayNamedActorInformations
{

	public const uint Id = 180;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public String OwnerName { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte Level { get; set; }

	public GameRolePlayMountInformations() {}


	public GameRolePlayMountInformations InitGameRolePlayMountInformations(String OwnerName, byte Level)
	{
		this.OwnerName = OwnerName;
		this.Level = Level;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteUTF(this.OwnerName);
		writer.WriteByte(this.Level);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		this.OwnerName = reader.ReadUTF();
		this.Level = reader.ReadByte();
	}
}
}
