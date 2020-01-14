using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class MountInformationsForPaddock : NetworkType
{

	public const uint Id = 184;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short ModelId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public String Name { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public String OwnerName { get; set; }

	public MountInformationsForPaddock() {}


	public MountInformationsForPaddock InitMountInformationsForPaddock(short ModelId, String Name, String OwnerName)
	{
		this.ModelId = ModelId;
		this.Name = Name;
		this.OwnerName = OwnerName;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteVarShort(this.ModelId);
		writer.WriteUTF(this.Name);
		writer.WriteUTF(this.OwnerName);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.ModelId = reader.ReadVarShort();
		this.Name = reader.ReadUTF();
		this.OwnerName = reader.ReadUTF();
	}
}
}
