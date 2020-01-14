using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class SymbioticObjectAssociateRequestMessage : NetworkMessage
{

	public const uint Id = 6522;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int SymbioteUID { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte SymbiotePos { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int HostUID { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte HostPos { get; set; }

	public SymbioticObjectAssociateRequestMessage() {}


	public SymbioticObjectAssociateRequestMessage InitSymbioticObjectAssociateRequestMessage(int SymbioteUID, byte SymbiotePos, int HostUID, byte HostPos)
	{
		this.SymbioteUID = SymbioteUID;
		this.SymbiotePos = SymbiotePos;
		this.HostUID = HostUID;
		this.HostPos = HostPos;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteVarInt(this.SymbioteUID);
		writer.WriteByte(this.SymbiotePos);
		writer.WriteVarInt(this.HostUID);
		writer.WriteByte(this.HostPos);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.SymbioteUID = reader.ReadVarInt();
		this.SymbiotePos = reader.ReadByte();
		this.HostUID = reader.ReadVarInt();
		this.HostPos = reader.ReadByte();
	}
}
}
