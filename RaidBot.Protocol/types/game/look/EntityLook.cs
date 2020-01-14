using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class EntityLook : NetworkType
{

	public const uint Id = 55;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short BonesId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short[] Skins { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int[] IndexedColors { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short[] Scales { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public SubEntity[] Subentities { get; set; }

	public EntityLook() {}


	public EntityLook InitEntityLook(short BonesId, short[] Skins, int[] IndexedColors, short[] Scales, SubEntity[] Subentities)
	{
		this.BonesId = BonesId;
		this.Skins = Skins;
		this.IndexedColors = IndexedColors;
		this.Scales = Scales;
		this.Subentities = Subentities;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteVarShort(this.BonesId);
		writer.WriteShort(this.Skins.Length);
		foreach (short item in this.Skins)
		{
			writer.WriteVarShort(item);
		}
		writer.WriteShort(this.IndexedColors.Length);
		foreach (int item in this.IndexedColors)
		{
			writer.WriteInt(item);
		}
		writer.WriteShort(this.Scales.Length);
		foreach (short item in this.Scales)
		{
			writer.WriteVarShort(item);
		}
		writer.WriteShort(this.Subentities.Length);
		foreach (SubEntity item in this.Subentities)
		{
			item.Serialize(writer);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.BonesId = reader.ReadVarShort();
		int SkinsLen = reader.ReadShort();
		Skins = new short[SkinsLen];
		for (int i = 0; i < SkinsLen; i++)
		{
			this.Skins[i] = reader.ReadVarShort();
		}
		int IndexedColorsLen = reader.ReadShort();
		IndexedColors = new int[IndexedColorsLen];
		for (int i = 0; i < IndexedColorsLen; i++)
		{
			this.IndexedColors[i] = reader.ReadInt();
		}
		int ScalesLen = reader.ReadShort();
		Scales = new short[ScalesLen];
		for (int i = 0; i < ScalesLen; i++)
		{
			this.Scales[i] = reader.ReadVarShort();
		}
		int SubentitiesLen = reader.ReadShort();
		Subentities = new SubEntity[SubentitiesLen];
		for (int i = 0; i < SubentitiesLen; i++)
		{
			this.Subentities[i] = new SubEntity();
			this.Subentities[i].Deserialize(reader);
		}
	}
}
}
