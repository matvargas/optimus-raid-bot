using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class ObjectFeedMessage : NetworkMessage
{

	public const uint Id = 6290;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int ObjectUID { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public ObjectItemQuantity[] Meal { get; set; }

	public ObjectFeedMessage() {}


	public ObjectFeedMessage InitObjectFeedMessage(int ObjectUID, ObjectItemQuantity[] Meal)
	{
		this.ObjectUID = ObjectUID;
		this.Meal = Meal;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteVarInt(this.ObjectUID);
		writer.WriteShort(this.Meal.Length);
		foreach (ObjectItemQuantity item in this.Meal)
		{
			item.Serialize(writer);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.ObjectUID = reader.ReadVarInt();
		int MealLen = reader.ReadShort();
		Meal = new ObjectItemQuantity[MealLen];
		for (int i = 0; i < MealLen; i++)
		{
			this.Meal[i] = new ObjectItemQuantity();
			this.Meal[i].Deserialize(reader);
		}
	}
}
}
