using RaidBot.Engine.Controler.Game.Player;
using RaidBot.Engine.Utility.Pathfinding;
using RaidBot.Protocol.Enums;
using RaidBot.Protocol.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidBot.Engine.Model.Game.World.Fight.IA
{
    public class Placement
    {
        public Brain mBrain;

		RaidFight Fight
		{
			get
			{
				return mBrain.Fight;
			}
		}
        PlayerControler Player
        {
            get
            {
                return Fight.playedFighter;
            }
        }
        public Placement(Brain brain)
        {
			mBrain = brain;

        }
        public void Place()
        {
            Cell currentCell = Fight.finder.matrix[Player.PlayerBaseInformations.CellId];
            List<Cell> TeamCells =  new List<Cell>();
            foreach (short cellid in this.GetTeamCells())
            {
                TeamCells.Add(Fight.finder.matrix[cellid]);
            }
            List<Cell> EnnemiesCells =  new List<Cell>();
            foreach (short cellid in this.GetEnnemiesCells())
            {
                EnnemiesCells.Add(Fight.finder.matrix[cellid]);
            }
            switch (Player.PlayerFightInformations.IA)
            {
                case Enums.IAEnum.Fuyard:
                    Cell farCell = TeamCells.OrderBy(cell => cell.ManhattanDistanceTo( EnnemiesCells.OrderBy(mcell => mcell.ManhattanDistanceTo(currentCell)).LastOrDefault())).LastOrDefault();
                  ChangePrePlacement(farCell);
                    break;
                case Enums.IAEnum.Passif:
                    break;
                case Enums.IAEnum.Offensif:
                 Cell NearbyCell = TeamCells.OrderBy(cell => cell.ManhattanDistanceTo( EnnemiesCells.OrderBy(mcell => mcell.ManhattanDistanceTo(currentCell)).LastOrDefault())).FirstOrDefault();
                   ChangePrePlacement(NearbyCell);
                    break;
            }
            Ready();
        }
        public void ChangePrePlacement(Cell cell)
        {
            Fight.mHost.SendMessage(new GameFightPlacementPositionRequestMessage((ushort)cell.Id), Engine.Enums.DestinationEnum.SERVER);
        }
       
        public void Ready()
        {
            
            Fight.mHost.SendMessage(new GameFightReadyMessage(true));
        }
        public List<ushort> GetEnnemiesCells()
        {
            if (Fight.playedFighter.PlayerFightInformations.TeamSide == TeamEnum.TEAM_CHALLENGER)
                return Fight.Placements.positionsForDefenders.ToList();
            else if ((Fight.playedFighter.PlayerFightInformations.TeamSide == TeamEnum.TEAM_DEFENDER))
                return Fight.Placements.positionsForChallengers.ToList();

            return null;
        }
        public List<ushort> GetTeamCells()
        {
            if (Fight.playedFighter.PlayerFightInformations.TeamSide == TeamEnum.TEAM_CHALLENGER)
                return Fight.Placements.positionsForChallengers.ToList();
            else if ((Fight.playedFighter.PlayerFightInformations.TeamSide == TeamEnum.TEAM_DEFENDER))
                return Fight.Placements.positionsForDefenders.ToList();
            return null;
        }
    }
}
