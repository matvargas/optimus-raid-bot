using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class JobBookSubscriptionMessage : NetworkMessage
{

	public const uint Id = 6593;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public JobBookSubscription[] Subscriptions { get; set; }

	public JobBookSubscriptionMessage() {}


	public JobBookSubscriptionMessage InitJobBookSubscriptionMessage(JobBookSubscription[] Subscriptions)
	{
		this.Subscriptions = Subscriptions;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteShort(this.Subscriptions.Length);
		foreach (JobBookSubscription item in this.Subscriptions)
		{
			item.Serialize(writer);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		int SubscriptionsLen = reader.ReadShort();
		Subscriptions = new JobBookSubscription[SubscriptionsLen];
		for (int i = 0; i < SubscriptionsLen; i++)
		{
			this.Subscriptions[i] = new JobBookSubscription();
			this.Subscriptions[i].Deserialize(reader);
		}
	}
}
}
