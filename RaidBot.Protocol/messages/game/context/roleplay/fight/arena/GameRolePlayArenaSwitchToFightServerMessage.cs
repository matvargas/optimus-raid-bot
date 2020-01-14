using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class GameRolePlayArenaSwitchToFightServerMessage : NetworkMessage
{

	public const uint Id = 6575;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public String Address { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short[] Ports { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte[] Ticket { get; set; }

	public GameRolePlayArenaSwitchToFightServerMessage() {}


	public GameRolePlayArenaSwitchToFightServerMessage InitGameRolePlayArenaSwitchToFightServerMessage(String Address, short[] Ports, byte[] Ticket)
	{
		this.Address = Address;
		this.Ports = Ports;
		this.Ticket = Ticket;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteUTF(this.Address);
		writer.WriteShort(this.Ports.Length);
		foreach (short item in this.Ports)
		{
			writer.WriteShort(item);
		}
		writer.WriteVarInt(this.Ticket.Length);
		foreach (byte item in this.Ticket)
		{
			writer.WriteByte(item);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.Address = reader.ReadUTF();
		int PortsLen = reader.ReadShort();
		Ports = new short[PortsLen];
		for (int i = 0; i < PortsLen; i++)
		{
			this.Ports[i] = reader.ReadShort();
		}
		int TicketLen = reader.ReadVarInt();
		Ticket = new byte[TicketLen];
		for (int i = 0; i < TicketLen; i++)
		{
			this.Ticket[i] = reader.ReadByte();
		}
	}
}
}
