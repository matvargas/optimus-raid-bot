using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class PartyIdol : Idol
{

	public const uint Id = 490;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public long[] OwnersIds { get; set; }

	public PartyIdol() {}


	public PartyIdol InitPartyIdol(long[] OwnersIds)
	{
		this.OwnersIds = OwnersIds;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteShort(this.OwnersIds.Length);
		foreach (long item in this.OwnersIds)
		{
			writer.WriteVarLong(item);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		int OwnersIdsLen = reader.ReadShort();
		OwnersIds = new long[OwnersIdsLen];
		for (int i = 0; i < OwnersIdsLen; i++)
		{
			this.OwnersIds[i] = reader.ReadVarLong();
		}
	}
}
}
