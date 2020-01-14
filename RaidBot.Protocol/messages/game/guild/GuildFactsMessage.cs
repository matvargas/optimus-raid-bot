using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class GuildFactsMessage : NetworkMessage
{

	public const uint Id = 6415;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public GuildFactSheetInformations Infos { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int CreationDate { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short NbTaxCollectors { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public CharacterMinimalInformations[] Members { get; set; }

	public GuildFactsMessage() {}


	public GuildFactsMessage InitGuildFactsMessage(GuildFactSheetInformations Infos, int CreationDate, short NbTaxCollectors, CharacterMinimalInformations[] Members)
	{
		this.Infos = Infos;
		this.CreationDate = CreationDate;
		this.NbTaxCollectors = NbTaxCollectors;
		this.Members = Members;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
writer.WriteShort(Infos.MessageId);
		Infos.Serialize(writer);
		writer.WriteInt(this.CreationDate);
		writer.WriteVarShort(this.NbTaxCollectors);
		writer.WriteShort(this.Members.Length);
		foreach (CharacterMinimalInformations item in this.Members)
		{
			item.Serialize(writer);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
this.Infos = ProtocolTypeManager.GetInstance<GuildFactSheetInformations>(reader.ReadShort());
		this.Infos.Deserialize(reader);
		this.CreationDate = reader.ReadInt();
		this.NbTaxCollectors = reader.ReadVarShort();
		int MembersLen = reader.ReadShort();
		Members = new CharacterMinimalInformations[MembersLen];
		for (int i = 0; i < MembersLen; i++)
		{
			this.Members[i] = new CharacterMinimalInformations();
			this.Members[i].Deserialize(reader);
		}
	}
}
}
