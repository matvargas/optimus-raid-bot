using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class PaddockInformationsForSell : NetworkType
{

	public const uint Id = 222;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public String GuildOwner { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short WorldX { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short WorldY { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short SubAreaId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte NbMount { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte NbObject { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public long Price { get; set; }

	public PaddockInformationsForSell() {}


	public PaddockInformationsForSell InitPaddockInformationsForSell(String GuildOwner, short WorldX, short WorldY, short SubAreaId, byte NbMount, byte NbObject, long Price)
	{
		this.GuildOwner = GuildOwner;
		this.WorldX = WorldX;
		this.WorldY = WorldY;
		this.SubAreaId = SubAreaId;
		this.NbMount = NbMount;
		this.NbObject = NbObject;
		this.Price = Price;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteUTF(this.GuildOwner);
		writer.WriteShort(this.WorldX);
		writer.WriteShort(this.WorldY);
		writer.WriteVarShort(this.SubAreaId);
		writer.WriteByte(this.NbMount);
		writer.WriteByte(this.NbObject);
		writer.WriteVarLong(this.Price);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.GuildOwner = reader.ReadUTF();
		this.WorldX = reader.ReadShort();
		this.WorldY = reader.ReadShort();
		this.SubAreaId = reader.ReadVarShort();
		this.NbMount = reader.ReadByte();
		this.NbObject = reader.ReadByte();
		this.Price = reader.ReadVarLong();
	}
}
}
