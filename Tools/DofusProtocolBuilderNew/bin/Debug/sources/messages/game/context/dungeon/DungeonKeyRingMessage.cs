using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class DungeonKeyRingMessage : NetworkMessage
{

	public const uint Id = 6299;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short[] Availables { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short[] Unavailables { get; set; }

	public DungeonKeyRingMessage() {}


	public DungeonKeyRingMessage InitDungeonKeyRingMessage(short[] Availables, short[] Unavailables)
	{
		this.Availables = Availables;
		this.Unavailables = Unavailables;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteShort(this.Availables.Length);
		foreach (short item in this.Availables)
		{
			writer.WriteVarShort(item);
		}
		writer.WriteShort(this.Unavailables.Length);
		foreach (short item in this.Unavailables)
		{
			writer.WriteVarShort(item);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		int AvailablesLen = reader.ReadShort();
		Availables = new short[AvailablesLen];
		for (int i = 0; i < AvailablesLen; i++)
		{
			this.Availables[i] = reader.ReadVarShort();
		}
		int UnavailablesLen = reader.ReadShort();
		Unavailables = new short[UnavailablesLen];
		for (int i = 0; i < UnavailablesLen; i++)
		{
			this.Unavailables[i] = reader.ReadVarShort();
		}
	}
}
}
