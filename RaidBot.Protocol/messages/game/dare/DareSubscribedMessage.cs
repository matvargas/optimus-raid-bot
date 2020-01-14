using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class DareSubscribedMessage : NetworkMessage
{

	public const uint Id = 6660;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool Success { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool Subscribe { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public double DareId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public DareVersatileInformations DareVersatilesInfos { get; set; }

	public DareSubscribedMessage() {}


	public DareSubscribedMessage InitDareSubscribedMessage(bool Success, bool Subscribe, double DareId, DareVersatileInformations DareVersatilesInfos)
	{
		this.Success = Success;
		this.Subscribe = Subscribe;
		this.DareId = DareId;
		this.DareVersatilesInfos = DareVersatilesInfos;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		byte box = 0;
		box = BooleanByteWrapper.SetFlag(box, 0, Success);
		box = BooleanByteWrapper.SetFlag(box, 1, Subscribe);
		writer.WriteByte(box);
		writer.WriteDouble(this.DareId);
		this.DareVersatilesInfos.Serialize(writer);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		byte box = reader.ReadByte();
		this.Success = BooleanByteWrapper.GetFlag(box, 0);
		this.Subscribe = BooleanByteWrapper.GetFlag(box, 1);
		this.DareId = reader.ReadDouble();
		this.DareVersatilesInfos = new DareVersatileInformations();
		this.DareVersatilesInfos.Deserialize(reader);
	}
}
}
