using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class ServerSettingsMessage : NetworkMessage
{

	public const uint Id = 6340;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool IsMonoAccount { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool HasFreeAutopilot { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public String Lang { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte Community { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte GameType { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short ArenaLeaveBanTime { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int ItemMaxLevel { get; set; }

	public ServerSettingsMessage() {}


	public ServerSettingsMessage InitServerSettingsMessage(bool IsMonoAccount, bool HasFreeAutopilot, String Lang, byte Community, byte GameType, short ArenaLeaveBanTime, int ItemMaxLevel)
	{
		this.IsMonoAccount = IsMonoAccount;
		this.HasFreeAutopilot = HasFreeAutopilot;
		this.Lang = Lang;
		this.Community = Community;
		this.GameType = GameType;
		this.ArenaLeaveBanTime = ArenaLeaveBanTime;
		this.ItemMaxLevel = ItemMaxLevel;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		byte box = 0;
		box = BooleanByteWrapper.SetFlag(box, 0, IsMonoAccount);
		box = BooleanByteWrapper.SetFlag(box, 1, HasFreeAutopilot);
		writer.WriteByte(box);
		writer.WriteUTF(this.Lang);
		writer.WriteByte(this.Community);
		writer.WriteByte(this.GameType);
		writer.WriteVarShort(this.ArenaLeaveBanTime);
		writer.WriteInt(this.ItemMaxLevel);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		byte box = reader.ReadByte();
		this.IsMonoAccount = BooleanByteWrapper.GetFlag(box, 0);
		this.HasFreeAutopilot = BooleanByteWrapper.GetFlag(box, 1);
		this.Lang = reader.ReadUTF();
		this.Community = reader.ReadByte();
		this.GameType = reader.ReadByte();
		this.ArenaLeaveBanTime = reader.ReadVarShort();
		this.ItemMaxLevel = reader.ReadInt();
	}
}
}
