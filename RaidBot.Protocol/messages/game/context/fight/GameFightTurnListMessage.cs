using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class GameFightTurnListMessage : NetworkMessage
{

	public const uint Id = 713;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public double[] Ids { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public double[] DeadsIds { get; set; }

	public GameFightTurnListMessage() {}


	public GameFightTurnListMessage InitGameFightTurnListMessage(double[] Ids, double[] DeadsIds)
	{
		this.Ids = Ids;
		this.DeadsIds = DeadsIds;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteShort(this.Ids.Length);
		foreach (double item in this.Ids)
		{
			writer.WriteDouble(item);
		}
		writer.WriteShort(this.DeadsIds.Length);
		foreach (double item in this.DeadsIds)
		{
			writer.WriteDouble(item);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		int IdsLen = reader.ReadShort();
		Ids = new double[IdsLen];
		for (int i = 0; i < IdsLen; i++)
		{
			this.Ids[i] = reader.ReadDouble();
		}
		int DeadsIdsLen = reader.ReadShort();
		DeadsIds = new double[DeadsIdsLen];
		for (int i = 0; i < DeadsIdsLen; i++)
		{
			this.DeadsIds[i] = reader.ReadDouble();
		}
	}
}
}
