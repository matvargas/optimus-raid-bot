using RaidBot.Data.IO.D2P.Map;
using RaidBot.Engine.Bot.Data;
using RaidBot.Engine.Bot.Frames.GameContext;
using RaidBot.Engine.Dispatcher;
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

namespace RaidBot.Engine.Bot.Managers.GameContext
{
    public class MovementManager : Manager
    {
        public const int MAP_WIDTH = 14;
        public const int MAP_HEIGHT = 20;
        public const int RUN_LINEAR_VELOCITY = 170;
        public const int RUN_HORIZONTAL_DIAGONAL_VELOCITY = 255;
        public const int RUN_VERTICAL_DIAGONAL_VELOCITY = 150;
        public const int WALK_LINEAR_VELOCITY = 480;
        public const int WALK_HORIZONTAL_DIAGONAL_VELOCITY = 510;
        public const int WALK_VERTICAL_DIAGONAL_VELOCITY = 425;

        public event Action<List<CellWithOrientation>, int, bool> MovementPerformed;
        public void OnMovementPerformed(List<CellWithOrientation> path, int dellay, bool success)
        {
            if (MovementPerformed != null)
                MovementPerformed(path, dellay, success);
        }

        public MovementManager(Brain brain) : base(brain)
        {
        }

        public static int GetMoveCellDellay(int orientation, int linear, int horizontal, int vertical)
        {
            if (orientation == 4 || orientation == 0)
                return vertical;
            if (orientation == 2 || orientation == 6)
                return horizontal;
            return linear;
        }

        async Task WaitDellay(int d)
        {
            await Task.Delay(d);
        }

        public static int GetMoveDellay(List<CellWithOrientation> path, int linear, int horizontal, int vertical)
        {
            int direction = path[0].Orientation;
            int dellay = GetMoveCellDellay(direction, linear, horizontal, vertical);
            int total = 0;
            foreach (CellWithOrientation cell in path)
            {
                if (direction != cell.Orientation)
                {
                    direction = cell.Orientation;
                    dellay = GetMoveCellDellay(direction, linear, horizontal, vertical);
                }
                total += dellay;
            }
            return total;
        }

        public static DirectionsEnum GetChangeMapDirection(short i, uint cdata)
        {
            Point coord = PathingUtils.CellIdToCoord(i);
            if (i < 14 && (cdata & 32) != 0 || (cdata & 64) != 0 || i < 32 && (cdata & 128) != 0)
                return DirectionsEnum.DIRECTION_NORTH;
            else if (i >= 560 - 14 && (cdata & 2) != 0 || (cdata & 4) != 0 || i >= 560 - 14 && (cdata & 8) != 0)
                return DirectionsEnum.DIRECTION_SOUTH;
            else if ((cdata & 1) != 0 || (i + 1) % (14 * 2) == 0 && (cdata & 2) != 0 || (i + 1) % (14 * 2) == 0 && (cdata & 128) != 0)
                return DirectionsEnum.DIRECTION_WEST;
            else if (coord.X == -coord.Y && (cdata & 8) != 0 || (cdata & 16) != 0 || coord.X == -coord.Y && (cdata & 32) != 0)
                return DirectionsEnum.DIRECTION_EAST;
            return (DirectionsEnum.NONE);
        }

        public List<short> GetContactCells(short cellid)
        {
            List<short> freeCells = new List<short>();
            List<CellData> cells = Brain.State.CurrentMap.Get().Cells;
            Point p = PathingUtils.CellIdToCoord(cellid);
            int[] a = new int[8];
            a[0] = PathingUtils.CoordToCellId(new Point(p.X, p.Y + 1));
            a[1] = PathingUtils.CoordToCellId(new Point(p.X, p.Y - 1));
            a[2] = PathingUtils.CoordToCellId(new Point(p.X + 1, p.Y));
            a[3] = PathingUtils.CoordToCellId(new Point(p.X - 1, p.Y));
            a[4] = PathingUtils.CoordToCellId(new Point(p.X + 1, p.Y + 1));
            a[5] = PathingUtils.CoordToCellId(new Point(p.X - 1, p.Y + 1));
            a[6] = PathingUtils.CoordToCellId(new Point(p.X + 1, p.Y - 1));
            a[7] = PathingUtils.CoordToCellId(new Point(p.X - 1, p.Y - 1));
            foreach (int id in a)
            {
                if (id >= 0 && id < 560 && cells[id].mov && cells[id].allowWalkRP)
                    freeCells.Add((short)id);
            }
            return freeCells.OrderBy(elem => PathingUtils.DistanceToPoint(PathingUtils.CellIdToCoord(elem), PathingUtils.CellIdToCoord(Brain.State.Player.RolePlayInformations.Get().Disposition.CellId))).ToList();
        }

