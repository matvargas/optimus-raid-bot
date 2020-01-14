using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class ActorRestrictionsInformations : NetworkType
{

	public const uint Id = 204;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool CantBeAggressed { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool CantBeChallenged { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool CantTrade { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool CantBeAttackedByMutant { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool CantRun { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool ForceSlowWalk { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool CantMinimize { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool CantMove { get; set; }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool CantAggress { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool CantChallenge { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool CantExchange { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool CantAttack { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool CantChat { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool CantBeMerchant { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool CantUseObject { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool CantUseTaxCollector { get; set; }

        [TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool CantUseInteractive { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool CantSpeakToNPC { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool CantChangeZone { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool CantAttackMonster { get; set; }

	public ActorRestrictionsInformations() {}


	public ActorRestrictionsInformations InitActorRestrictionsInformations(bool CantBeAggressed, bool CantBeChallenged, bool CantTrade, bool CantBeAttackedByMutant, bool CantRun, bool ForceSlowWalk, bool CantMinimize, bool CantMove, bool CantAggress, bool CantChallenge, bool CantExchange, bool CantAttack, bool CantChat, bool CantBeMerchant, bool CantUseObject, bool CantUseTaxCollector, bool CantUseInteractive, bool CantSpeakToNPC, bool CantChangeZone, bool CantAttackMonster)
	{
		this.CantBeAggressed = CantBeAggressed;
		this.CantBeChallenged = CantBeChallenged;
		this.CantTrade = CantTrade;
		this.CantBeAttackedByMutant = CantBeAttackedByMutant;
		this.CantRun = CantRun;
		this.ForceSlowWalk = ForceSlowWalk;
		this.CantMinimize = CantMinimize;
		this.CantMove = CantMove;
		this.CantAggress = CantAggress;
		this.CantChallenge = CantChallenge;
		this.CantExchange = CantExchange;
		this.CantAttack = CantAttack;
		this.CantChat = CantChat;
		this.CantBeMerchant = CantBeMerchant;
		this.CantUseObject = CantUseObject;
		this.CantUseTaxCollector = CantUseTaxCollector;
		this.CantUseInteractive = CantUseInteractive;
		this.CantSpeakToNPC = CantSpeakToNPC;
		this.CantChangeZone = CantChangeZone;
		this.CantAttackMonster = CantAttackMonster;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		byte box = 0;
		box = BooleanByteWrapper.SetFlag(box, 0, CantBeAggressed);
		box = BooleanByteWrapper.SetFlag(box, 1, CantBeChallenged);
		box = BooleanByteWrapper.SetFlag(box, 2, CantTrade);
		box = BooleanByteWrapper.SetFlag(box, 3, CantBeAttackedByMutant);
		box = BooleanByteWrapper.SetFlag(box, 4, CantRun);
		box = BooleanByteWrapper.SetFlag(box, 5, ForceSlowWalk);
		box = BooleanByteWrapper.SetFlag(box, 6, CantMinimize);
		box = BooleanByteWrapper.SetFlag(box, 7, CantMove);
		writer.WriteByte(box);
		box = 0;
		box = BooleanByteWrapper.SetFlag(box, 0, CantAggress);
		box = BooleanByteWrapper.SetFlag(box, 1, CantChallenge);
		box = BooleanByteWrapper.SetFlag(box, 2, CantExchange);
		box = BooleanByteWrapper.SetFlag(box, 3, CantAttack);
		box = BooleanByteWrapper.SetFlag(box, 4, CantChat);
		box = BooleanByteWrapper.SetFlag(box, 5, CantBeMerchant);
		box = BooleanByteWrapper.SetFlag(box, 6, CantUseObject);
		box = BooleanByteWrapper.SetFlag(box, 7, CantUseTaxCollector);
		writer.WriteByte(box);
		box = 0;
		box = BooleanByteWrapper.SetFlag(box, 0, CantUseInteractive);
		box = BooleanByteWrapper.SetFlag(box, 1, CantSpeakToNPC);
		box = BooleanByteWrapper.SetFlag(box, 2, CantChangeZone);
		box = BooleanByteWrapper.SetFlag(box, 3, CantAttackMonster);
		writer.WriteByte(box);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		byte box = reader.ReadByte();
		this.CantBeAggressed = BooleanByteWrapper.GetFlag(box, 0);
		this.CantBeChallenged = BooleanByteWrapper.GetFlag(box, 1);
		this.CantTrade = BooleanByteWrapper.GetFlag(box, 2);
		this.CantBeAttackedByMutant = BooleanByteWrapper.GetFlag(box, 3);
		this.CantRun = BooleanByteWrapper.GetFlag(box, 4);
		this.ForceSlowWalk = BooleanByteWrapper.GetFlag(box, 5);
		this.CantMinimize = BooleanByteWrapper.GetFlag(box, 6);
		this.CantMove = BooleanByteWrapper.GetFlag(box, 7);
		box = reader.ReadByte();
		this.CantAggress = BooleanByteWrapper.GetFlag(box, 0);
		this.CantChallenge = BooleanByteWrapper.GetFlag(box, 1);
		this.CantExchange = BooleanByteWrapper.GetFlag(box, 2);
		this.CantAttack = BooleanByteWrapper.GetFlag(box, 3);
		this.CantChat = BooleanByteWrapper.GetFlag(box, 4);
		this.CantBeMerchant = BooleanByteWrapper.GetFlag(box, 5);
		this.CantUseObject = BooleanByteWrapper.GetFlag(box, 6);
		this.CantUseTaxCollector = BooleanByteWrapper.GetFlag(box, 7);
		box = reader.ReadByte();
		this.CantUseInteractive = BooleanByteWrapper.GetFlag(box, 0);
		this.CantSpeakToNPC = BooleanByteWrapper.GetFlag(box, 1);
		this.CantChangeZone = BooleanByteWrapper.GetFlag(box, 2);
		this.CantAttackMonster = BooleanByteWrapper.GetFlag(box, 3);
	}
}
}
