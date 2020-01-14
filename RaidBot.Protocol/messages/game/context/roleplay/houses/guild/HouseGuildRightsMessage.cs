using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class HouseGuildRightsMessage : NetworkMessage
{

	public const uint Id = 5703;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int HouseId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int InstanceId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool SecondHand { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public GuildInformations GuildInfo { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int Rights { get; set; }

	public HouseGuildRightsMessage() {}


	public HouseGuildRightsMessage InitHouseGuildRightsMessage(int HouseId, int InstanceId, bool SecondHand, GuildInformations GuildInfo, int Rights)
	{
		this.HouseId = HouseId;
		this.InstanceId = InstanceId;
		this.SecondHand = SecondHand;
		this.GuildInfo = GuildInfo;
		this.Rights = Rights;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteVarInt(this.HouseId);
		writer.WriteInt(this.InstanceId);
		writer.WriteBoolean(this.SecondHand);
		this.GuildInfo.Serialize(writer);
		writer.WriteVarInt(this.Rights);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.HouseId = reader.ReadVarInt();
		this.InstanceId = reader.ReadInt();
		this.SecondHand = reader.ReadBoolean();
		this.GuildInfo = new GuildInformations();
		this.GuildInfo.Deserialize(reader);
		this.Rights = reader.ReadVarInt();
	}
}
}
