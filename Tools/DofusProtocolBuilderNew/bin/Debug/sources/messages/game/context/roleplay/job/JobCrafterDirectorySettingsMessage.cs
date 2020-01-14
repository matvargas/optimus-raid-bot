using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class JobCrafterDirectorySettingsMessage : NetworkMessage
{

	public const uint Id = 5652;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public JobCrafterDirectorySettings[] CraftersSettings { get; set; }

	public JobCrafterDirectorySettingsMessage() {}


	public JobCrafterDirectorySettingsMessage InitJobCrafterDirectorySettingsMessage(JobCrafterDirectorySettings[] CraftersSettings)
	{
		this.CraftersSettings = CraftersSettings;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteShort(this.CraftersSettings.Length);
		foreach (JobCrafterDirectorySettings item in this.CraftersSettings)
		{
			item.Serialize(writer);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		int CraftersSettingsLen = reader.ReadShort();
		CraftersSettings = new JobCrafterDirectorySettings[CraftersSettingsLen];
		for (int i = 0; i < CraftersSettingsLen; i++)
		{
			this.CraftersSettings[i] = new JobCrafterDirectorySettings();
			this.CraftersSettings[i].Deserialize(reader);
		}
	}
}
}
