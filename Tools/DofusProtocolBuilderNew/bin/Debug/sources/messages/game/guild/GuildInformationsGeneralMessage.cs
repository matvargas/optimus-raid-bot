using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class GuildInformationsGeneralMessage : NetworkMessage
{

	public const uint Id = 5557;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool AbandonnedPaddock { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte Level { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public long ExpLevelFloor { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public long Experience { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public long ExpNextLevelFloor { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int CreationDate { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short NbTotalMembers { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short NbConnectedMembers { get; set; }

	public GuildInformationsGeneralMessage() {}


	public GuildInformationsGeneralMessage InitGuildInformationsGeneralMessage(bool AbandonnedPaddock, byte Level, long ExpLevelFloor, long Experience, long ExpNextLevelFloor, int CreationDate, short NbTotalMembers, short NbConnectedMembers)
	{
		this.AbandonnedPaddock = AbandonnedPaddock;
		this.Level = Level;
		this.ExpLevelFloor = ExpLevelFloor;
		this.Experience = Experience;
		this.ExpNextLevelFloor = ExpNextLevelFloor;
		this.CreationDate = CreationDate;
		this.NbTotalMembers = NbTotalMembers;
		this.NbConnectedMembers = NbConnectedMembers;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteBoolean(this.AbandonnedPaddock);
		writer.WriteByte(this.Level);
		writer.WriteVarLong(this.ExpLevelFloor);
		writer.WriteVarLong(this.Experience);
		writer.WriteVarLong(this.ExpNextLevelFloor);
		writer.WriteInt(this.CreationDate);
		writer.WriteVarShort(this.NbTotalMembers);
		writer.WriteVarShort(this.NbConnectedMembers);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.AbandonnedPaddock = reader.ReadBoolean();
		this.Level = reader.ReadByte();
		this.ExpLevelFloor = reader.ReadVarLong();
		this.Experience = reader.ReadVarLong();
		this.ExpNextLevelFloor = reader.ReadVarLong();
		this.CreationDate = reader.ReadInt();
		this.NbTotalMembers = reader.ReadVarShort();
		this.NbConnectedMembers = reader.ReadVarShort();
	}
}
}
