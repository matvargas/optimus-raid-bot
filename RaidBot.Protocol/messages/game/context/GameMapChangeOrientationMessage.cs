using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class GameMapChangeOrientationMessage : NetworkMessage
{

	public const uint Id = 946;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public ActorOrientation Orientation { get; set; }

	public GameMapChangeOrientationMessage() {}


	public GameMapChangeOrientationMessage InitGameMapChangeOrientationMessage(ActorOrientation Orientation)
	{
		this.Orientation = Orientation;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		this.Orientation.Serialize(writer);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.Orientation = new ActorOrientation();
		this.Orientation.Deserialize(reader);
	}
}
}
