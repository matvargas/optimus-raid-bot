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
	public class DiagonalCross : IShape
	{
		public DiagonalCross(byte minRadius, byte radius)
		{
			MinRadius = minRadius;
			Radius = radius;
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
			var line1 = new Line (Radius, MinRadius, DirectionsEnum.DIRECTION_EAST);
			var line2 = new Line (Radius, MinRadius, DirectionsEnum.DIRECTION_NORTH);
			var line3 = new Line (Radius, MinRadius, DirectionsEnum.DIRECTION_WEST);
			var line4 = new Line (Radius, MinRadius, DirectionsEnum.DIRECTION_SOUTH);
			result.AddRange (line1.GetCells(centerCell , finder).ToList());
			result.AddRange (line2.GetCells(centerCell , finder).ToList());
			result.AddRange (line3.GetCells(centerCell , finder).ToList());
			result.AddRange (line4.GetCells(centerCell , finder).ToList());


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

