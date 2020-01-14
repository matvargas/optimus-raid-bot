using RaidBot.Data.IO.D2P.Map;
using RaidBot.Engine.Utility.Pathfinding;
using RaidBot.Protocol.DataCenter;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidBot.Engine.Utility
{
    public class SpellShape
    {
        private static SpellShape instance = new SpellShape();
        public static SpellShape GetInstance()
        {
            return instance;
        }

        /**Returns shape of selected spellLevel...
        * @param  {number} source - source Cllid
        * @param  {object} spellLevel - level data
        */
        public System.Drawing.Point[] GetSpellRange(short source, SpellLevel spell)
        {
            System.Drawing.Point coords = PathingUtils.CellIdToCoord(source);

            System.Drawing.Point[] rangeCoords;

            if (spell.CastInLine && spell.CastInDiagonal)
            {
                rangeCoords = ShapeCross(coords.X, coords.Y, (int)spell.MinRange, (int)spell.Range)
                    .Concat(ShapeStar(coords.X, coords.Y, (int)spell.MinRange, (int)spell.Range)).ToArray();
            }
            else if (spell.CastInLine)
            {
                rangeCoords = ShapeCross(coords.X, coords.Y, (int)spell.MinRange, (int)spell.Range);
            }
            else if (spell.CastInDiagonal)
            {
                rangeCoords = ShapeStar(coords.X, coords.Y, (int)spell.MinRange, (int)spell.Range);
            }
            else
            {
                rangeCoords = ShapeRing(coords.X, coords.Y, (int)spell.MinRange, (int)spell.Range);
            }
            return (from item in rangeCoords where !((item.X != 0 || item.Y != 0) && PathingUtils.CoordToCellId(item) == 0) && item.X + item.Y != 28 && item.X + item.Y <= 27 && item.Y + item.X >= 0 && PathingUtils.CoordToCellId(item) >= 0 && PathingUtils.CoordToCellId(item) < 560 select item).ToArray();
        }

        /** Returns the range of a ring shaped area.
         *  cell in result range are ordered by distance to the center, ascending.
         *
         * @param {number} x - x coordinate of center
         * @param {number} y - y coordinate of center
         * @param {number} radiusMin - radius of inner limit of ring
         * @param {number} radiusMax - radius of outter limit of ring
         *
         * @return {Array} range - an array of point coordinate.
         */
        public System.Drawing.Point[] ShapeRing(int x, int y, int radiusMin, int radiusMax)
        { //TODO: appears to return duplicates, investigate.
            List<System.Drawing.Point> range = new List<System.Drawing.Point>();
            if (radiusMin == 0) { range.Add(new System.Drawing.Point(x, y)); }
            for (int radius = (radiusMin > 0) ? radiusMin : 1; radius <= radiusMax; radius++)
            {
                for (int i = 0; i < radius; i++)
                {
                    int r = radius - i;
                    range.Add(new System.Drawing.Point(x + i, y - r));
                    range.Add(new System.Drawing.Point(x + r, y + i));
                    range.Add(new System.Drawing.Point(x - i, y + r));
                    range.Add(new System.Drawing.Point(x - r, y - i));
                }
            }
            return range.ToArray();
        }

        /** Returns the range of a cross shaped area.
         *  cell in result range are ordered by distance to the center, ascending.
         *
         * @param {number} x - x coordinate of center
         * @param {number} y - y coordinate of center
         * @param {number} radiusMin - inner radius of area
         * @param {number} radiusMax - outter radius of area
         *
         * @return {number[]} range - an array of point coordinate.
         */
        public System.Drawing.Point[] ShapeCross(int x, int y, int radiusMin, int radiusMax)
        {
            List<System.Drawing.Point> range = new List<System.Drawing.Point>();
            if (radiusMin == 0) { range.Add(new System.Drawing.Point(x, y)); }
            for (int i = (radiusMin > 0) ? radiusMin : 1; i <= radiusMax; i++)
            {
                range.Add(new System.Drawing.Point(x - i, y));
                range.Add(new System.Drawing.Point(x + i, y));
                range.Add(new System.Drawing.Point(x, y - i));
                range.Add(new System.Drawing.Point(x, y + i));
            }
            return range.ToArray();
        }

        /** Returns the range of a star shaped area. */
        public System.Drawing.Point[] ShapeStar(int x, int y, int radiusMin, int radiusMax)
        {
            List<System.Drawing.Point> range = new List<System.Drawing.Point>();
            if (radiusMin == 0) { range.Add(new System.Drawing.Point(x, y)); }
            for (int i = (radiusMin > 0) ? radiusMin : 1; i <= radiusMax; i++)
            {
                range.Add(new System.Drawing.Point(x - i, y - i));
                range.Add(new System.Drawing.Point(x - i, y + i));
                range.Add(new System.Drawing.Point(x + i, y - i));
                range.Add(new System.Drawing.Point(x + i, y + i));
            }
            return range.ToArray();
        }

        /** Combinaison of shapeCross and shapeStar */
        public System.Drawing.Point[] ShapeCrossAndStar(int x, int y, int radiusMin, int radiusMax)
        {
            List<System.Drawing.Point> range = new List<System.Drawing.Point>();
            if (radiusMin == 0) { range.Add(new System.Drawing.Point(x, y)); }
            for (int i = (radiusMin > 0) ? radiusMin : 1; i <= radiusMax; i++)
            {
                // cross
                range.Add(new System.Drawing.Point(x - i, y));
                range.Add(new System.Drawing.Point(x + i, y));
                range.Add(new System.Drawing.Point(x, y - i));
                range.Add(new System.Drawing.Point(x, y + i));
                // star
                range.Add(new System.Drawing.Point(x - i, y - i));
                range.Add(new System.Drawing.Point(x - i, y + i));
                range.Add(new System.Drawing.Point(x + i, y - i));
                range.Add(new System.Drawing.Point(x + i, y + i));
            }
            List<System.Drawing.Point> ret = new List<System.Drawing.Point>();
            foreach (System.Drawing.Point p in range)
            {
                short id = PathingUtils.CoordToCellId(p);
                if (id >= 0 && id <= 560)
                    ret.Add(p);
            }
            return ret.ToArray();
        }

        /** Returns the range of a square shaped area. */
        public System.Drawing.Point[] ShapeSquare(int x, int y, int radiusMin, int radiusMax)
        {
            List<System.Drawing.Point> range = new List<System.Drawing.Point>();
            if (radiusMin == 0) { range.Add(new System.Drawing.Point(x, y)); }
            for (int radius = (radiusMin > 0) ? radiusMin : 1; radius <= radiusMax; radius++)
            {
                // segment middles
                range.Add(new System.Drawing.Point(x - radius, y));
                range.Add(new System.Drawing.Point(x + radius, y));
                range.Add(new System.Drawing.Point(x, y - radius));
                range.Add(new System.Drawing.Point(x, y + radius));
                // segment corners
                range.Add(new System.Drawing.Point(x - radius, y - radius));
                range.Add(new System.Drawing.Point(x - radius, y + radius));
                range.Add(new System.Drawing.Point(x + radius, y - radius));
                range.Add(new System.Drawing.Point(x + radius, y + radius));
                // segment remaining
                for (int i = 1; i < radius; i++)
                {
                    range.Add(new System.Drawing.Point(x + radius, y + i));
                    range.Add(new System.Drawing.Point(x + radius, y - i));
                    range.Add(new System.Drawing.Point(x - radius, y + i));
                    range.Add(new System.Drawing.Point(x - radius, y - i));
                    range.Add(new System.Drawing.Point(x + i, y + radius));
                    range.Add(new System.Drawing.Point(x - i, y + radius));
                    range.Add(new System.Drawing.Point(x + i, y - radius));
                    range.Add(new System.Drawing.Point(x - i, y - radius));
                }
            }
            return range.ToArray();
        }

        /** Return the range of a cone shaped area (effect type 'V') */
        public System.Drawing.Point[] ShapeCone(int x, int y, int radiusMin, int radiusMax, int dirX, int dirY)
        {
            List<System.Drawing.Point> range = new List<System.Drawing.Point>();
            for (int radius = radiusMin; radius <= radiusMax; radius++)
            {
                int xx = x + radius * dirX;
                int yy = y + radius * dirY;
                range.Add(new System.Drawing.Point(xx, yy));
                for (int i = 1; i <= radius; i++)
                {
                    range.Add(new System.Drawing.Point(xx + i * dirY, yy - i * dirX));
                    range.Add(new System.Drawing.Point(xx - i * dirY, yy + i * dirX));
                }
            }
            return range.ToArray();
        }

        /** Return the range of a halfcircle shaped area (effect type 'U') */
        public System.Drawing.Point[] ShapeHalfcircle(int x, int y, int radiusMin, int radiusMax, int dirX, int dirY)
        {
            List<System.Drawing.Point> range = new List<System.Drawing.Point>();
            if (radiusMin == 0) { range.Add(new System.Drawing.Point(x, y)); }
            for (int radius = (radiusMin > 0) ? radiusMin : 1; radius <= radiusMax; radius++)
            {
                int xx = x - radius * dirX;
                int yy = y - radius * dirY;
                range.Add(new System.Drawing.Point(xx + radius * dirY, yy - radius * dirX));
                range.Add(new System.Drawing.Point(xx - radius * dirY, yy + radius * dirX));
            }
            return range.ToArray();
        }

        /** Returns the range of a four cones shaped area (effect type 'W')
         *  The shape is basicaly a square without the diagonals and central point.
         */
        public System.Drawing.Point[] ShapeCones(int x, int y, int radiusMin, int radiusMax)
        {
            List<System.Drawing.Point> range = new List<System.Drawing.Point>();
            for (int radius = (radiusMin > 0) ? radiusMin : 1; radius <= radiusMax; radius++)
            {
                // segment middles
                range.Add(new System.Drawing.Point(x - radius, y));
                range.Add(new System.Drawing.Point(x + radius, y));
                range.Add(new System.Drawing.Point(x, y - radius));
                range.Add(new System.Drawing.Point(x, y + radius));
                // segment remaining
                for (int i = 1; i < radius; i++)
                {
                    range.Add(new System.Drawing.Point(x + radius, y + i));
                    range.Add(new System.Drawing.Point(x + radius, y - i));
                    range.Add(new System.Drawing.Point(x - radius, y + i));
                    range.Add(new System.Drawing.Point(x - radius, y - i));
                    range.Add(new System.Drawing.Point(x + i, y + radius));
                    range.Add(new System.Drawing.Point(x - i, y + radius));
                    range.Add(new System.Drawing.Point(x + i, y - radius));
                    range.Add(new System.Drawing.Point(x - i, y - radius));
                }
            }
            return range.ToArray();
        }

        /** Returns the range of a inline segment shaped area. */
        public System.Drawing.Point[] ShapeLine(int x, int y, int radiusMin, int radiusMax, int dirX, int dirY)
        {
            List<System.Drawing.Point> range = new List<System.Drawing.Point>();
            for (int i = radiusMin; i <= radiusMax; i++)
            {
                range.Add(new System.Drawing.Point(x + dirX * i, y + dirY * i));
            }
            return range.ToArray();
        }

        /** Return the range of a circle perimeter area (effect type 'O')
         *  The function is based on shapeRing, replacing the radiusMin by radiusMax.
         */
        public System.Drawing.Point[] ShapeCirclePerimeter(int x, int y, int radiusMin, int radiusMax)
        {
            return ShapeRing(x, y, radiusMax, radiusMax);
        }

        /** Return the range of a inverted circle area (effect type 'I')
         *  The function is based on shapeRing, going from radiusMax to Infinity.
         *
         *  TODO: Algorithm could be optimized. This one add a lot of invalid cells.
         */
        public System.Drawing.Point[] ShapeInvertedCircle(int x, int y, int radiusMin, int radiusMax)
        {
            throw new NotImplementedException();
            //return ShapeRing(x, y, radiusMax, INFINITE_RANGE);
        }

        /** Return the range of a perpendicular segment shaped area (effect type '-' and 'T') */
        public System.Drawing.Point[] ShapePerpendicular(int x, int y, int radiusMin, int radiusMax, int dirX, int dirY)
        {
            List<System.Drawing.Point> range = new List<System.Drawing.Point>();
            if (radiusMin == 0) { range.Add(new System.Drawing.Point(x, y)); }
            for (int i = (radiusMin > 0) ? radiusMin : 1; i <= radiusMax; i++)
            {
                range.Add(new System.Drawing.Point(x + dirY * i, y - dirX * i));
                range.Add(new System.Drawing.Point(x - dirY * i, y + dirX * i));
            }
            return range.ToArray();
        }
    }
}