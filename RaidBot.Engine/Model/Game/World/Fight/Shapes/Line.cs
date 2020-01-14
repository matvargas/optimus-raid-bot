using RaidBot.Engine.Model.Game.World.Actor.Fight;
using RaidBot.Engine.Utility.Pathfinding;
using RaidBot.Protocol.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidBot.Engine.Model.Game.World.Fight.Shapes

{
	public class Line : IShape
	{
		public Line(byte radius,byte Minradius ,  DirectionsEnum direction = DirectionsEnum.DIRECTION_SOUTH_EAST)
		{
			Radius = radius;
			Direction = direction;
			MinRadius = Minradius;
		}

		#region IShape Members

		public uint Surface
		{
			get
			{
				return (uint)Radius + 1;
			}
		}

		public byte MinRadius
		{
			get;
			set;
		}

		public DirectionsEnum Direction
		{
			get;
			set;
		}

		public byte Radius
		{
			get;
			set;
		}

		public Cell[] GetCells(Cell centerCell, Pathfinder finder)
		{
			var result = new List<Cell>();

			for (int i = (int)MinRadius; i <= Radius; i++)
			{
				switch (Direction)
				{
				case DirectionsEnum.DIRECTION_WEST:
					AddCellIfValid(centerCell.Location.X - i, centerCell.Location.Y - i, finder, result);
					break;
				case DirectionsEnum.DIRECTION_NORTH:
					AddCellIfValid(centerCell.Location.X - i, centerCell.Location.Y + i, finder, result);
					break;
				case DirectionsEnum.DIRECTION_EAST:
					AddCellIfValid(centerCell.Location.X + i, centerCell.Location.Y + i, finder, result);
					break;
				case DirectionsEnum.DIRECTION_SOUTH:
					AddCellIfValid(centerCell.Location.X + i, centerCell.Location.Y - i, finder, result);
					break;
				case DirectionsEnum.DIRECTION_NORTH_WEST:
					AddCellIfValid(centerCell.Location.X - i, centerCell.Location.Y, finder, result);
					break;
				case DirectionsEnum.DIRECTION_SOUTH_WEST:
					AddCellIfValid(centerCell.Location.X, centerCell.Location.Y - i, finder, result);
					break;
				case DirectionsEnum.DIRECTION_SOUTH_EAST:
					AddCellIfValid(centerCell.Location.X + i, centerCell.Location.Y, finder, result);
					break;
				case DirectionsEnum.DIRECTION_NORTH_EAST:
					AddCellIfValid(centerCell.Location.X, centerCell.Location.Y + i, finder, result);
					break;
				}
			}

			return result.ToArray();
		}

		private void AddCellIfValid(int x, int y, Pathfinder finder, IList<Cell> container)
		{
			if (!Cell.IsInMap(x, y) || !finder.matrix.Any(cell => cell.Key == PathingUtils.CoordToCellId(x, y)))
			{
				return;
			}
			container.Add(finder.matrix[PathingUtils.CoordToCellId(x, y)]);
		}
		#endregion
	}
}