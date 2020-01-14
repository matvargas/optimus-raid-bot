using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class BasicWhoIsMessage : NetworkMessage
{

	public const uint Id = 180;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool Self { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool Verbose { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte Position { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public String AccountNickname { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int AccountId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public String PlayerName { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public long PlayerId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short AreaId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short ServerId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short OriginServerId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public AbstractSocialGroupInfos[] SocialGroups { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte PlayerState { get; set; }

	public BasicWhoIsMessage() {}


	public BasicWhoIsMessage InitBasicWhoIsMessage(bool Self, bool Verbose, byte Position, String AccountNickname, int AccountId, String PlayerName, long PlayerId, short AreaId, short ServerId, short OriginServerId, AbstractSocialGroupInfos[] SocialGroups, byte PlayerState)
	{
		this.Self = Self;
		this.Verbose = Verbose;
		this.Position = Position;
		this.AccountNickname = AccountNickname;
		this.AccountId = AccountId;
		this.PlayerName = PlayerName;
		this.PlayerId = PlayerId;
		this.AreaId = AreaId;
		this.ServerId = ServerId;
		this.OriginServerId = OriginServerId;
		this.SocialGroups = SocialGroups;
		this.PlayerState = PlayerState;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		byte box = 0;
		box = BooleanByteWrapper.SetFlag(box, 0, Self);
		box = BooleanByteWrapper.SetFlag(box, 1, Verbose);
		writer.WriteByte(box);
		writer.WriteByte(this.Position);
		writer.WriteUTF(this.AccountNickname);
		writer.WriteInt(this.AccountId);
		writer.WriteUTF(this.PlayerName);
		writer.WriteVarLong(this.PlayerId);
		writer.WriteShort(this.AreaId);
		writer.WriteShort(this.ServerId);
		writer.WriteShort(this.OriginServerId);
		writer.WriteShort(this.SocialGroups.Length);
		foreach (AbstractSocialGroupInfos item in this.SocialGroups)
		{
			writer.WriteShort(item.MessageId);
			item.Serialize(writer);
		}
		writer.WriteByte(this.PlayerState);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		byte box = reader.ReadByte();
		this.Self = BooleanByteWrapper.GetFlag(box, 0);
		this.Verbose = BooleanByteWrapper.GetFlag(box, 1);
		this.Position = reader.ReadByte();
		this.AccountNickname = reader.ReadUTF();
		this.AccountId = reader.ReadInt();
		this.PlayerName = reader.ReadUTF();
		this.PlayerId = reader.ReadVarLong();
		this.AreaId = reader.ReadShort();
		this.ServerId = reader.ReadShort();
		this.OriginServerId = reader.ReadShort();
		int SocialGroupsLen = reader.ReadShort();
		SocialGroups = new AbstractSocialGroupInfos[SocialGroupsLen];
		for (int i = 0; i < SocialGroupsLen; i++)
		{
			this.SocialGroups[i] = ProtocolTypeManager.GetInstance<AbstractSocialGroupInfos>(reader.ReadShort());
			this.SocialGroups[i].Deserialize(reader);
		}
		this.PlayerState = reader.ReadByte();
	}
}
}
