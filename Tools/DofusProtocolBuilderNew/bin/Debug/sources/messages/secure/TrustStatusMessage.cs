using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class TrustStatusMessage : NetworkMessage
{

	public const uint Id = 6267;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool Trusted { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool Certified { get; set; }

	public TrustStatusMessage() {}


	public TrustStatusMessage InitTrustStatusMessage(bool Trusted, bool Certified)
	{
		this.Trusted = Trusted;
		this.Certified = Certified;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		byte box = 0;
		box = BooleanByteWrapper.SetFlag(box, 0, Trusted);
		box = BooleanByteWrapper.SetFlag(box, 1, Certified);
		writer.WriteByte(box);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		byte box = reader.ReadByte();
		this.Trusted = BooleanByteWrapper.GetFlag(box, 0);
		this.Certified = BooleanByteWrapper.GetFlag(box, 1);
	}
}
}
