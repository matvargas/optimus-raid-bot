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
    public class Lozenge : IShape
    {
       
        public Lozenge(byte minRadius, byte radius)
        {
            MinRadius = minRadius;
            Radius = radius;
        }
        #region IShape Members

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

            if (Radius == 0)
            {
                if (MinRadius == 0)
                    result.Add(centerCell);

                return result.ToArray();
            }

            int x = (int)(centerCell.Location.X - Radius);
            int y = 0;
            int i = 0;
            int j = 1;
            while (x <= centerCell.Location.X + Radius)
            {
                y = -i;

                while (y <= i)
                {
                    if (MinRadius == 0 || Math.Abs(centerCell.Location.X - x) + Math.Abs(y) >= MinRadius)
                        AddCellIfValid(x, y + centerCell.Location.Y, finder, result);

                    y++;
                }

                if (i == Radius)
                {
                    j = -j;
                }

                i = i + j;
                x++;
            }

            return result.ToArray();
        }

        private void AddCellIfValid(int x, int y, Pathfinder finder, IList<Cell> container)
        {
            if ( !finder.matrix.Any(cell => cell.Key == PathingUtils.CoordToCellId(x, y)))
            {
                return;
            }
            container.Add(finder.matrix[PathingUtils.CoordToCellId(x, y)]);
        }

        #endregion
    }
}
