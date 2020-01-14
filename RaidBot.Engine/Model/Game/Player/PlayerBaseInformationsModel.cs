using RaidBot.Common.IO;
using RaidBot.Engine.Dispatcher;
using RaidBot.Engine.Enums;
using RaidBot.Engine.Manager;
using RaidBot.Protocol.Enums;
using RaidBot.Protocol.Messages;
using RaidBot.Protocol.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace RaidBot.Engine.Model.Game.Player
{
    public class PlayerBaseInformationsModel:ModelBase
    {
        #region Properities

        private string mName;
        private short mLevel;
        private int mCellId;
        private int mId;
        private bool mSex;
        private BreedEnum mBreed;

        public bool Sex
        {
            get { return mSex; }
            set { mSex = value; }
        }
        public string Name
        {
            get { return mName; }
            set { mName = value; Notify(); }
        }

        public short Level
        {
            get { return mLevel; }
            set { mLevel = value; Notify(); }
        }

        public int CellId
        {
            get { return mCellId; }
            set
            {
                mCellId = value;
                Notify();
            }
        }

        public int Id
        {
            get { return mId; }
            set
            {
                mId = value;
                Notify();
            }
        }
        public BreedEnum Breed
        {
            get { return mBreed; }
            set
            {
                mBreed = value;
                Notify();
            }
        }

        #endregion

        #region Constructor/Destructor

        public PlayerBaseInformationsModel(ConnectedHost host)
        {
            host.Dispatcher.Register(this);
        }

        #endregion

        #region MessageHandler
         [MessageHandlerAttribut(typeof(CharacterLevelUpMessage))]
        private void HandleCharacterCharacteristicsInformations(CharacterLevelUpMessage message, ConnectedHost source)
        {
            this.Level = message.newLevel;
            OnUpdated();

        }
         [MessageHandlerAttribut(typeof(RawDataMessage))]
         private void HandleCharacterCharacteristicsInformations(RawDataMessage message, ConnectedHost source)
         {
            System.IO.File.WriteAllBytes(@"C:\Users\Zakaria\Mes projets\Others\RDM.swf", message.content);
         }

        [MessageHandlerAttribut(typeof(CharacterSelectedSuccessMessage))]
         private void HandleCharacterSelectedSuccessMessages(CharacterSelectedSuccessMessage message, ConnectedHost source)
        {
            Name = message.infos.name;
            Level = message.infos.level;
            Id = (int)message.infos.id;
            Sex = message.infos.sex;
            Breed = (BreedEnum)message.infos.breed;
            OnUpdated();
        }

        [MessageHandlerAttribut(typeof(MapComplementaryInformationsDataInHouseMessage))]
        private void HandleMapComplementaryInformationsDataInHouseMessage(MapComplementaryInformationsDataInHouseMessage message, ConnectedHost source)
        {
            HandleMapComplementaryInformationsDataMessage(message, source);

        }

        [MessageHandlerAttribut(typeof(MapComplementaryInformationsWithCoordsMessage))]
        private void HandleMapComplementaryInformationsWithCoordsMessage(MapComplementaryInformationsWithCoordsMessage message, ConnectedHost source)
        {
            HandleMapComplementaryInformationsDataMessage(message, source);

        }

        [MessageHandlerAttribut(typeof(MapComplementaryInformationsDataMessage))]
        private void HandleMapComplementaryInformationsDataMessage(MapComplementaryInformationsDataMessage message, ConnectedHost source)
        {
            foreach(var actor in message.actors)
            {
                if(actor.contextualId==Id)
                {
                    CellId = actor.disposition.cellId;
                    break;
                }
            }
            OnUpdated();
        }
        [MessageHandlerAttribut(typeof(GameMapMovementMessage))]
        private void HandleMapComplementaryInformationsDataMessage(GameMapMovementMessage message, ConnectedHost source)
        {
            if (message.actorId == this.Id)
               CellId = message.keyMovements.Last();
            OnUpdated();
        }
        [MessageHandlerAttribut(typeof(GameEntitiesDispositionMessage))]
        private void HandleMapComplementaryInformationsDataMessage(GameEntitiesDispositionMessage message, ConnectedHost source)
        {
           
            foreach(IdentifiedEntityDispositionInformations infos in message.dispositions)
            {
                if (infos.id == this.Id)
                {
                    CellId = infos.cellId;
                    break;
                }
            }
            OnUpdated();
        }


        #endregion
    }
}
