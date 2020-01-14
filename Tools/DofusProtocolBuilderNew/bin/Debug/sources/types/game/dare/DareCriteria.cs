using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class DareCriteria : NetworkType
{

	public const uint Id = 501;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte Type { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int[] Params { get; set; }

	public DareCriteria() {}


	public DareCriteria InitDareCriteria(byte Type, int[] Params)
	{
		this.Type = Type;
		this.Params = Params;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteByte(this.Type);
		writer.WriteShort(this.Params.Length);
		foreach (int item in this.Params)
		{
			writer.WriteInt(item);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.Type = reader.ReadByte();
		int ParamsLen = reader.ReadShort();
		Params = new int[ParamsLen];
		for (int i = 0; i < ParamsLen; i++)
		{
			this.Params[i] = reader.ReadInt();
		}
	}
}
}
