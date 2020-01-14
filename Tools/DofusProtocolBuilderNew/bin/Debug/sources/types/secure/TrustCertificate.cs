using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class TrustCertificate : NetworkType
{

	public const uint Id = 377;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int Id_ { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public String Hash { get; set; }

	public TrustCertificate() {}


	public TrustCertificate InitTrustCertificate(int Id_, String Hash)
	{
		this.Id_ = Id_;
		this.Hash = Hash;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteInt(this.Id_);
		writer.WriteUTF(this.Hash);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.Id_ = reader.ReadInt();
		this.Hash = reader.ReadUTF();
	}
}
}
