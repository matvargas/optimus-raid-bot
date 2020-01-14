using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class IdolSelectedMessage : NetworkMessage
{

	public const uint Id = 6581;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool Activate { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool Party { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short IdolId { get; set; }

	public IdolSelectedMessage() {}


	public IdolSelectedMessage InitIdolSelectedMessage(bool Activate, bool Party, short IdolId)
	{
		this.Activate = Activate;
		this.Party = Party;
		this.IdolId = IdolId;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		byte box = 0;
		box = BooleanByteWrapper.SetFlag(box, 0, Activate);
		box = BooleanByteWrapper.SetFlag(box, 1, Party);
		writer.WriteByte(box);
		writer.WriteVarShort(this.IdolId);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		byte box = reader.ReadByte();
		this.Activate = BooleanByteWrapper.GetFlag(box, 0);
		this.Party = BooleanByteWrapper.GetFlag(box, 1);
		this.IdolId = reader.ReadVarShort();
	}
}
}
