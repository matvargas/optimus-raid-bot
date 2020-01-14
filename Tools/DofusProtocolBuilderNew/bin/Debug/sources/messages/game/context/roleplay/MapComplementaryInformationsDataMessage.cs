using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class MapComplementaryInformationsDataMessage : NetworkMessage
{

	public const uint Id = 226;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short SubAreaId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public double MapId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public HouseInformations[] Houses { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public GameRolePlayActorInformations[] Actors { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public InteractiveElement[] InteractiveElements { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public StatedElement[] StatedElements { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public MapObstacle[] Obstacles { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public FightCommonInformations[] Fights { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool HasAggressiveMonsters { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public FightStartingPositions FightStartPositions { get; set; }

	public MapComplementaryInformationsDataMessage() {}


	public MapComplementaryInformationsDataMessage InitMapComplementaryInformationsDataMessage(short SubAreaId, double MapId, HouseInformations[] Houses, GameRolePlayActorInformations[] Actors, InteractiveElement[] InteractiveElements, StatedElement[] StatedElements, MapObstacle[] Obstacles, FightCommonInformations[] Fights, bool HasAggressiveMonsters, FightStartingPositions FightStartPositions)
	{
		this.SubAreaId = SubAreaId;
		this.MapId = MapId;
		this.Houses = Houses;
		this.Actors = Actors;
		this.InteractiveElements = InteractiveElements;
		this.StatedElements = StatedElements;
		this.Obstacles = Obstacles;
		this.Fights = Fights;
		this.HasAggressiveMonsters = HasAggressiveMonsters;
		this.FightStartPositions = FightStartPositions;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteVarShort(this.SubAreaId);
		writer.WriteDouble(this.MapId);
		writer.WriteShort(this.Houses.Length);
		foreach (HouseInformations item in this.Houses)
		{
			writer.WriteShort(item.MessageId);
			item.Serialize(writer);
		}
		writer.WriteShort(this.Actors.Length);
		foreach (GameRolePlayActorInformations item in this.Actors)
		{
			writer.WriteShort(item.MessageId);
			item.Serialize(writer);
		}
		writer.WriteShort(this.InteractiveElements.Length);
		foreach (InteractiveElement item in this.InteractiveElements)
		{
			writer.WriteShort(item.MessageId);
			item.Serialize(writer);
		}
		writer.WriteShort(this.StatedElements.Length);
		foreach (StatedElement item in this.StatedElements)
		{
			item.Serialize(writer);
		}
		writer.WriteShort(this.Obstacles.Length);
		foreach (MapObstacle item in this.Obstacles)
		{
			item.Serialize(writer);
		}
		writer.WriteShort(this.Fights.Length);
		foreach (FightCommonInformations item in this.Fights)
		{
			item.Serialize(writer);
		}
		writer.WriteBoolean(this.HasAggressiveMonsters);
		this.FightStartPositions.Serialize(writer);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.SubAreaId = reader.ReadVarShort();
		this.MapId = reader.ReadDouble();
		int HousesLen = reader.ReadShort();
		Houses = new HouseInformations[HousesLen];
		for (int i = 0; i < HousesLen; i++)
		{
			this.Houses[i] = ProtocolTypeManager.GetInstance<HouseInformations>(reader.ReadShort());
			this.Houses[i].Deserialize(reader);
		}
		int ActorsLen = reader.ReadShort();
		Actors = new GameRolePlayActorInformations[ActorsLen];
		for (int i = 0; i < ActorsLen; i++)
		{
			this.Actors[i] = ProtocolTypeManager.GetInstance<GameRolePlayActorInformations>(reader.ReadShort());
			this.Actors[i].Deserialize(reader);
		}
		int InteractiveElementsLen = reader.ReadShort();
		InteractiveElements = new InteractiveElement[InteractiveElementsLen];
		for (int i = 0; i < InteractiveElementsLen; i++)
		{
			this.InteractiveElements[i] = ProtocolTypeManager.GetInstance<InteractiveElement>(reader.ReadShort());
			this.InteractiveElements[i].Deserialize(reader);
		}
		int StatedElementsLen = reader.ReadShort();
		StatedElements = new StatedElement[StatedElementsLen];
		for (int i = 0; i < StatedElementsLen; i++)
		{
			this.StatedElements[i] = new StatedElement();
			this.StatedElements[i].Deserialize(reader);
		}
		int ObstaclesLen = reader.ReadShort();
		Obstacles = new MapObstacle[ObstaclesLen];
		for (int i = 0; i < ObstaclesLen; i++)
		{
			this.Obstacles[i] = new MapObstacle();
			this.Obstacles[i].Deserialize(reader);
		}
		int FightsLen = reader.ReadShort();
		Fights = new FightCommonInformations[FightsLen];
		for (int i = 0; i < FightsLen; i++)
		{
			this.Fights[i] = new FightCommonInformations();
			this.Fights[i].Deserialize(reader);
		}
		this.HasAggressiveMonsters = reader.ReadBoolean();
		this.FightStartPositions = new FightStartingPositions();
		this.FightStartPositions.Deserialize(reader);
	}
}
}
