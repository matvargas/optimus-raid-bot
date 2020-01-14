using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidBot.Protocol.DataCenter
{
    [Serializable()]
    public class Rectangle : IData
    {
        private Point _location;
        private int _width;
        private int _height;

        public Point Location
        {
            get
            {
                return _location;
            }
        }
        public int Width
        {
            get
            {
                return _width;
            }
        }
        public int Height
        {
            get
            {
                return _height;
            }
        }

        public Rectangle()
        {
        }

        //public Rectangle(Point location, int width, int hight)
        //{
        //    _location = location;
        //    _width = width;
        //    _hight = hight;
        //}
        public Rectangle(int x, int y, int width, int height)
        {
            _location = new Point(x, y);
            _width = width;
            _height = height;
        }
    }
}
