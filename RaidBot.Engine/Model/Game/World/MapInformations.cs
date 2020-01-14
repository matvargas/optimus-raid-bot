using RaidBot.Data.IO.D2P.Map;
using RaidBot.Engine.Dispatcher;
using RaidBot.Engine.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RaidBot.Data.GameData;
using System.Threading.Tasks;
using RaidBot.Protocol.DataCenter;
using RaidBot.Engine.Enums;
using RaidBot.Data.MapData;
using RaidBot.Engine.Model.Game.World.Actor;
using RaidBot.Engine.Model.Game.World.Actor.Characters;
using RaidBot.Engine.Model.Game.World.Actor.Npcs;
using RaidBot.Engine.Model.Game.World.Actor.Monsters;
using RaidBot.Engine.Model.Game.World.Actor.Interactives;
using RaidBot.Data.IO.D2P.Map.Elements;
using RaidBot.Protocol.Messages;
using RaidBot.Protocol.Types;

namespace RaidBot.Engine.Model.Game.World
{
    public class MapInformations:ModelBase
    {
      
        private ConnectedHost mHost;
        private int mWortldId;
        public int WorldId
        {
            get
            {
                return mWortldId;
               
             
            }
            set
            {
                mWortldId = value;
            }
        }
        private int mX;
        public int X
        {
            get
            {
                return mX;
            }
            set
            {
                mX = value;
            }
        }

        private int mY;
        public int Y
        {
            get
            {
                return mY;
            }
            set
            {
                mY = value;
            }
           
        }

        private Map mData;
        public Map Data
        {
            get { return mData; }
            set
            {
                mData = value;
                Notify();
            }
        }


        private Dictionary<int, ActorModel> mActors;
        public Dictionary<int, ActorModel> Actors
        {
            get { return mActors; }
            set
            {
                mActors = value;
                Notify();
                
            }
        }

        private Dictionary<int, CharacterModel> mCharacters;
        public Dictionary<int,CharacterModel>Characters
        {
            get { return mCharacters; }
            set
            {
                mCharacters = value;
                Notify();
            }
        }
        private Dictionary<int, ElementModel> mInteractives;
        public Dictionary<int, ElementModel> Interactives
        {
            get { return mInteractives; }
            set
            {
               mInteractives = value;
                Notify();
            }
        }
        private Dictionary<int, ElementModel> mDoors;
        public Dictionary<int, ElementModel> Doors
        {
            get { return mDoors; }
            set
            {
                mDoors = value;
                Notify();
            }
        }

        private Dictionary<int, NpcModel> mNpcs;
        public Dictionary<int, NpcModel> Npcs
        {
            get { return mNpcs; }
            set
            {
                mNpcs = value;
                Notify();
            }
        }

        private Dictionary<int, GroupOfMonstersModel> mMonsters;
        public Dictionary<int, GroupOfMonstersModel> Monsters
        {
            get { return mMonsters; }
            set
            {
                mMonsters = value;
                Notify();
            }
        }

        public MapInformations(ConnectedHost host)
        {
            mHost = host;
            mHost.Dispatcher.Register(this);
        }

