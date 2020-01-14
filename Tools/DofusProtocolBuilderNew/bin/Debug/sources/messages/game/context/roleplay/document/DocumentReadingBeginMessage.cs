using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class DocumentReadingBeginMessage : NetworkMessage
{

	public const uint Id = 5675;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short DocumentId { get; set; }

	public DocumentReadingBeginMessage() {}


	public DocumentReadingBeginMessage InitDocumentReadingBeginMessage(short DocumentId)
	{
		this.DocumentId = DocumentId;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteVarShort(this.DocumentId);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.DocumentId = reader.ReadVarShort();
	}
}
}
