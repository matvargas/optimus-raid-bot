using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class InteractiveElement : NetworkType
{

	public const uint Id = 80;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int ElementId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int ElementTypeId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public InteractiveElementSkill[] EnabledSkills { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public InteractiveElementSkill[] DisabledSkills { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool OnCurrentMap { get; set; }

	public InteractiveElement() {}


	public InteractiveElement InitInteractiveElement(int ElementId, int ElementTypeId, InteractiveElementSkill[] EnabledSkills, InteractiveElementSkill[] DisabledSkills, bool OnCurrentMap)
	{
		this.ElementId = ElementId;
		this.ElementTypeId = ElementTypeId;
		this.EnabledSkills = EnabledSkills;
		this.DisabledSkills = DisabledSkills;
		this.OnCurrentMap = OnCurrentMap;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteInt(this.ElementId);
		writer.WriteInt(this.ElementTypeId);
		writer.WriteShort(this.EnabledSkills.Length);
		foreach (InteractiveElementSkill item in this.EnabledSkills)
		{
			writer.WriteShort(item.MessageId);
			item.Serialize(writer);
		}
		writer.WriteShort(this.DisabledSkills.Length);
		foreach (InteractiveElementSkill item in this.DisabledSkills)
		{
			writer.WriteShort(item.MessageId);
			item.Serialize(writer);
		}
		writer.WriteBoolean(this.OnCurrentMap);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.ElementId = reader.ReadInt();
		this.ElementTypeId = reader.ReadInt();
		int EnabledSkillsLen = reader.ReadShort();
		EnabledSkills = new InteractiveElementSkill[EnabledSkillsLen];
		for (int i = 0; i < EnabledSkillsLen; i++)
		{
			this.EnabledSkills[i] = ProtocolTypeManager.GetInstance<InteractiveElementSkill>(reader.ReadShort());
			this.EnabledSkills[i].Deserialize(reader);
		}
		int DisabledSkillsLen = reader.ReadShort();
		DisabledSkills = new InteractiveElementSkill[DisabledSkillsLen];
		for (int i = 0; i < DisabledSkillsLen; i++)
		{
			this.DisabledSkills[i] = ProtocolTypeManager.GetInstance<InteractiveElementSkill>(reader.ReadShort());
			this.DisabledSkills[i].Deserialize(reader);
		}
		this.OnCurrentMap = reader.ReadBoolean();
	}
}
}
