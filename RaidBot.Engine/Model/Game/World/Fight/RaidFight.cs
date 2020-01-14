using RaidBot.Engine.Controler.Game.Player;
using RaidBot.Engine.Dispatcher;
using RaidBot.Engine.Functionality.Fight;
using RaidBot.Engine.Manager;
using RaidBot.Engine.Model.Game.World;
using RaidBot.Engine.Model.Game.World.Actor.Fight;
using RaidBot.Engine.Model.Game.World.Fight.Enums;
using RaidBot.Engine.Model.Game.World.Fight.IA;
using RaidBot.Engine.Utility.Pathfinding;
using RaidBot.Protocol.Enums;
using RaidBot.Protocol.Messages;
using RaidBot.Protocol.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidBot.Engine.Model.Game.World.Fight
{
    public class RaidFight 
    {
       

        #region fields
        public Pathfinder finder
        {
            get
            {
                return mHost.Bot.Game.Player.mPathFinder;
            }
        }
        public FightPhase Phase;
        public List<FighterModel> Defenders;
        public List<FighterModel> Challengers;
        public FightTypeEnum FightType;
        public GameFightJoinMessage JoinigInfos;
        public FighterModel CurrentPLayingFighter;
        public PlayerControler playedFighter
        {
            get
          {
              if (mHost.Bot.Game.Player == null)
                  return null;
             return mHost.Bot.Game.Player;
          }
        }
        public GameFightPlacementPossiblePositionsMessage Placements;
        public ConnectedHost mHost;
        #endregion

        public RaidFight(GameFightJoinMessage msg , ConnectedHost host , MapInformations CurrentMap)
        {
            if (!(host.Bot.Game.World.Map.Data == null))
            mHost = host;        
            Defenders = new List<FighterModel>();
            Challengers = new List<FighterModel>();
            JoinigInfos = msg;
            FightType = (FightTypeEnum)msg.fightType;
            CurrentMap.Updated += ActualizeMap;
            Challengers.Add(new FighterModel(playedFighter.PlayerBaseInformations.Id, (short)playedFighter.PlayerBaseInformations.CellId, TeamEnum.TEAM_CHALLENGER, this));
        }

        public void Update(GameFightPlacementPossiblePositionsMessage msg)
        {
            Placements = msg;
            playedFighter.PlayerFightInformations.TeamSide = (TeamEnum)msg.teamNumber;
        }
        public void Update(GameMapMovementMessage message)
        {
        foreach (FighterModel fighter in Defenders)
        {
            if (fighter.ContextualId == message.actorId)
            {
                fighter.CellId = message.keyMovements.Last();
                break;
            }
        }
        foreach (FighterModel fighter in Challengers)
        {
            if (fighter.ContextualId == message.actorId)
            {
                fighter.CellId = message.keyMovements.Last();
                break;
            }
        }
        }
        public void Update(GameActionFightDeathMessage message)
        {
            if(Defenders.Any(fighter => fighter.ContextualId == message.targetId))
            Defenders.Where(fighter => fighter.ContextualId == message.targetId).FirstOrDefault().Alive = false;
            if (Challengers.Any(fighter => fighter.ContextualId == message.targetId))
            Challengers.Where(fighter => fighter.ContextualId == message.targetId).FirstOrDefault().Alive = false;    
        }
        public void Update(GameFightSynchronizeMessage message)
        {

            foreach (GameFightFighterInformations infos in message.fighters)
            {
                foreach (FighterModel fighter in Defenders)
                {
                    if (fighter.ContextualId == infos.contextualId)
                    {
                        fighter.Team = (TeamEnum)infos.teamId;
                        fighter.Alive = infos.alive;
                        fighter.CellId = infos.disposition.cellId;
                        if (fighter.Team == TeamEnum.TEAM_CHALLENGER && !Challengers.Contains(fighter))
                        {
                            Challengers.Add(fighter);
                            Defenders.Remove(fighter);
                        }
                        return;
                    }
                }
                foreach (FighterModel fighter in Challengers)
                {
                    if (fighter.ContextualId == infos.contextualId)
                    {
                        fighter.Team = (TeamEnum)infos.teamId;
                        fighter.Alive = infos.alive;
                        fighter.CellId = infos.disposition.cellId;
             
                      if (fighter.Team == TeamEnum.TEAM_CHALLENGER && !Defenders.Contains(fighter))
                            Defenders.Add(fighter);
                            Challengers.Remove(fighter);
                        return;
                    }
                }
                FighterModel ft = new FighterModel(infos.contextualId, infos.disposition.cellId, (TeamEnum)infos.teamId, this);
                if (ft.Team == TeamEnum.TEAM_DEFENDER)
                    Defenders.Add(ft);
                else if (ft.Team == TeamEnum.TEAM_CHALLENGER)
                    Challengers.Add(ft);
            }
        }
        public void Update(GameEntitiesDispositionMessage message)
        {
            foreach (IdentifiedEntityDispositionInformations infos in message.dispositions)
            {
                foreach (FighterModel fighter in Defenders)
                {
                    if (fighter.ContextualId == infos.id)
                    {
                        fighter.CellId = infos.cellId;
                       
                        break;
                    }
                }
                foreach (FighterModel fighter in Challengers)
                {
                    if (fighter.ContextualId == infos.id)
                    {
                        fighter.CellId = infos.cellId;
                        break;
                    }
                }
            }
        }
       
        public void ActualizeMap(object sender , EventArgs e)
        {
            if (!(((MapInformations)sender).Data == null))
            finder.SetMap((MapInformations)sender, false);
        }
        public void AddFighter(GameFightFighterInformations msg)
        {
            if(msg.teamId == 0)
            {
                Challengers.Add( new FighterModel(msg.contextualId , msg.disposition.cellId , (TeamEnum)msg.teamId , this));
            }
            else if (msg.teamId == 1)
            {
                Defenders.Add(new FighterModel(msg.contextualId , msg.disposition.cellId , (TeamEnum)msg.teamId , this));
            }
        }
       
        public List<FighterModel> GetEnnemies(int FighterId)
        {
           
           if (Defenders.Any(fighter => fighter.ContextualId == FighterId))
                return Challengers;
            else if (Challengers.Any(fighter => fighter.ContextualId == FighterId))
               return Defenders;
           mHost.logger.Log("le joueur " + FighterId + " ne figure pas dans la liste des combatants , il se peut qu'il soit un spectateur");
           return null;
        }
        public List<FighterModel> GetTeam(int FighterId)
        {
            if (Defenders.Any(fighter => fighter.ContextualId == FighterId))
                return Defenders;
            else if (Challengers.Any(fighter => fighter.ContextualId == FighterId))
                return Challengers;
            mHost.logger.Log("le joueur " + FighterId + " ne figure pas dans la liste des combatants , il se peut qu'il soit un spectateur");
            return null;
        }
        public List<short> GetEnnemiesCells(int FighterId)
        {
            if (Defenders.Any(fighter => fighter.ContextualId == FighterId))
                return (List<short>)Challengers.Select(fighter => fighter.CellId);
            else if (Challengers.Any(fighter => fighter.ContextualId == FighterId))
                return (List<short>)Defenders.Select(fighter => fighter.CellId);
            mHost.logger.Log("le joueur " + FighterId + " ne figure pas dans la liste des combatants , il se peut qu'il soit un spectateur");
            return null;
        }
        public List<short> GetTeamCells(int FighterId)
        {
            if (Defenders.Any(fighter => fighter.ContextualId == FighterId))
                return (List<short>)Defenders.Select(fighter => fighter.CellId);
            else if (Challengers.Any(fighter => fighter.ContextualId == FighterId))
                return (List<short>)Challengers.Select(fighter => fighter.CellId);
            mHost.logger.Log("le joueur " + FighterId + " ne figure pas dans la liste des combatants , il se peut qu'il soit un spectateur");
            return null;
        }

        public List<FighterModel> GetEnnemies()
        {
            if (playedFighter.PlayerFightInformations.TeamSide == TeamEnum.TEAM_CHALLENGER)
                return Defenders;
            else if ((playedFighter.PlayerFightInformations.TeamSide == TeamEnum.TEAM_DEFENDER))
                return Challengers;
            
            return new List<FighterModel>(); 
        }
        public List<FighterModel> GetTeam()
        {
            if (playedFighter.PlayerFightInformations.TeamSide == TeamEnum.TEAM_CHALLENGER)
                return Challengers;
            else if ((playedFighter.PlayerFightInformations.TeamSide == TeamEnum.TEAM_DEFENDER))
                return Defenders;
            return null;
        }
        public IEnumerable<short> GetEnnemiesCells()
        {
            if (playedFighter.PlayerFightInformations.TeamSide == TeamEnum.TEAM_CHALLENGER)
                return Defenders.Select(fighter => fighter.CellId);
            else if ((playedFighter.PlayerFightInformations.TeamSide == TeamEnum.TEAM_DEFENDER))
                return Challengers.Select(fighter => fighter.CellId);
          
            return null;
        }
        public IEnumerable<short> GetTeamCells()
        {
			if (playedFighter.PlayerFightInformations.TeamSide == TeamEnum.TEAM_CHALLENGER) {
				return (List<short>)Challengers.Select (fighter => fighter.CellId);
			}
            else if ((playedFighter.PlayerFightInformations.TeamSide == TeamEnum.TEAM_DEFENDER))
                return (List<short>)Defenders.Select(fighter => fighter.CellId);
            return null;
        }
		public FighterModel GetNearestEnnemie()
		{
			return GetEnnemies ().OrderBy (ennemie => finder.matrix [ennemie.CellId].DistanceTo (finder.matrix [playedFighter.PlayerBaseInformations.CellId])).FirstOrDefault();
		}
		public FighterModel GetFarthestEnnemie()
		{
			return GetEnnemies ().OrderBy (ennemie => finder.matrix [ennemie.CellId].DistanceTo (finder.matrix [playedFighter.PlayerBaseInformations.CellId])).LastOrDefault();
		}

        
       
       
    }
}
