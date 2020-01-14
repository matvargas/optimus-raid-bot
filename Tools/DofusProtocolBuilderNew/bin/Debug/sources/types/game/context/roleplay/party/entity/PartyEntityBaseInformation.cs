using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class PartyEntityBaseInformation : NetworkType
{

	public const uint Id = 552;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte IndexId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte EntityModelId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public EntityLook EntityLook { get; set; }

	public PartyEntityBaseInformation() {}


	public PartyEntityBaseInformation InitPartyEntityBaseInformation(byte IndexId, byte EntityModelId, EntityLook EntityLook)
	{
		this.IndexId = IndexId;
		this.EntityModelId = EntityModelId;
		this.EntityLook = EntityLook;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteByte(this.IndexId);
		writer.WriteByte(this.EntityModelId);
		this.EntityLook.Serialize(writer);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.IndexId = reader.ReadByte();
		this.EntityModelId = reader.ReadByte();
		this.EntityLook = new EntityLook();
		this.EntityLook.Deserialize(reader);
	}
}
}
