using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class EnabledChannelsMessage : NetworkMessage
{

	public const uint Id = 892;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte[] Channels { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte[] Disallowed { get; set; }

	public EnabledChannelsMessage() {}


	public EnabledChannelsMessage InitEnabledChannelsMessage(byte[] Channels, byte[] Disallowed)
	{
		this.Channels = Channels;
		this.Disallowed = Disallowed;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteShort(this.Channels.Length);
		foreach (byte item in this.Channels)
		{
			writer.WriteByte(item);
		}
		writer.WriteShort(this.Disallowed.Length);
		foreach (byte item in this.Disallowed)
		{
			writer.WriteByte(item);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		int ChannelsLen = reader.ReadShort();
		Channels = new byte[ChannelsLen];
		for (int i = 0; i < ChannelsLen; i++)
		{
			this.Channels[i] = reader.ReadByte();
		}
		int DisallowedLen = reader.ReadShort();
		Disallowed = new byte[DisallowedLen];
		for (int i = 0; i < DisallowedLen; i++)
		{
			this.Disallowed[i] = reader.ReadByte();
		}
	}
}
}
