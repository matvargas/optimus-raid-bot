using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;

namespace RaidBot.Protocol.Messages
{
public class CharacterCreationRequestMessage : NetworkMessage
{

	public const uint Id = 160;
	public override uint MessageId { get { return Id; } }

	public String Name { get; set; }
	public byte Breed { get; set; }
	public bool Sex { get; set; }
	public short CosmeticId { get; set; }

	public CharacterCreationRequestMessage() {}


	public CharacterCreationRequestMessage InitCharacterCreationRequestMessage(String Name, byte Breed, bool Sex, short CosmeticId)
	{
		this.Name = Name;
		this.Breed = Breed;
		this.Sex = Sex;
		this.CosmeticId = CosmeticId;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteUTF(this.Name);
		writer.WriteByte(this.Breed);
		writer.WriteBoolean(this.Sex);
		writer.WriteVarShort(this.CosmeticId);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.Name = reader.ReadUTF();
		this.Breed = reader.ReadByte();
		this.Sex = reader.ReadBoolean();
		this.CosmeticId = reader.ReadVarShort();
	}
}
}
