using RaidBot.Engine.Controler.Game.Player;
using RaidBot.Engine.Functionality.Fight;
using RaidBot.Engine.Utility.Pathfinding;
using RaidBot.Protocol.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RaidBot.Engine.Model.Game.World.Fight.Shapes;
using System.Threading.Tasks;

namespace RaidBot.Engine.Model.Game.World.Fight.IA
{
    public class Movement
    {
		Lozenge zone;
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
		public Movement(Brain brain)
		{
			mBrain = brain;

		}
		public void Move()
		{
			switch (Player.PlayerFightInformations.IA)
				{
			case Enums.IAEnum.Fuyard:
				flee ();
					break;
			case Enums.IAEnum.Passif:
				return;
			case Enums.IAEnum.Offensif:
				sink ();
					break;
				}
		}
		public void flee(int PM)
        {
            if (Player == null)
                return;
            zone = new Lozenge(0, (byte)PM);
            Cell farCell;
            List<Cell> cells = zone.GetCells(Fight.finder.matrix[Player.PlayerBaseInformations.CellId], Fight.finder).ToList();
            farCell = cells.OrderBy(placementCell => Fight.GetEnnemies().Select(fighter => Fight.finder.matrix[fighter.CellId]).ToList().Min(cell => cell.ManhattanDistanceTo(placementCell))).LastOrDefault();
            MoveToCell((short)farCell.Id);
        }
		public void sink(int PM)
        {
            zone = new Lozenge(0, (byte)PM);
            List<Cell> cells = zone.GetCells(Fight.finder.matrix[Player.PlayerBaseInformations.CellId], Fight.finder).ToList();
            Cell farCell = cells.OrderBy(placementCell => Fight.GetEnnemies().Select(fighter => Fight.finder.matrix[fighter.CellId]).ToList().Min(cell => cell.ManhattanDistanceTo(placementCell))).FirstOrDefault();
            MoveToCell((short)farCell.Id);
            mBrain.Fight.mHost.SendMessage(new ShowCellMessage(Player.PlayerBaseInformations.Id, (ushort)farCell.Id), Engine.Enums.DestinationEnum.CLIENT);
        }
		public void sink()
		{
			sink (Player.PlayerFightInformations.Stats.movementPoints);
		}
		public void flee()
		{
			flee (Player.PlayerFightInformations.Stats.movementPoints);
		}
        public void MoveToCell(short CellID)
        {
            List<CellWithOrientation> path = Fight.finder.GetPath((short)Player.PlayerBaseInformations.CellId, CellID);
            //if (path == null) {
            //    Fight.mHost.logger.Log ("Path null !", RaidBot.Common.Default.Loging.LogLevelEnum.Error);
            //    return;
            //}
            //if (path.Count - 1 > Player.PlayerFightInformations.Stats.movementPoints)
            //{
            //    path = path.GetRange(0, Player.PlayerFightInformations.Stats.movementPoints + 1);
            //}
            //for (int i = 0; i <= path.Count - 1 ; i++ )
            //{
            //    RaidBot.Data.IO.D2P.Map.CellData Cellule = Player.mWorld.Map.Data.Cells[path[i].Id];
            //    if (Fight.GetEnnemiesCells().Any(cell => cell == path[i].Id) || !Cellule.los)
            //    {
            //        if (path.Count < 3)
            //            return;
            //        path = path.GetRange(0, i );
            //          break;
            //    }
            //}
            //if (path.Count < 2) {
            //    Fight.mHost.logger.Log ("path count !", RaidBot.Common.Default.Loging.LogLevelEnum.Error);
            //    return;
            //}
                Player.mHost.SendMessage(new GameMapMovementRequestMessage(PathingUtils.GetCompressedPath(path), (int)Player.mWorld.Map.Data.Id));
        }
        
    }
}
