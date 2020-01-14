using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class IdentificationSuccessMessage : NetworkMessage
{

	public const uint Id = 22;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool HasRights { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool WasAlreadyConnected { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public String Login { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public String Nickname { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int AccountId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte CommunityId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public String SecretQuestion { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public double AccountCreation { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public double SubscriptionElapsedDuration { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public double SubscriptionEndDate { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte HavenbagAvailableRoom { get; set; }

	public IdentificationSuccessMessage() {}


	public IdentificationSuccessMessage InitIdentificationSuccessMessage(bool HasRights, bool WasAlreadyConnected, String Login, String Nickname, int AccountId, byte CommunityId, String SecretQuestion, double AccountCreation, double SubscriptionElapsedDuration, double SubscriptionEndDate, byte HavenbagAvailableRoom)
	{
		this.HasRights = HasRights;
		this.WasAlreadyConnected = WasAlreadyConnected;
		this.Login = Login;
		this.Nickname = Nickname;
		this.AccountId = AccountId;
		this.CommunityId = CommunityId;
		this.SecretQuestion = SecretQuestion;
		this.AccountCreation = AccountCreation;
		this.SubscriptionElapsedDuration = SubscriptionElapsedDuration;
		this.SubscriptionEndDate = SubscriptionEndDate;
		this.HavenbagAvailableRoom = HavenbagAvailableRoom;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		byte box = 0;
		box = BooleanByteWrapper.SetFlag(box, 0, HasRights);
		box = BooleanByteWrapper.SetFlag(box, 1, WasAlreadyConnected);
		writer.WriteByte(box);
		writer.WriteUTF(this.Login);
		writer.WriteUTF(this.Nickname);
		writer.WriteInt(this.AccountId);
		writer.WriteByte(this.CommunityId);
		writer.WriteUTF(this.SecretQuestion);
		writer.WriteDouble(this.AccountCreation);
		writer.WriteDouble(this.SubscriptionElapsedDuration);
		writer.WriteDouble(this.SubscriptionEndDate);
		writer.WriteByte(this.HavenbagAvailableRoom);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		byte box = reader.ReadByte();
		this.HasRights = BooleanByteWrapper.GetFlag(box, 0);
		this.WasAlreadyConnected = BooleanByteWrapper.GetFlag(box, 1);
		this.Login = reader.ReadUTF();
		this.Nickname = reader.ReadUTF();
		this.AccountId = reader.ReadInt();
		this.CommunityId = reader.ReadByte();
		this.SecretQuestion = reader.ReadUTF();
		this.AccountCreation = reader.ReadDouble();
		this.SubscriptionElapsedDuration = reader.ReadDouble();
		this.SubscriptionEndDate = reader.ReadDouble();
		this.HavenbagAvailableRoom = reader.ReadByte();
	}
}
}
