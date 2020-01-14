using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class GameActionFightTackledMessage : AbstractGameActionMessage
{

	public const uint Id = 1004;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public double[] TacklersIds { get; set; }

	public GameActionFightTackledMessage() {}


	public GameActionFightTackledMessage InitGameActionFightTackledMessage(double[] TacklersIds)
	{
		this.TacklersIds = TacklersIds;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteShort(this.TacklersIds.Length);
		foreach (double item in this.TacklersIds)
		{
			writer.WriteDouble(item);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		int TacklersIdsLen = reader.ReadShort();
		TacklersIds = new double[TacklersIdsLen];
		for (int i = 0; i < TacklersIdsLen; i++)
		{
			this.TacklersIds[i] = reader.ReadDouble();
		}
	}
}
}
