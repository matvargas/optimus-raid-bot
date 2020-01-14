using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class AbstractContactInformations : NetworkType
{

	public const uint Id = 380;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int AccountId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public String AccountName { get; set; }

	public AbstractContactInformations() {}


	public AbstractContactInformations InitAbstractContactInformations(int AccountId, String AccountName)
	{
		this.AccountId = AccountId;
		this.AccountName = AccountName;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteInt(this.AccountId);
		writer.WriteUTF(this.AccountName);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.AccountId = reader.ReadInt();
		this.AccountName = reader.ReadUTF();
	}
}
}
