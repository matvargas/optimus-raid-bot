using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class ServerSessionConstantsMessage : NetworkMessage
{

	public const uint Id = 6434;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public ServerSessionConstant[] Variables { get; set; }

	public ServerSessionConstantsMessage() {}


	public ServerSessionConstantsMessage InitServerSessionConstantsMessage(ServerSessionConstant[] Variables)
	{
		this.Variables = Variables;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteShort(this.Variables.Length);
		foreach (ServerSessionConstant item in this.Variables)
		{
			writer.WriteShort(item.MessageId);
			item.Serialize(writer);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		int VariablesLen = reader.ReadShort();
		Variables = new ServerSessionConstant[VariablesLen];
		for (int i = 0; i < VariablesLen; i++)
		{
			this.Variables[i] = ProtocolTypeManager.GetInstance<ServerSessionConstant>(reader.ReadShort());
			this.Variables[i].Deserialize(reader);
		}
	}
}
}
