using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class SubEntity : NetworkType
{

	public const uint Id = 54;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte BindingPointCategory { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte BindingPointIndex { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public EntityLook SubEntityLook { get; set; }

	public SubEntity() {}


	public SubEntity InitSubEntity(byte BindingPointCategory, byte BindingPointIndex, EntityLook SubEntityLook)
	{
		this.BindingPointCategory = BindingPointCategory;
		this.BindingPointIndex = BindingPointIndex;
		this.SubEntityLook = SubEntityLook;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteByte(this.BindingPointCategory);
		writer.WriteByte(this.BindingPointIndex);
		this.SubEntityLook.Serialize(writer);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.BindingPointCategory = reader.ReadByte();
		this.BindingPointIndex = reader.ReadByte();
		this.SubEntityLook = new EntityLook();
		this.SubEntityLook.Deserialize(reader);
	}
}
}
