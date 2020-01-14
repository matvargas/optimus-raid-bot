using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class MigratedServerListMessage : NetworkMessage
{

	public const uint Id = 6731;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short[] MigratedServerIds { get; set; }

	public MigratedServerListMessage() {}


	public MigratedServerListMessage InitMigratedServerListMessage(short[] MigratedServerIds)
	{
		this.MigratedServerIds = MigratedServerIds;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteShort(this.MigratedServerIds.Length);
		foreach (short item in this.MigratedServerIds)
		{
			writer.WriteVarShort(item);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		int MigratedServerIdsLen = reader.ReadShort();
		MigratedServerIds = new short[MigratedServerIdsLen];
		for (int i = 0; i < MigratedServerIdsLen; i++)
		{
			this.MigratedServerIds[i] = reader.ReadVarShort();
		}
	}
}
}
