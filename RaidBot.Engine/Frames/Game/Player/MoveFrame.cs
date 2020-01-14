using RaidBot.Common.Default.Loging;
using RaidBot.Data.IO.D2P.Map;
using RaidBot.Engine.Controler.Game.World;
using RaidBot.Engine.Dispatcher;
using RaidBot.Engine.Enums;
using RaidBot.Engine.Manager;
using RaidBot.Engine.Model.Game.World;
using RaidBot.Engine.Utility.Pathfinding;
using RaidBot.Protocol.Enums;
using RaidBot.Protocol.Messages;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RaidBot.Engine.Frames.Game.Player
{
    public class MoveFrame : Frame
    {
        private ConnectedHost mHost;
        private bool mMoving = false;
        private bool mChangeMap = false;
        private DirectionsEnum MapDirection;
        
        public  MoveFrame(ConnectedHost host)
        {

            mHost = host;
            mHost.Dispatcher.Register(this);
        }

        public void Dispose()
        {
            mHost.Dispatcher.UnRegister(this);
        }

        public int Move(short destinationCellId, short curentCellId, bool useDiagonal, WorldControler world)
        {
           mHost.Bot.Game.Player.mPathFinder.SetMap(world.Map, useDiagonal);
			DebugHighlightCellsMessage message = new DebugHighlightCellsMessage(Color.Red.ToArgb(), new List<ushort>().ToArray());

            List<CellWithOrientation> cels = mHost.Bot.Game.Player.mPathFinder.GetPath(curentCellId, destinationCellId);
            if (cels == null)
                Move(destinationCellId, curentCellId, useDiagonal, world);
            List<short> keyMouvement = new List<short>();
            foreach (CellWithOrientation ce in cels)
            {
                message.cells.ToList().Add((ushort)ce.Id);
                ce.GetCompressedValue();
                keyMouvement.Add(ce.CompressedValue);
            }
            mHost.SendMessage(message,DestinationEnum.CLIENT);
            Move(keyMouvement.ToArray(), world.Map.Data.Id);
            return (int)keyMouvement.Count;
        }

        public void Move(DirectionsEnum direction,  WorldControler world,short cellId)
        {
            mChangeMap=true;
            MapDirection = direction;

            Move(GetRandomCellid(direction, world), cellId, true, world);
        }

        public void Move(short[] keyMouvements, uint mapId)
        {
            mMoving=true;
            GameMapMovementRequestMessage mouvementRequest = new GameMapMovementRequestMessage(keyMouvements, (int)mapId);
            mHost.SendMessage(mouvementRequest);
        } 

        [MessageHandlerAttribut(typeof(GameMapMovementConfirmMessage))]
        private void HandleGameMapMouvementConfirmMessage(GameMapMovementConfirmMessage message, ConnectedHost source)
        {
            if(mMoving)
            {
                mMoving=false;
                if(mChangeMap)
                {
                    mChangeMap =false;
                    new Task(() => ChangeMap(MapDirection, mHost.Bot.Game.World.Map.Data)).Start();
                }
            }
        }

        private void ChangeMap(DirectionsEnum direction, Map map)
        {
          
            ChangeMapMessage message = new ChangeMapMessage();
            switch (direction)
            {
                case DirectionsEnum.DIRECTION_EAST:
                    message = new ChangeMapMessage(map.RightNeighbourId);
                    break;
                case DirectionsEnum.DIRECTION_NORTH:
                    message = new ChangeMapMessage(map.TopNeighbourId);
                    break;
                case DirectionsEnum.DIRECTION_SOUTH:
                    message = new ChangeMapMessage(map.BottomNeighbourId);

                    break;
                case DirectionsEnum.DIRECTION_WEST:
                    message = new ChangeMapMessage(map.LeftNeighbourId);
                    break;
            }
            mHost.SendMessage(message);
        }

        private short GetRandomCellid(DirectionsEnum direction, WorldControler world, bool passAcotrs = false)
        {
			try
			{
            switch (direction)
            {
                case DirectionsEnum.DIRECTION_SOUTH:
                    while (true)
                    {
                        Random al = new Random();
                        int dest = al.Next(532, 559);
                        if (world.Map.Data.Cells[dest].mov  && world.Map.Data.Cells[dest].mapChangeData != 0 && (!world.Map.Actors.ContainsKey(dest) && !passAcotrs) )
                        {
                            return (short)(dest);
                        }
                    }

                case DirectionsEnum.DIRECTION_NORTH:
                    while (true)
                    {
                        Random al = new Random();
                        int dest = al.Next(14, 27);
                        if (world.Map.Data.Cells[dest].mov && world.Map.Data.Cells[dest].mapChangeData != 0 && (!world.Map.Actors.ContainsKey(dest) && !passAcotrs))
                        {
                            return (short)(dest);
                        }
                    }

                case DirectionsEnum.DIRECTION_EAST:
                    while (true)
                    {
                        Random al = new Random();
                        int dest = al.Next(0, 39) * 14 + 13;
                        if (world.Map.Data.Cells[dest].mov && world.Map.Data.Cells[dest].mapChangeData != 0 && (!world.Map.Actors.ContainsKey(dest) && !passAcotrs))
                        {
                            return (short)(dest);
                        }
                    }

                case DirectionsEnum.DIRECTION_WEST:
                    while (true)
                    {
                        Random al = new Random();
                        int dest = al.Next(0, 39) * 14;
                        if (world.Map.Data.Cells[dest].mov && world.Map.Data.Cells[dest].mapChangeData != 0 && (!world.Map.Actors.ContainsKey(dest) && !passAcotrs))
                        {
                            return (short)(dest);
                        }
                    }
            }
			}
			catch (Exception e) {
				mHost.logger.Log ("Impossible de changer la map vers cette direction", LogLevelEnum.Error);
			}
            return GetRandomCellid(direction, world, true);
        }

        private void WaitMove(int length)
        {
            if (length < 3)
            {
                Thread.Sleep(length * 500);
            }
            else
            {
                Thread.Sleep(length * 300);
            }
        }
    }
}