using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class PaddockToSellListRequestMessage : NetworkMessage
{

	public const uint Id = 6141;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short PageIndex { get; set; }

	public PaddockToSellListRequestMessage() {}


	public PaddockToSellListRequestMessage InitPaddockToSellListRequestMessage(short PageIndex)
	{
		this.PageIndex = PageIndex;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteVarShort(this.PageIndex);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.PageIndex = reader.ReadVarShort();
	}
}
}
