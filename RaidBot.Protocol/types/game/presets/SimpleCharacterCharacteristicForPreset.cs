using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class SimpleCharacterCharacteristicForPreset : NetworkType
{

	public const uint Id = 541;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public String Keyword { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short Base { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short Additionnal { get; set; }

	public SimpleCharacterCharacteristicForPreset() {}


	public SimpleCharacterCharacteristicForPreset InitSimpleCharacterCharacteristicForPreset(String Keyword, short Base, short Additionnal)
	{
		this.Keyword = Keyword;
		this.Base = Base;
		this.Additionnal = Additionnal;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteUTF(this.Keyword);
		writer.WriteVarShort(this.Base);
		writer.WriteVarShort(this.Additionnal);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.Keyword = reader.ReadUTF();
		this.Base = reader.ReadVarShort();
		this.Additionnal = reader.ReadVarShort();
	}
}
}
