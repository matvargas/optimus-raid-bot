using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class GameFightSpellCooldown : NetworkType
{

	public const uint Id = 205;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int SpellId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte Cooldown { get; set; }

	public GameFightSpellCooldown() {}


	public GameFightSpellCooldown InitGameFightSpellCooldown(int SpellId, byte Cooldown)
	{
		this.SpellId = SpellId;
		this.Cooldown = Cooldown;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteInt(this.SpellId);
		writer.WriteByte(this.Cooldown);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.SpellId = reader.ReadInt();
		this.Cooldown = reader.ReadByte();
	}
}
}
