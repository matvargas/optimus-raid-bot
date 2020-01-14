using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class GameMapChangeOrientationsMessage : NetworkMessage
{

	public const uint Id = 6155;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public ActorOrientation[] Orientations { get; set; }

	public GameMapChangeOrientationsMessage() {}


	public GameMapChangeOrientationsMessage InitGameMapChangeOrientationsMessage(ActorOrientation[] Orientations)
	{
		this.Orientations = Orientations;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteShort(this.Orientations.Length);
		foreach (ActorOrientation item in this.Orientations)
		{
			item.Serialize(writer);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		int OrientationsLen = reader.ReadShort();
		Orientations = new ActorOrientation[OrientationsLen];
		for (int i = 0; i < OrientationsLen; i++)
		{
			this.Orientations[i] = new ActorOrientation();
			this.Orientations[i].Deserialize(reader);
		}
	}
}
}
