using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class AllianceFactsMessage : NetworkMessage
{

	public const uint Id = 6414;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public AllianceFactSheetInformations Infos { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public GuildInAllianceInformations[] Guilds { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short[] ControlledSubareaIds { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public long LeaderCharacterId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public String LeaderCharacterName { get; set; }

	public AllianceFactsMessage() {}


	public AllianceFactsMessage InitAllianceFactsMessage(AllianceFactSheetInformations Infos, GuildInAllianceInformations[] Guilds, short[] ControlledSubareaIds, long LeaderCharacterId, String LeaderCharacterName)
	{
		this.Infos = Infos;
		this.Guilds = Guilds;
		this.ControlledSubareaIds = ControlledSubareaIds;
		this.LeaderCharacterId = LeaderCharacterId;
		this.LeaderCharacterName = LeaderCharacterName;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
writer.WriteShort(Infos.MessageId);
		Infos.Serialize(writer);
		writer.WriteShort(this.Guilds.Length);
		foreach (GuildInAllianceInformations item in this.Guilds)
		{
			item.Serialize(writer);
		}
		writer.WriteShort(this.ControlledSubareaIds.Length);
		foreach (short item in this.ControlledSubareaIds)
		{
			writer.WriteVarShort(item);
		}
		writer.WriteVarLong(this.LeaderCharacterId);
		writer.WriteUTF(this.LeaderCharacterName);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
this.Infos = ProtocolTypeManager.GetInstance<AllianceFactSheetInformations>(reader.ReadShort());
		this.Infos.Deserialize(reader);
		int GuildsLen = reader.ReadShort();
		Guilds = new GuildInAllianceInformations[GuildsLen];
		for (int i = 0; i < GuildsLen; i++)
		{
			this.Guilds[i] = new GuildInAllianceInformations();
			this.Guilds[i].Deserialize(reader);
		}
		int ControlledSubareaIdsLen = reader.ReadShort();
		ControlledSubareaIds = new short[ControlledSubareaIdsLen];
		for (int i = 0; i < ControlledSubareaIdsLen; i++)
		{
			this.ControlledSubareaIds[i] = reader.ReadVarShort();
		}
		this.LeaderCharacterId = reader.ReadVarLong();
		this.LeaderCharacterName = reader.ReadUTF();
	}
}
}
