using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class GuildInsiderFactSheetInformations : GuildFactSheetInformations
{

	public const uint Id = 423;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public String LeaderName { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short NbConnectedMembers { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte NbTaxCollectors { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int LastActivity { get; set; }

	public GuildInsiderFactSheetInformations() {}


	public GuildInsiderFactSheetInformations InitGuildInsiderFactSheetInformations(String LeaderName, short NbConnectedMembers, byte NbTaxCollectors, int LastActivity)
	{
		this.LeaderName = LeaderName;
		this.NbConnectedMembers = NbConnectedMembers;
		this.NbTaxCollectors = NbTaxCollectors;
		this.LastActivity = LastActivity;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteUTF(this.LeaderName);
		writer.WriteVarShort(this.NbConnectedMembers);
		writer.WriteByte(this.NbTaxCollectors);
		writer.WriteInt(this.LastActivity);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		this.LeaderName = reader.ReadUTF();
		this.NbConnectedMembers = reader.ReadVarShort();
		this.NbTaxCollectors = reader.ReadByte();
		this.LastActivity = reader.ReadInt();
	}
}
}
