using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class AllianceInsiderInfoMessage : NetworkMessage
{

	public const uint Id = 6403;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public AllianceFactSheetInformations AllianceInfos { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public GuildInsiderFactSheetInformations[] Guilds { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public PrismSubareaEmptyInfo[] Prisms { get; set; }

	public AllianceInsiderInfoMessage() {}


	public AllianceInsiderInfoMessage InitAllianceInsiderInfoMessage(AllianceFactSheetInformations AllianceInfos, GuildInsiderFactSheetInformations[] Guilds, PrismSubareaEmptyInfo[] Prisms)
	{
		this.AllianceInfos = AllianceInfos;
		this.Guilds = Guilds;
		this.Prisms = Prisms;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		this.AllianceInfos.Serialize(writer);
		writer.WriteShort(this.Guilds.Length);
		foreach (GuildInsiderFactSheetInformations item in this.Guilds)
		{
			item.Serialize(writer);
		}
		writer.WriteShort(this.Prisms.Length);
		foreach (PrismSubareaEmptyInfo item in this.Prisms)
		{
			writer.WriteShort(item.MessageId);
			item.Serialize(writer);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.AllianceInfos = new AllianceFactSheetInformations();
		this.AllianceInfos.Deserialize(reader);
		int GuildsLen = reader.ReadShort();
		Guilds = new GuildInsiderFactSheetInformations[GuildsLen];
		for (int i = 0; i < GuildsLen; i++)
		{
			this.Guilds[i] = new GuildInsiderFactSheetInformations();
			this.Guilds[i].Deserialize(reader);
		}
		int PrismsLen = reader.ReadShort();
		Prisms = new PrismSubareaEmptyInfo[PrismsLen];
		for (int i = 0; i < PrismsLen; i++)
		{
			this.Prisms[i] = ProtocolTypeManager.GetInstance<PrismSubareaEmptyInfo>(reader.ReadShort());
			this.Prisms[i].Deserialize(reader);
		}
	}
}
}
