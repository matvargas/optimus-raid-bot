using RaidBot.Engine.Dispatcher;
using RaidBot.Engine.Manager;
using RaidBot.Engine.Model.Game.World.Actor.Monsters;
using RaidBot.Engine.Model.Game.World.Fight;
using RaidBot.Engine.Model.Game.World.Fight.Enums;
using RaidBot.Engine.Model.Game.World.Fight.IA;
using RaidBot.Protocol.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RaidBot.Engine.Functionality.Fight
{
    public class FightingFrame : MessagesHandler
    {
        #region Fields
	   bool mStartingFight = false;
       int AttackedMonstersId;
       public RaidFight fight;
       public ConnectedHost mHost;
	   public Brain IA;
        #endregion

        #region Constructeurs
        public FightingFrame(ConnectedHost host)
        {
            host.Dispatcher.Register(this);
            mHost = host;

        }
        #endregion

        #region Events
     
        #endregion

        #region Public methodes
       public void LaunchFinght(GroupOfMonstersModel monsters)
        {
			if (monsters == null)
				return;
            AttackedMonstersId = monsters.ContextualId;
            mHost.logger.Log("Attacking monster id : " + AttackedMonstersId , Common.Default.Loging.LogLevelEnum.Succes);
            mStartingFight = true;
           mHost.Bot.Game.Player.mMoveFrame.Move(monsters.CellId, (short)mHost.Bot.Game.Player.PlayerBaseInformations.CellId, true, mHost.Bot.Game.World);
         
        }
        public void LaunchLittleFight()
       {
         GroupOfMonstersModel monsters =  mHost.Bot.Game.World.Map.Monsters.OrderBy(group => group.Value.Monsters.Count).FirstOrDefault().Value;
         LaunchFinght(monsters);
       }



        #endregion

        #region Private methodes

        #endregion

        #region Handlers
        [MessageHandlerAttribut(typeof(GameFightJoinMessage))]
        private void HandleMapComplementaryInformationsDataMessage(GameFightJoinMessage message, ConnectedHost source)
        {
            fight = new RaidFight(message , source , source.Bot.Game.World.Map);
			IA = new Brain (fight);
            source.Bot.Game.Player.SendMessage("Fight starting...");
            source.Bot.BotState = Engine.Enums.BotStatsEnum.FIGHTING;
        }
        [MessageHandlerAttribut(typeof(GameMapMovementMessage))]
        private void HandleMapComplementaryInformationsDataMessage(GameMapMovementMessage message, ConnectedHost source)
        {
            if (fight != null)
            fight.Update(message);
        }

		private bool mConfirmed = false;
		[MessageHandlerAttribut(typeof(GameMapMovementConfirmMessage))]
		private void HandleGameMapMovementConfirmMessage(GameMapMovementConfirmMessage message,ConnectedHost source)
		{
			if (mStartingFight)
			{
				mConfirmed = true;
				mStartingFight = false;
			}
		}

		[MessageHandlerAttribut(typeof(BasicAckMessage))]
		private void HandleBasicAckMessage(BasicAckMessage message,ConnectedHost source)
		{
			if(mConfirmed)
			{
				mConfirmed = false;
				GameRolePlayAttackMonsterRequestMessage newMessage = new GameRolePlayAttackMonsterRequestMessage();
				newMessage.monsterGroupId = AttackedMonstersId;
				mHost.SendMessage(newMessage);

			}
		}
       
        [MessageHandlerAttribut(typeof(GameFightSynchronizeMessage))]
        private void HandleMapComplementaryInformationsDataMessage(GameFightSynchronizeMessage message, ConnectedHost source)
        {
            fight.Update(message);
        }
        [MessageHandlerAttribut(typeof(GameActionFightDeathMessage))]
        private void HandleMapComplementaryInformationsDataMessage(GameActionFightDeathMessage message, ConnectedHost source)
        {
            fight.Update(message);
        }
        [MessageHandlerAttribut(typeof(GameEntitiesDispositionMessage))]
        private void HandleMapComplementaryInformationsDataMessage(GameEntitiesDispositionMessage message, ConnectedHost source)
        {
            fight.Update(message);
        }
        [MessageHandlerAttribut(typeof(GameFightShowFighterMessage))]
        private void HandleMapComplementaryInformationsDataMessage(GameFightShowFighterMessage message, ConnectedHost source)
        {
            fight.AddFighter(message.informations);
        }
        [MessageHandlerAttribut(typeof(GameFightTurnStartPlayingMessage))]
        private void HandleMapComplementaryInformationsDataMessage(GameFightTurnStartPlayingMessage message, ConnectedHost source)
        {
			IA.SpellsLauncher.StartFight();
        }
        [MessageHandlerAttribut(typeof(GameFightPlacementPossiblePositionsMessage))]
        private void HandleMapComplementaryInformationsDataMessage(GameFightPlacementPossiblePositionsMessage message, ConnectedHost source)
        {
            fight.Phase = FightPhase.Placement;
            fight.Update(message);           
			IA.Placement.Place ();      
        }
       


        
        #endregion

    }
}
