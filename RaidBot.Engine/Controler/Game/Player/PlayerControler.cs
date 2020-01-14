using RaidBot.Common.Default.Loging;
using RaidBot.Data.IO.D2P.Map;
using RaidBot.Engine.Controler.Game.World;
using RaidBot.Engine.Frames.Game.Player;
using RaidBot.Engine.Manager;
using RaidBot.Engine.Model.Game.Player;
using RaidBot.Engine.Model.Game.Player.Characteristics;
using RaidBot.Engine.Model.Game.Player.Inventory;
using RaidBot.Engine.Model.Game.Player.Job;
using RaidBot.Engine.Utility.Pathfinding;
using RaidBot.Protocol.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RaidBot.Engine.Model.Game.Player.Spells;
using RaidBot.Protocol.Messages;
using RaidBot.Engine.Functionality.Fight;
using RaidBot.Engine.Model.Game.Player.Fight;

namespace RaidBot.Engine.Controler.Game.Player
{
    public class PlayerControler
    {
        public ConnectedHost mHost;
        public Pathfinder mPathFinder;
        public WorldControler mWorld;
        public MoveFrame mMoveFrame;
        public PlayerBaseInformationsModel PlayerBaseInformations { get; private set; }
        public PlayerCharacteristicsModel PlayerCharacteristics { get; private set; }
        public PlayerInventoryModel PlayerInventory { get; private set; }
        public PlayerSpellsModel PlayerSpells { get; private set; }
        public PlayerJobsModel PlayerJobs { get; private set; }
        public PlayerFightInformationsModel PlayerFightInformations { get; private set; }
        public FightingFrame fightManager { get; private set; }

        public PlayerControler(ConnectedHost host,WorldControler world)
        {
            mHost = host;
            mWorld = world;
            mPathFinder = new Pathfinder();
            PlayerBaseInformations = new PlayerBaseInformationsModel(mHost);
            PlayerCharacteristics = new PlayerCharacteristicsModel(mHost);
            PlayerInventory = new PlayerInventoryModel(mHost);
            PlayerSpells = new PlayerSpellsModel(mHost);
            PlayerJobs = new PlayerJobsModel(mHost);
            mMoveFrame = new MoveFrame(mHost);
            PlayerFightInformations = new PlayerFightInformationsModel(mHost);

        }

        public void Move(short cellId,bool diagonal=false)
        {
            mMoveFrame.Move(cellId, (short)PlayerBaseInformations.CellId, diagonal, mWorld);
        }

        public void Move(DirectionsEnum direction)
        {
            mMoveFrame.Move(direction, mWorld, (short)PlayerBaseInformations.CellId);
        }
        public void SendMessage(string message)
        {
            SendTextInformation(TextInformationTypeEnum.TEXT_INFORMATION_MESSAGE, 0, message);
        }

        #region Private method
        private void SendTextInformation(TextInformationTypeEnum type, ushort id, params object[] parameters)
        {
            mHost.SendMessage((new TextInformationMessage((sbyte)type, id, parameters.Select(entry => entry.ToString()).ToArray())), Enums.DestinationEnum.CLIENT);

        }
        #endregion
    }
}
