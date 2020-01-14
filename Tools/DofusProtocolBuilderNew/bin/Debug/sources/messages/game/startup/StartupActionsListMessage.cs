using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class StartupActionsListMessage : NetworkMessage
{

	public const uint Id = 1301;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public StartupActionAddObject[] Actions { get; set; }

	public StartupActionsListMessage() {}


	public StartupActionsListMessage InitStartupActionsListMessage(StartupActionAddObject[] Actions)
	{
		this.Actions = Actions;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteShort(this.Actions.Length);
		foreach (StartupActionAddObject item in this.Actions)
		{
			item.Serialize(writer);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		int ActionsLen = reader.ReadShort();
		Actions = new StartupActionAddObject[ActionsLen];
		for (int i = 0; i < ActionsLen; i++)
		{
			this.Actions[i] = new StartupActionAddObject();
			this.Actions[i].Deserialize(reader);
		}
	}
}
}
