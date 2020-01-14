using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class TextInformationMessage : NetworkMessage
{

	public const uint Id = 780;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte MsgType { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short MsgId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public String[] Parameters { get; set; }

	public TextInformationMessage() {}


	public TextInformationMessage InitTextInformationMessage(byte MsgType, short MsgId, String[] Parameters)
	{
		this.MsgType = MsgType;
		this.MsgId = MsgId;
		this.Parameters = Parameters;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteByte(this.MsgType);
		writer.WriteVarShort(this.MsgId);
		writer.WriteShort(this.Parameters.Length);
		foreach (String item in this.Parameters)
		{
			writer.WriteUTF(item);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.MsgType = reader.ReadByte();
		this.MsgId = reader.ReadVarShort();
		int ParametersLen = reader.ReadShort();
		Parameters = new String[ParametersLen];
		for (int i = 0; i < ParametersLen; i++)
		{
			this.Parameters[i] = reader.ReadUTF();
		}
	}
}
}
