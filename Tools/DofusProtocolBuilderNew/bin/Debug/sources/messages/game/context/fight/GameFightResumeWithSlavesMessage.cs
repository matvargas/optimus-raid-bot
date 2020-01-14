using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class GameFightResumeWithSlavesMessage : GameFightResumeMessage
{

	public const uint Id = 6215;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public GameFightResumeSlaveInfo[] SlavesInfo { get; set; }

	public GameFightResumeWithSlavesMessage() {}


	public GameFightResumeWithSlavesMessage InitGameFightResumeWithSlavesMessage(GameFightResumeSlaveInfo[] SlavesInfo)
	{
		this.SlavesInfo = SlavesInfo;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteShort(this.SlavesInfo.Length);
		foreach (GameFightResumeSlaveInfo item in this.SlavesInfo)
		{
			item.Serialize(writer);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		int SlavesInfoLen = reader.ReadShort();
		SlavesInfo = new GameFightResumeSlaveInfo[SlavesInfoLen];
		for (int i = 0; i < SlavesInfoLen; i++)
		{
			this.SlavesInfo[i] = new GameFightResumeSlaveInfo();
			this.SlavesInfo[i].Deserialize(reader);
		}
	}
}
}
