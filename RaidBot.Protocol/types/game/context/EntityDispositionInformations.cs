using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class EntityDispositionInformations : NetworkType
{

	public const uint Id = 60;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short CellId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte Direction { get; set; }

	public EntityDispositionInformations() {}


	public EntityDispositionInformations InitEntityDispositionInformations(short CellId, byte Direction)
	{
		this.CellId = CellId;
		this.Direction = Direction;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteShort(this.CellId);
		writer.WriteByte(this.Direction);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.CellId = reader.ReadShort();
		this.Direction = reader.ReadByte();
	}
}
}
