using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class HumanOptionTitle : HumanOption
{

	public const uint Id = 408;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short TitleId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public String TitleParam { get; set; }

	public HumanOptionTitle() {}


	public HumanOptionTitle InitHumanOptionTitle(short TitleId, String TitleParam)
	{
		this.TitleId = TitleId;
		this.TitleParam = TitleParam;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteVarShort(this.TitleId);
		writer.WriteUTF(this.TitleParam);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		this.TitleId = reader.ReadVarShort();
		this.TitleParam = reader.ReadUTF();
	}
}
}
