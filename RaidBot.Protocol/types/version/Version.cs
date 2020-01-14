using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class Version : NetworkType
{

	public const uint Id = 11;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte Major { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte Minor { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte Release { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int Revision { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte Patch { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte BuildType { get; set; }

	public Version() {}


	public Version InitVersion(byte Major, byte Minor, byte Release, int Revision, byte Patch, byte BuildType)
	{
		this.Major = Major;
		this.Minor = Minor;
		this.Release = Release;
		this.Revision = Revision;
		this.Patch = Patch;
		this.BuildType = BuildType;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteByte(this.Major);
		writer.WriteByte(this.Minor);
		writer.WriteByte(this.Release);
		writer.WriteInt(this.Revision);
		writer.WriteByte(this.Patch);
		writer.WriteByte(this.BuildType);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.Major = reader.ReadByte();
		this.Minor = reader.ReadByte();
		this.Release = reader.ReadByte();
		this.Revision = reader.ReadInt();
		this.Patch = reader.ReadByte();
		this.BuildType = reader.ReadByte();
	}
}
}
