using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class PartyJoinMessage : AbstractPartyMessage
{

	public const uint Id = 5576;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte PartyType { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public long PartyLeaderId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte MaxParticipants { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public PartyMemberInformations[] Members { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public PartyGuestInformations[] Guests { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool Restricted { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public String PartyName { get; set; }

	public PartyJoinMessage() {}


	public PartyJoinMessage InitPartyJoinMessage(byte PartyType, long PartyLeaderId, byte MaxParticipants, PartyMemberInformations[] Members, PartyGuestInformations[] Guests, bool Restricted, String PartyName)
	{
		this.PartyType = PartyType;
		this.PartyLeaderId = PartyLeaderId;
		this.MaxParticipants = MaxParticipants;
		this.Members = Members;
		this.Guests = Guests;
		this.Restricted = Restricted;
		this.PartyName = PartyName;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteByte(this.PartyType);
		writer.WriteVarLong(this.PartyLeaderId);
		writer.WriteByte(this.MaxParticipants);
		writer.WriteShort(this.Members.Length);
		foreach (PartyMemberInformations item in this.Members)
		{
			writer.WriteShort(item.MessageId);
			item.Serialize(writer);
		}
		writer.WriteShort(this.Guests.Length);
		foreach (PartyGuestInformations item in this.Guests)
		{
			item.Serialize(writer);
		}
		writer.WriteBoolean(this.Restricted);
		writer.WriteUTF(this.PartyName);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		this.PartyType = reader.ReadByte();
		this.PartyLeaderId = reader.ReadVarLong();
		this.MaxParticipants = reader.ReadByte();
		int MembersLen = reader.ReadShort();
		Members = new PartyMemberInformations[MembersLen];
		for (int i = 0; i < MembersLen; i++)
		{
			this.Members[i] = ProtocolTypeManager.GetInstance<PartyMemberInformations>(reader.ReadShort());
			this.Members[i].Deserialize(reader);
		}
		int GuestsLen = reader.ReadShort();
		Guests = new PartyGuestInformations[GuestsLen];
		for (int i = 0; i < GuestsLen; i++)
		{
			this.Guests[i] = new PartyGuestInformations();
			this.Guests[i].Deserialize(reader);
		}
		this.Restricted = reader.ReadBoolean();
		this.PartyName = reader.ReadUTF();
	}
}
}
