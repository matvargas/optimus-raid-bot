using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class TitlesAndOrnamentsListMessage : NetworkMessage
{

	public const uint Id = 6367;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short[] Titles { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short[] Ornaments { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short ActiveTitle { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short ActiveOrnament { get; set; }

	public TitlesAndOrnamentsListMessage() {}


	public TitlesAndOrnamentsListMessage InitTitlesAndOrnamentsListMessage(short[] Titles, short[] Ornaments, short ActiveTitle, short ActiveOrnament)
	{
		this.Titles = Titles;
		this.Ornaments = Ornaments;
		this.ActiveTitle = ActiveTitle;
		this.ActiveOrnament = ActiveOrnament;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteShort(this.Titles.Length);
		foreach (short item in this.Titles)
		{
			writer.WriteVarShort(item);
		}
		writer.WriteShort(this.Ornaments.Length);
		foreach (short item in this.Ornaments)
		{
			writer.WriteVarShort(item);
		}
		writer.WriteVarShort(this.ActiveTitle);
		writer.WriteVarShort(this.ActiveOrnament);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		int TitlesLen = reader.ReadShort();
		Titles = new short[TitlesLen];
		for (int i = 0; i < TitlesLen; i++)
		{
			this.Titles[i] = reader.ReadVarShort();
		}
		int OrnamentsLen = reader.ReadShort();
		Ornaments = new short[OrnamentsLen];
		for (int i = 0; i < OrnamentsLen; i++)
		{
			this.Ornaments[i] = reader.ReadVarShort();
		}
		this.ActiveTitle = reader.ReadVarShort();
		this.ActiveOrnament = reader.ReadVarShort();
	}
}
}
