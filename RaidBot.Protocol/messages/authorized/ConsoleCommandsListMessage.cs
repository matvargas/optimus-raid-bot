using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class ConsoleCommandsListMessage : NetworkMessage
{

	public const uint Id = 6127;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public String[] Aliases { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public String[] Args { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public String[] Descriptions { get; set; }

	public ConsoleCommandsListMessage() {}


	public ConsoleCommandsListMessage InitConsoleCommandsListMessage(String[] Aliases, String[] Args, String[] Descriptions)
	{
		this.Aliases = Aliases;
		this.Args = Args;
		this.Descriptions = Descriptions;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteShort(this.Aliases.Length);
		foreach (String item in this.Aliases)
		{
			writer.WriteUTF(item);
		}
		writer.WriteShort(this.Args.Length);
		foreach (String item in this.Args)
		{
			writer.WriteUTF(item);
		}
		writer.WriteShort(this.Descriptions.Length);
		foreach (String item in this.Descriptions)
		{
			writer.WriteUTF(item);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		int AliasesLen = reader.ReadShort();
		Aliases = new String[AliasesLen];
		for (int i = 0; i < AliasesLen; i++)
		{
			this.Aliases[i] = reader.ReadUTF();
		}
		int ArgsLen = reader.ReadShort();
		Args = new String[ArgsLen];
		for (int i = 0; i < ArgsLen; i++)
		{
			this.Args[i] = reader.ReadUTF();
		}
		int DescriptionsLen = reader.ReadShort();
		Descriptions = new String[DescriptionsLen];
		for (int i = 0; i < DescriptionsLen; i++)
		{
			this.Descriptions[i] = reader.ReadUTF();
		}
	}
}
}
