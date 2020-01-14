using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class StartupActionAddObject : NetworkType
{

	public const uint Id = 52;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int Uid { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public String Title { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public String Text { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public String DescUrl { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public String PictureUrl { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public ObjectItemInformationWithQuantity[] Items { get; set; }

	public StartupActionAddObject() {}


	public StartupActionAddObject InitStartupActionAddObject(int Uid, String Title, String Text, String DescUrl, String PictureUrl, ObjectItemInformationWithQuantity[] Items)
	{
		this.Uid = Uid;
		this.Title = Title;
		this.Text = Text;
		this.DescUrl = DescUrl;
		this.PictureUrl = PictureUrl;
		this.Items = Items;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteInt(this.Uid);
		writer.WriteUTF(this.Title);
		writer.WriteUTF(this.Text);
		writer.WriteUTF(this.DescUrl);
		writer.WriteUTF(this.PictureUrl);
		writer.WriteShort(this.Items.Length);
		foreach (ObjectItemInformationWithQuantity item in this.Items)
		{
			item.Serialize(writer);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.Uid = reader.ReadInt();
		this.Title = reader.ReadUTF();
		this.Text = reader.ReadUTF();
		this.DescUrl = reader.ReadUTF();
		this.PictureUrl = reader.ReadUTF();
		int ItemsLen = reader.ReadShort();
		Items = new ObjectItemInformationWithQuantity[ItemsLen];
		for (int i = 0; i < ItemsLen; i++)
		{
			this.Items[i] = new ObjectItemInformationWithQuantity();
			this.Items[i].Deserialize(reader);
		}
	}
}
}
