using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class HumanInformations : NetworkType
{

	public const uint Id = 157;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public ActorRestrictionsInformations Restrictions { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool Sex { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public HumanOption[] Options { get; set; }

	public HumanInformations() {}


	public HumanInformations InitHumanInformations(ActorRestrictionsInformations Restrictions, bool Sex, HumanOption[] Options)
	{
		this.Restrictions = Restrictions;
		this.Sex = Sex;
		this.Options = Options;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		this.Restrictions.Serialize(writer);
		writer.WriteBoolean(this.Sex);
		writer.WriteShort(this.Options.Length);
		foreach (HumanOption item in this.Options)
		{
			writer.WriteShort(item.MessageId);
			item.Serialize(writer);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.Restrictions = new ActorRestrictionsInformations();
		this.Restrictions.Deserialize(reader);
		this.Sex = reader.ReadBoolean();
		int OptionsLen = reader.ReadShort();
		Options = new HumanOption[OptionsLen];
		for (int i = 0; i < OptionsLen; i++)
		{
			this.Options[i] = ProtocolTypeManager.GetInstance<HumanOption>(reader.ReadShort());
			this.Options[i].Deserialize(reader);
		}
	}
}
}
