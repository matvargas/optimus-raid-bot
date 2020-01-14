using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class GameServerInformations : NetworkType
{

	public const uint Id = 25;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool IsMonoAccount { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool IsSelectable { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short Id_ { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte Type { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte Status { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte Completion { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte CharactersCount { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte CharactersSlots { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public double Date { get; set; }

	public GameServerInformations() {}


	public GameServerInformations InitGameServerInformations(bool IsMonoAccount, bool IsSelectable, short Id_, byte Type, byte Status, byte Completion, byte CharactersCount, byte CharactersSlots, double Date)
	{
		this.IsMonoAccount = IsMonoAccount;
		this.IsSelectable = IsSelectable;
		this.Id_ = Id_;
		this.Type = Type;
		this.Status = Status;
		this.Completion = Completion;
		this.CharactersCount = CharactersCount;
		this.CharactersSlots = CharactersSlots;
		this.Date = Date;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		byte box = 0;
		box = BooleanByteWrapper.SetFlag(box, 0, IsMonoAccount);
		box = BooleanByteWrapper.SetFlag(box, 1, IsSelectable);
		writer.WriteByte(box);
		writer.WriteVarShort(this.Id_);
		writer.WriteByte(this.Type);
		writer.WriteByte(this.Status);
		writer.WriteByte(this.Completion);
		writer.WriteByte(this.CharactersCount);
		writer.WriteByte(this.CharactersSlots);
		writer.WriteDouble(this.Date);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		byte box = reader.ReadByte();
		this.IsMonoAccount = BooleanByteWrapper.GetFlag(box, 0);
		this.IsSelectable = BooleanByteWrapper.GetFlag(box, 1);
		this.Id_ = reader.ReadVarShort();
		this.Type = reader.ReadByte();
		this.Status = reader.ReadByte();
		this.Completion = reader.ReadByte();
		this.CharactersCount = reader.ReadByte();
		this.CharactersSlots = reader.ReadByte();
		this.Date = reader.ReadDouble();
	}
}
}