        private async Task<bool> ConfirmeMove(bool AllowChangeMap, List<CellWithOrientation> path)
        {
            Brain.SendMessage(new GameMapMovementConfirmMessage());
            Brain.CurrentState.Set(Brain.BrainState.Idle);
            Brain.State.Player.RolePlayInformations.Get().Disposition.CellId = (short)path[path.Count - 1].Id;
            Brain.State.Player.RolePlayInformations.OnChanged();
            if (AllowChangeMap)
            {
                TaskCompletionSource<bool> task = new TaskCompletionSource<bool>();
                DirectionsEnum dir = GetChangeMapDirection((short)path[path.Count - 1].Id, Brain.State.CurrentMap.Get().Cells[path[path.Count - 1].Id].mapChangeData);
                if (dir != DirectionsEnum.NONE)
                {

                    int mapid = 0;
                    switch (dir)
                    {
                        case DirectionsEnum.DIRECTION_SOUTH:
                            mapid = Brain.State.CurrentMap.Get().BottomNeighbourId;
                            break;
                        case DirectionsEnum.DIRECTION_NORTH:
                            mapid = Brain.State.CurrentMap.Get().TopNeighbourId;
                            break;
                        case DirectionsEnum.DIRECTION_EAST:
                            mapid = Brain.State.CurrentMap.Get().LeftNeighbourId;
                            break;
                        case DirectionsEnum.DIRECTION_WEST:
                            mapid = Brain.State.CurrentMap.Get().RightNeighbourId;
                            break;
                    }
                    bool done = false;
                    void stateUpdate(Map current)
                    {
                        done = true;
                        Brain.State.CurrentMap.changed -= stateUpdate;
                        if (task.Task.IsCompleted || task.Task.IsFaulted || task.Task.IsCanceled)
                            Error("Can't end task move that already finished !");
                        task.SetResult(true);
                    }
                    async Task timeout()
                    {
                        await Task.Delay(5000);
                        if (!done)
                        {
                            Brain.State.CurrentMap.changed -= stateUpdate;
                            Error("Changing map timedout !");
                            if (task.Task.IsCompleted || task.Task.IsFaulted || task.Task.IsCanceled)
                                Error("Can't end task move that already finished !");
                            task.SetResult(false);
                        }
                    }
                    Brain.State.CurrentMap.changed += stateUpdate;
                    timeout();
                    Brain.SendMessage(new ChangeMapMessage().InitChangeMapMessage(mapid, false));
                    return await task.Task;
                }
                else
                {
                    Warn("No change map data");
                }
            }
            return true;
        }

        public async Task<bool> Move(int cellId, bool allowChangeMap = true)
        {
            if (Brain.CurrentState.Get() != Brain.BrainState.Idle)
            {
                Warn("Can't performe movement, player is not idle");
                return false;
            }
            Brain.CurrentState.Set(Brain.BrainState.Moving);
            bool result = await LowMove(cellId, allowChangeMap);
            Brain.CurrentState.Set(Brain.BrainState.Idle);
            return result;
        }

        public async Task<bool> LowMove(int cellId, bool allowChangeMap)
        {
            Pathfinder finder = new Pathfinder(this.Brain.ElementsManager.UsedCells);
            finder.SetMap(Brain.State.CurrentMap, true);
            List<CellWithOrientation> path = finder.GetPath(Brain.State.Player.RolePlayInformations.Get().Disposition.CellId, (short)cellId);
            if (path.Count > 1)
            {
                TaskCompletionSource<bool> step = new TaskCompletionSource<bool>();
                async void handler(MovementResultEnum res)
                {
                    int dellay;
                    if (res == MovementResultEnum.ForceWalk)
                        Warn("Forced to walk, probabley out of pods");
                    if (path.Count <= 4 || res == MovementResultEnum.ForceWalk)
                        dellay = GetMoveDellay(path, WALK_LINEAR_VELOCITY, WALK_HORIZONTAL_DIAGONAL_VELOCITY, WALK_VERTICAL_DIAGONAL_VELOCITY);
                    else
                        dellay = GetMoveDellay(path, RUN_LINEAR_VELOCITY, RUN_HORIZONTAL_DIAGONAL_VELOCITY, RUN_VERTICAL_DIAGONAL_VELOCITY);
                    Brain.PlayerManager.Movement.OnMovementPerformed(path, dellay, res != MovementResultEnum.Error);
                    Brain.MovementFrame.MovementResult -= handler;
                    if (res != MovementResultEnum.Error)
                    {
                        await Task.Delay(dellay);
                        step.SetResult(await ConfirmeMove(allowChangeMap, path));
                    }
                    else
                        step.SetResult(false);
                }
                Brain.MovementFrame.MovementResult += handler;
                GameMapMovementRequestMessage gmrm = new GameMapMovementRequestMessage();
                gmrm.InitGameMapMovementRequestMessage(PathingUtils.GetCompressedPath(path), Brain.State.CurrentMap.Get().Id);
                Brain.SendMessage(gmrm);
                return await step.Task;
            }
            return true;
        }

