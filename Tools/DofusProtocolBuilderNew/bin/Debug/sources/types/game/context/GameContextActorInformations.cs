using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class GameContextActorInformations : NetworkType
{

	public const uint Id = 150;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public double ContextualId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public EntityLook Look { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public EntityDispositionInformations Disposition { get; set; }

	public GameContextActorInformations() {}


	public GameContextActorInformations InitGameContextActorInformations(double ContextualId, EntityLook Look, EntityDispositionInformations Disposition)
	{
		this.ContextualId = ContextualId;
		this.Look = Look;
		this.Disposition = Disposition;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteDouble(this.ContextualId);
		this.Look.Serialize(writer);
writer.WriteShort(Disposition.MessageId);
		Disposition.Serialize(writer);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.ContextualId = reader.ReadDouble();
		this.Look = new EntityLook();
		this.Look.Deserialize(reader);
this.Disposition = ProtocolTypeManager.GetInstance<EntityDispositionInformations>(reader.ReadShort());
		this.Disposition.Deserialize(reader);
	}
}
}
