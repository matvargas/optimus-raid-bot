using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class IdentificationMessage : NetworkMessage
{

	public const uint Id = 4;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool Autoconnect { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool UseCertificate { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool UseLoginToken { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public VersionExtended Version { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public String Lang { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte[] Credentials { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short ServerId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public long SessionOptionalSalt { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short[] FailedAttempts { get; set; }

	public IdentificationMessage() {}


	public IdentificationMessage InitIdentificationMessage(bool Autoconnect, bool UseCertificate, bool UseLoginToken, VersionExtended Version, String Lang, byte[] Credentials, short ServerId, long SessionOptionalSalt, short[] FailedAttempts)
	{
		this.Autoconnect = Autoconnect;
		this.UseCertificate = UseCertificate;
		this.UseLoginToken = UseLoginToken;
		this.Version = Version;
		this.Lang = Lang;
		this.Credentials = Credentials;
		this.ServerId = ServerId;
		this.SessionOptionalSalt = SessionOptionalSalt;
		this.FailedAttempts = FailedAttempts;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		byte box = 0;
		box = BooleanByteWrapper.SetFlag(box, 0, Autoconnect);
		box = BooleanByteWrapper.SetFlag(box, 1, UseCertificate);
		box = BooleanByteWrapper.SetFlag(box, 2, UseLoginToken);
		writer.WriteByte(box);
		this.Version.Serialize(writer);
		writer.WriteUTF(this.Lang);
		writer.WriteVarInt(this.Credentials.Length);
		foreach (byte item in this.Credentials)
		{
			writer.WriteByte(item);
		}
		writer.WriteShort(this.ServerId);
		writer.WriteVarLong(this.SessionOptionalSalt);
		writer.WriteShort(this.FailedAttempts.Length);
		foreach (short item in this.FailedAttempts)
		{
			writer.WriteVarShort(item);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		byte box = reader.ReadByte();
		this.Autoconnect = BooleanByteWrapper.GetFlag(box, 0);
		this.UseCertificate = BooleanByteWrapper.GetFlag(box, 1);
		this.UseLoginToken = BooleanByteWrapper.GetFlag(box, 2);
		this.Version = new VersionExtended();
		this.Version.Deserialize(reader);
		this.Lang = reader.ReadUTF();
		int CredentialsLen = reader.ReadVarInt();
		Credentials = new byte[CredentialsLen];
		for (int i = 0; i < CredentialsLen; i++)
		{
			this.Credentials[i] = reader.ReadByte();
		}
		this.ServerId = reader.ReadShort();
		this.SessionOptionalSalt = reader.ReadVarLong();
		int FailedAttemptsLen = reader.ReadShort();
		FailedAttempts = new short[FailedAttemptsLen];
		for (int i = 0; i < FailedAttemptsLen; i++)
		{
			this.FailedAttempts[i] = reader.ReadVarShort();
		}
	}
}
}
