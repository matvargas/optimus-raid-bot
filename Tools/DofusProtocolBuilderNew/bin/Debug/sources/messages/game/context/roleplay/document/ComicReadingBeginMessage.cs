using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class ComicReadingBeginMessage : NetworkMessage
{

	public const uint Id = 6536;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short ComicId { get; set; }

	public ComicReadingBeginMessage() {}


	public ComicReadingBeginMessage InitComicReadingBeginMessage(short ComicId)
	{
		this.ComicId = ComicId;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteVarShort(this.ComicId);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.ComicId = reader.ReadVarShort();
	}
}
}
