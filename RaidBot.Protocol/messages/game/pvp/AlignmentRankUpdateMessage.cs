using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class AlignmentRankUpdateMessage : NetworkMessage
{

	public const uint Id = 6058;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte AlignmentRank { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool Verbose { get; set; }

	public AlignmentRankUpdateMessage() {}


	public AlignmentRankUpdateMessage InitAlignmentRankUpdateMessage(byte AlignmentRank, bool Verbose)
	{
		this.AlignmentRank = AlignmentRank;
		this.Verbose = Verbose;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteByte(this.AlignmentRank);
		writer.WriteBoolean(this.Verbose);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.AlignmentRank = reader.ReadByte();
		this.Verbose = reader.ReadBoolean();
	}
}
}
