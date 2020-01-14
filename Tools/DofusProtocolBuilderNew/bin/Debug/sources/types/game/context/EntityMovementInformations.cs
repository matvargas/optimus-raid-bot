using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class EntityMovementInformations : NetworkType
{

	public const uint Id = 63;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int Id_ { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte[] Steps { get; set; }

	public EntityMovementInformations() {}


	public EntityMovementInformations InitEntityMovementInformations(int Id_, byte[] Steps)
	{
		this.Id_ = Id_;
		this.Steps = Steps;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteInt(this.Id_);
		writer.WriteShort(this.Steps.Length);
		foreach (byte item in this.Steps)
		{
			writer.WriteByte(item);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.Id_ = reader.ReadInt();
		int StepsLen = reader.ReadShort();
		Steps = new byte[StepsLen];
		for (int i = 0; i < StepsLen; i++)
		{
			this.Steps[i] = reader.ReadByte();
		}
	}
}
}