        [MessageHandlerAttribut(typeof(MapComplementaryInformationsDataMessage))]
        private void HandleMapComplementaryInformationsDataMessage(MapComplementaryInformationsDataMessage message,ConnectedHost source)
        {
            Data = MapDataAdapter.GetMap(message.mapId);
            Actors = new Dictionary<int, ActorModel>();
            Monsters = new Dictionary<int, GroupOfMonstersModel>();
            Characters = new Dictionary<int, CharacterModel>();
            Npcs = new Dictionary<int, NpcModel>();
            Interactives = new Dictionary<int, ElementModel>();
            Doors = new Dictionary<int, ElementModel>();
            X = GameDataAdapter.GetClass<MapPosition>((int)Data.Id).PosX;
            Y = GameDataAdapter.GetClass<MapPosition>((int)Data.Id).PosY;
            WorldId = GameDataAdapter.GetClass<MapPosition>((int)Data.Id).WorldMap;
            foreach (var mess in message.actors)
            {
                switch(mess.TypeId)
                {
                    case GameRolePlayNpcInformations.Id:
                        Npcs.Add(mess.contextualId, new NpcModel((GameRolePlayNpcInformations)mess));
                        break;
                    case GameRolePlayNpcWithQuestInformations.Id:
                        Npcs.Add(mess.contextualId, new NpcModel((GameRolePlayNpcInformations)mess));
                        break;
                    case GameRolePlayCharacterInformations.Id:
                        Characters.Add(mess.contextualId, new CharacterModel((GameRolePlayCharacterInformations)mess));
                        break;
                    case GameRolePlayGroupMonsterInformations.Id:
                        Monsters.Add(mess.contextualId, new GroupOfMonstersModel((GameRolePlayGroupMonsterInformations)mess));
                        break;
                }
                Actors.Add(mess.contextualId, new ActorModel((GameRolePlayActorInformations)mess));
            }
            foreach (var element in message.interactiveElements)
            {
                Interactives.Add(element.elementId, new ElementModel(element));
                InteractiveElement interactiveElement = element;
                List<int> listDoorSkillId = new List<int>(new[] { 184, 183, 187, 198, 114 });
                List<int> listDoorTypeId = new List<int>(new[] { -1, 128, 168, 16 });
                if (listDoorTypeId.Contains(interactiveElement.elementTypeId) && (interactiveElement.enabledSkills.Length > 0) && (listDoorSkillId.Contains((int)interactiveElement.enabledSkills[0].skillId)))
                {
                    foreach (var layer in Data.Layers)
                    {
                        foreach (var cell in layer.cells)
                        {
                            foreach (var layerElement in cell.elements)
                            {
                                if (layerElement is GraphicalElement)
                                {
                                    GraphicalElement graphicalElement = (GraphicalElement)layerElement;
                                    if ((graphicalElement.identifier == interactiveElement.elementId) && !Doors.ContainsKey(cell.cellId))
                                        Doors.Add(element.elementId, new ElementModel(element));
                                }
                            }
                        }
                    }
                }

            }
            foreach (StatedElement element in message.statedElements)
            {
                ElementModel value;
                if (Interactives.TryGetValue(element.elementId , out value ))
                {
                    value.Update(element);
                }
            }
            source.Bot.Game.Player.mPathFinder.SetMap(this, false);
            OnUpdated();
        }
        [MessageHandlerAttribut(typeof(GameMapMovementMessage))]
        private void HandleMapComplementaryInformationsDataMessage(GameMapMovementMessage message, ConnectedHost source)
        {
            GroupOfMonstersModel value;
            if (Monsters.TryGetValue(message.actorId ,out value))
            {
                value.CellId = message.keyMovements[message.keyMovements.Length - 1];
            }

            CharacterModel mvalue;
            if (Characters.TryGetValue(message.actorId, out mvalue))
            {
                mvalue.CellId = message.keyMovements[message.keyMovements.Length - 1];
            }
            ActorModel _value;
            if (Actors.TryGetValue(message.actorId, out _value))
            {
                _value.CellId = message.keyMovements[message.keyMovements.Length - 1];
            }
            OnUpdated();
        }
        [MessageHandlerAttribut(typeof(InteractiveMapUpdateMessage))]
        private void HandleMapComplementaryInformationsDataMessage(InteractiveMapUpdateMessage message, ConnectedHost source)
        {

            foreach(InteractiveElement element in message.interactiveElements)
            {
                ElementModel value;
               if( Interactives.TryGetValue(element.elementId ,out value ))
                {
                    value.Update(element);
                }
            }
            OnUpdated();
        }
        [MessageHandlerAttribut(typeof(InteractiveElementUpdatedMessage))]
        private void HandleMapComplementaryInformationsDataMessage(InteractiveElementUpdatedMessage message, ConnectedHost source)
        {
            if ( Interactives == null)
            return;
           
                ElementModel value;
                if (Interactives.TryGetValue(message.interactiveElement.elementId, out value))
                {
                    value.Update(message.interactiveElement);
                }
           
            OnUpdated();
        }
        [MessageHandlerAttribut(typeof(StatedMapUpdateMessage))]
        private void HandleMapComplementaryInformationsDataMessage(StatedMapUpdateMessage message, ConnectedHost source)
        {
            foreach (StatedElement element in message.statedElements)
            {
                ElementModel value;
                if (Interactives.TryGetValue(element.elementId, out value))
                {
                    value.Update(element);
                }
            }
            OnUpdated();
        }
        [MessageHandlerAttribut(typeof(StatedElementUpdatedMessage))]
        private void HandleMapComplementaryInformationsDataMessage(StatedElementUpdatedMessage message, ConnectedHost source)
        {
            if (Interactives == null)
                return;
          
                ElementModel value;
                if (Interactives.TryGetValue(message.statedElement.elementId, out value))
                {
                    value.Update(message.statedElement);
                }
           
            OnUpdated();
        }
       
        [MessageHandlerAttribut(typeof(InteractiveUseEndedMessage))]
        private void HandleMapComplementaryInformationsDataMessage(InteractiveUseEndedMessage message, ConnectedHost source)
        {
           
                ElementModel value;
                if (Interactives.TryGetValue((int)message.elemId, out value))
                {
                    value.Enabled = false;
                }
           
            OnUpdated();
        }
       
    }
}
