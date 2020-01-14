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
    public interface IShape
    {
        uint Surface
        {
            get;
        }

        byte MinRadius
        {
            get;
            set;
        }

        DirectionsEnum Direction
        {
            get;
            set;
        }

        byte Radius
        {
            get;
            set;
        }

        Cell[] GetCells(Cell centerCell,Pathfinder finder);
    }
}