using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class UpdateLifePointsMessage : NetworkMessage
{

	public const uint Id = 5658;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int LifePoints { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int MaxLifePoints { get; set; }

	public UpdateLifePointsMessage() {}


	public UpdateLifePointsMessage InitUpdateLifePointsMessage(int LifePoints, int MaxLifePoints)
	{
		this.LifePoints = LifePoints;
		this.MaxLifePoints = MaxLifePoints;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteVarInt(this.LifePoints);
		writer.WriteVarInt(this.MaxLifePoints);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.LifePoints = reader.ReadVarInt();
		this.MaxLifePoints = reader.ReadVarInt();
	}
}
}