        public int FindNearestBorderCell(DirectionsEnum direction, Point fromCellPoint)
        {
            int currentlyCheckedCellX = 0;
            int currentlyCheckedCellY = 0;
            int i = 0;
            int maxI = 0;
            int currentCellId = 0;
            int mapChangeData = 0;
            Map currentMap = Brain.State.CurrentMap;
            switch (direction)
            {
                case DirectionsEnum.DIRECTION_EAST:
                    currentlyCheckedCellX = MAP_WIDTH - 1;
                    currentlyCheckedCellY = MAP_WIDTH - 1;
                    break;
                case DirectionsEnum.DIRECTION_NORTH_WEST:
                    currentlyCheckedCellX = 0;
                    currentlyCheckedCellY = 0;
                    break;
                case DirectionsEnum.DIRECTION_SOUTH:
                    currentlyCheckedCellX = MAP_HEIGHT - 1;
                    currentlyCheckedCellY = -(MAP_HEIGHT - 1);
                    break;
                case DirectionsEnum.DIRECTION_NORTH:
                    currentlyCheckedCellX = 0;
                    currentlyCheckedCellY = 0;
                    break;
                default:
                    throw new Exception("Invalide direction");
            }
            int cellid = PathingUtils.CoordToCellId(fromCellPoint);
            if (cellid >= currentMap.CellsCount || cellid < 0)
                throw new Exception("Invalide cell coord");
            CellData closestCustomCellData = currentMap.Cells[PathingUtils.CoordToCellId(fromCellPoint)];
            if (direction == DirectionsEnum.DIRECTION_EAST || direction == DirectionsEnum.DIRECTION_WEST)
            {
                maxI = MAP_HEIGHT * 2;
                for (i = 0; i < maxI; i++)
                {
                    currentCellId = PathingUtils.CoordToCellId(new Point(currentlyCheckedCellX, currentlyCheckedCellY));
                    mapChangeData = (int)currentMap.Cells[currentCellId].mapChangeData;
                    if (mapChangeData != 0 && (direction == DirectionsEnum.DIRECTION_WEST && ((mapChangeData & 1) != 0 || ((currentCellId + 1) % (MAP_WIDTH * 2) == 0) && (mapChangeData & 2) != 0 || (currentCellId + 1) % (MAP_WIDTH * 2) == 0 && (mapChangeData & 128) != 0) || direction == DirectionsEnum.DIRECTION_EAST && (currentlyCheckedCellX == -currentlyCheckedCellY && (mapChangeData & 8) != 0 || (mapChangeData & 16) != 0 || currentlyCheckedCellX == -currentlyCheckedCellY && (mapChangeData & 32) != 0)))
                        return currentCellId;
                }
                if (i % 2 == 0)
                    currentlyCheckedCellX++;
                else
                    currentlyCheckedCellY--;
            }
            else
            {
                for (i = 0; i < MAP_WIDTH * 2; i++)
                {
                    currentCellId = PathingUtils.CoordToCellId(currentlyCheckedCellX, currentlyCheckedCellY);
                    mapChangeData = (int)currentMap.Cells[currentCellId].mapChangeData;
                    if (mapChangeData != 0 && (direction == DirectionsEnum.DIRECTION_NORTH && (currentCellId < MAP_WIDTH && (mapChangeData & 32) != 0 || (mapChangeData & 64) != 0 || currentCellId < MAP_WIDTH && (mapChangeData & 128) != 0 || direction == DirectionsEnum.DIRECTION_SOUTH && (currentCellId >= currentMap.CellsCount - MAP_WIDTH && (mapChangeData & 2) != 0 || (mapChangeData & 4) != 0 || currentCellId >= currentMap.CellsCount - MAP_WIDTH && (mapChangeData & 8) != 0))))
                        return currentCellId;
                    if (i % 2 != 0)
                        currentlyCheckedCellX++;
                    else
                        currentlyCheckedCellY++;
                }
            }
            return -1;
        }
    }
}
