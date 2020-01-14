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
	public class DiagonalLine
	{
		bool direction;
		public DiagonalLine(byte minRadius, byte radius , bool Direction)
		{
			MinRadius = minRadius;
			Radius = radius;
			direction = Direction;
		}

		public uint Surface
		{
			get
			{
				return ((uint)Radius + 1) * ((uint)Radius + 1) + Radius * (uint)Radius;
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

		public Cell[] GetCells(Cell centerCell,Pathfinder finder)
		{
			var result = new List<Cell>();
			int x = (int)(centerCell.Location.X - Radius);
			int y = (int)(centerCell.Location.Y - Radius);
			if (Radius == 0)
			{
				if (MinRadius == 0)
					result.Add(centerCell);

				return result.ToArray();
			}
			if (direction) {
				while (x <= centerCell.Location.X + Radius) {
					if (Math.Abs (x) >= MinRadius)
				    AddCellIfValid (x, y, finder, result);
					x++;
					y++;
				}	
			} else {
				while (y <= centerCell.Location.X + Radius) {
					if (Math.Abs (x) >= MinRadius)
						AddCellIfValid (-x, y , finder, result);
					y++;
					x++;
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
	}
}

