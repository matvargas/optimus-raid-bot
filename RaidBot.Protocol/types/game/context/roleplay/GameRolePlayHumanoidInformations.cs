using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class GameRolePlayHumanoidInformations : GameRolePlayNamedActorInformations
{

	public const uint Id = 159;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public HumanInformations HumanoidInfo { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int AccountId { get; set; }

	public GameRolePlayHumanoidInformations() {}


	public GameRolePlayHumanoidInformations InitGameRolePlayHumanoidInformations(HumanInformations HumanoidInfo, int AccountId)
	{
		this.HumanoidInfo = HumanoidInfo;
		this.AccountId = AccountId;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
writer.WriteShort(HumanoidInfo.MessageId);
		HumanoidInfo.Serialize(writer);
		writer.WriteInt(this.AccountId);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
this.HumanoidInfo = ProtocolTypeManager.GetInstance<HumanInformations>(reader.ReadShort());
		this.HumanoidInfo.Deserialize(reader);
		this.AccountId = reader.ReadInt();
	}
}
}
