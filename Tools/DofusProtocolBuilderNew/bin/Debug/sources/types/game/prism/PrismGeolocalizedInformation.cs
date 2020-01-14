using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class PrismGeolocalizedInformation : PrismSubareaEmptyInfo
{

	public const uint Id = 434;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short WorldX { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short WorldY { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public double MapId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public PrismInformation Prism { get; set; }

	public PrismGeolocalizedInformation() {}


	public PrismGeolocalizedInformation InitPrismGeolocalizedInformation(short WorldX, short WorldY, double MapId, PrismInformation Prism)
	{
		this.WorldX = WorldX;
		this.WorldY = WorldY;
		this.MapId = MapId;
		this.Prism = Prism;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteShort(this.WorldX);
		writer.WriteShort(this.WorldY);
		writer.WriteDouble(this.MapId);
writer.WriteShort(Prism.MessageId);
		Prism.Serialize(writer);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		this.WorldX = reader.ReadShort();
		this.WorldY = reader.ReadShort();
		this.MapId = reader.ReadDouble();
this.Prism = ProtocolTypeManager.GetInstance<PrismInformation>(reader.ReadShort());
		this.Prism.Deserialize(reader);
	}
}
}
