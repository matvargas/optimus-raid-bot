using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class HumanOptionFollowers : HumanOption
{

	public const uint Id = 410;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public IndexedEntityLook[] FollowingCharactersLook { get; set; }

	public HumanOptionFollowers() {}


	public HumanOptionFollowers InitHumanOptionFollowers(IndexedEntityLook[] FollowingCharactersLook)
	{
		this.FollowingCharactersLook = FollowingCharactersLook;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteShort(this.FollowingCharactersLook.Length);
		foreach (IndexedEntityLook item in this.FollowingCharactersLook)
		{
			item.Serialize(writer);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		int FollowingCharactersLookLen = reader.ReadShort();
		FollowingCharactersLook = new IndexedEntityLook[FollowingCharactersLookLen];
		for (int i = 0; i < FollowingCharactersLookLen; i++)
		{
			this.FollowingCharactersLook[i] = new IndexedEntityLook();
			this.FollowingCharactersLook[i].Deserialize(reader);
		}
	}
}
}
