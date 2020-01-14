using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class StatedElement : NetworkType
{

	public const uint Id = 108;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int ElementId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short ElementCellId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int ElementState { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool OnCurrentMap { get; set; }

	public StatedElement() {}


	public StatedElement InitStatedElement(int ElementId, short ElementCellId, int ElementState, bool OnCurrentMap)
	{
		this.ElementId = ElementId;
		this.ElementCellId = ElementCellId;
		this.ElementState = ElementState;
		this.OnCurrentMap = OnCurrentMap;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteInt(this.ElementId);
		writer.WriteVarShort(this.ElementCellId);
		writer.WriteVarInt(this.ElementState);
		writer.WriteBoolean(this.OnCurrentMap);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.ElementId = reader.ReadInt();
		this.ElementCellId = reader.ReadVarShort();
		this.ElementState = reader.ReadVarInt();
		this.OnCurrentMap = reader.ReadBoolean();
	}
}
}
