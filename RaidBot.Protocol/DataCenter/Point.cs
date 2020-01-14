using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidBot.Protocol.DataCenter
{
    [Serializable()]
    public class Point:IData
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Point()
        {

        }
        public Point(int x,int y)
        {
            X = x;
            Y = y;
        }
    }
}
