using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class MapComplementaryInformationsDataInHavenBagMessage : MapComplementaryInformationsDataMessage
{

	public const uint Id = 6622;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public CharacterMinimalInformations OwnerInformations { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte Theme { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte RoomId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte MaxRoomId { get; set; }

	public MapComplementaryInformationsDataInHavenBagMessage() {}


	public MapComplementaryInformationsDataInHavenBagMessage InitMapComplementaryInformationsDataInHavenBagMessage(CharacterMinimalInformations OwnerInformations, byte Theme, byte RoomId, byte MaxRoomId)
	{
		this.OwnerInformations = OwnerInformations;
		this.Theme = Theme;
		this.RoomId = RoomId;
		this.MaxRoomId = MaxRoomId;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		this.OwnerInformations.Serialize(writer);
		writer.WriteByte(this.Theme);
		writer.WriteByte(this.RoomId);
		writer.WriteByte(this.MaxRoomId);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		this.OwnerInformations = new CharacterMinimalInformations();
		this.OwnerInformations.Deserialize(reader);
		this.Theme = reader.ReadByte();
		this.RoomId = reader.ReadByte();
		this.MaxRoomId = reader.ReadByte();
	}
}
}
