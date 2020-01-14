using MoonSharp.Interpreter;
using RaidBot.Engine.Bot.Data.Context;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidBot.Engine.Bot.Managers.Extension.Extensions
{
    [ExtensionHandler(ExtensionHandlerAttribute.PriorityEnum.Low)]
    public class GatherExtension : Extension
    {
        /// <summary>
        /// GatherContext is the name of the function called in the lua script that return list of map actions,
        /// by example if we want bank trajet wa call bank() method and for normale move we call move() method
        /// </summary>
        public string GatherContext { get; set; }

        private bool GatherCanBeUsed
        {
            get
            {
                return Brain.CurrentState.Get() == Brain.BrainState.Idle;
            }
        }

        public GatherExtension(Brain brain) : base(brain)
        {
        }

        private async Task RefreshGatherContext()
        {
            if (!Brain.PartyManager.IsFollower && !Brain.PartyManager.IsLost)
            {
                foreach (Brain br in Brain.Group.Bots.Values)
                {
                    if (!br.PartyManager.CanFollow)
                    {
                        Warn("{0} is not ready to follow, wating 15sec and loading lost context if is not ready !", br.Config.Username);
                        await Task.Delay(15000);
                        if (!br.PartyManager.CanFollow)
                        {
                            Warn("Switching to lost context !");
                            foreach (Brain bot in Brain.Group.Bots.Values)
                            {
                                bot.PartyManager.YourLost();
                            }
                        }
                    }
                }
            }
            if (Brain.PartyManager.CheckPoint)
            {
                if (Brain.PartyManager.IsFollower)
                {
                    Log("Wating leader to the checkpoint !");
                    GatherContext = "checkpoint";
                }
                else
                {
                    Log("Checkpoint reached, looking for followers ...");
                    while (!Brain.PartyManager.AllFollowerCanFollow)
                    {
                        Log("Waiting all followers to the checkopoint, next check in 10sec ...");
                        await Task.Delay(10000);
                    }
                    Log("Resuming trajet ...");
                    foreach (Brain bot in Brain.Group.Bots.Values)
                        bot.PartyManager.Recovered();
                    await RefreshGatherContext();
                    return;
                }
            }
            else if (Brain.PartyManager.IsLost)
                GatherContext = "lost";
            else
                GatherContext = "move";
        }

        [ExtensionHandler(ExtensionHandlerAttribute.PriorityEnum.Low)]
        public override async Task RpLoaded()
        {
            await RefreshGatherContext();
            if (!GatherCanBeUsed)
            {
                Warn("Gather can't be used right now !");
                return;
            }
            Table moveTable;
            try
            {
                moveTable = Brain.ExtManager.Trajet.Get().Call(Brain.ExtManager.Trajet.Get().Globals.Get(GatherContext)).Table;
            }
            catch
            {
                Warn("Can't read movement table for conext {0}", GatherContext);
                return;
            }
            ExecuteMoveTable(moveTable);
        }

        #region Gather execution

        #region Execution

        Queue<KeyValuePair<uint, string>> lastDirs = new Queue<KeyValuePair<uint, string>>();
        async Task<bool> MapActionPath(string path)
        {
            if (path == null || path == string.Empty)
                return false;
            string[] possibleDir = path.Split('|');
            string useDir = possibleDir[Brain.ExtManager.Rnd.Next(0, possibleDir.Length)];
            foreach (KeyValuePair<uint, string> dir in lastDirs)
            {
                if (useDir == dir.Value)
                {
                    for (int i = 0; i < 10; i++)
                    {
                        useDir = possibleDir[Brain.ExtManager.Rnd.Next(0, possibleDir.Length)];
                        if (useDir != dir.Value)
                            break;
                    }
                }
            }
            switch (useDir)
            {
                case "left":
                    lastDirs.Enqueue(new KeyValuePair<uint, string>(Brain.State.CurrentMap.Get().Id, "right"));
                    if (lastDirs.Count > 1) lastDirs.Dequeue();
                    return await Brain.PlayerManager.ChangeMap(Protocol.Enums.DirectionsEnum.DIRECTION_EAST);
                case "right":
                    lastDirs.Enqueue(new KeyValuePair<uint, string>(Brain.State.CurrentMap.Get().Id, "left"));
                    if (lastDirs.Count > 1) lastDirs.Dequeue();
                    return await Brain.PlayerManager.ChangeMap(Protocol.Enums.DirectionsEnum.DIRECTION_WEST);
                case "top":
                    lastDirs.Enqueue(new KeyValuePair<uint, string>(Brain.State.CurrentMap.Get().Id, "bottom"));
                    if (lastDirs.Count > 1) lastDirs.Dequeue();
                    return await Brain.PlayerManager.ChangeMap(Protocol.Enums.DirectionsEnum.DIRECTION_NORTH);
                case "bottom":
                    lastDirs.Enqueue(new KeyValuePair<uint, string>(Brain.State.CurrentMap.Get().Id, "top"));
                    if (lastDirs.Count > 1) lastDirs.Dequeue();
                    return await Brain.PlayerManager.ChangeMap(Protocol.Enums.DirectionsEnum.DIRECTION_SOUTH);
                default:
                    int cell = int.Parse(useDir);
                    if (cell >= 0 && cell < 560)
                        return await Brain.PlayerManager.Movement.Move(cell, true);
                    break;
            }
            return false;
        }

        async Task<bool> MapActionUse(DynValue value)
        {
            if (value.Type == DataType.Number)
            {
                if (!Brain.ElementsManager.Elements.Get().ContainsKey(value.Number))
                {
                    Error("Can't find interactive elemement {0}", value.Number);
                    return false;
                }
                ElementDetails elem = Brain.ElementsManager.Elements.Get()[value.Number];
                if (elem.EnabelSkills.Count == 0)
                {
                    Error("No enabel skills on element {0}", value.Number);
                    return false;
                }
                Brain.PlayerManager.UseInteractive(elem, elem.EnabelSkills[0]);
                Log("Interactive use request  form trajet success !");
                return true;
            }
            return false;
        }

        private async Task<bool> FightAction()
        {
            List<double> failed = new List<double>();
            int retry = 0;
            if (Brain.ElementsManager.GroupMonster.Get().Count > 0)
            {
                for (int i = 0; i < Brain.ElementsManager.GroupMonster.Get().Count && retry < 4; i++)
                {
                    if (failed.Contains(Brain.ElementsManager.GroupMonster.Get().ElementAt(i).Value.Informatons.ContextualId))
                        continue;
                    if (await Brain.PlayerManager.AttackGroupMonster(Brain.ElementsManager.GroupMonster.Get().ElementAt(i).Value))
                        return true;
                    else
                    {
                        retry++;
                        await Task.Delay(500);
                    }
                }
            }
            return false;
        }

        private async Task ExecuteMoveAction(Table action)
        {
            DynValue path = action.Get("path");
            DynValue use = action.Get("use");
            DynValue fight = action.Get("fight");
            DynValue end = action.Get("checkpoint");

            await Task.Delay(1000 + Brain.ExtManager.Rnd.Next(100, 500));
            if (!end.IsNilOrNan() && end.Type == DataType.Boolean && end.Boolean && Brain.PartyManager.IsLost)
            {
                Brain.PartyManager.CheckPoint = true;
                await RpLoaded();//TODO check recursion limi just in case
                return;
            }
            if (!fight.IsNilOrNan() && await FightAction())
                return;
            if (!use.IsNilOrNan() && await MapActionUse(use))
                return;
            if (!path.IsNilOrNan() && await MapActionPath(path.String))
                return;
            Error("No succesed action ! {0}", action.ToString());
        }

        #endregion

        #region Loading and select

        private Task ExecuteParsedMoveTable(Dictionary<int, Table> mapIds, Dictionary<Point, Table> coords)
        {
            if (mapIds.ContainsKey((int)Brain.State.CurrentMap.Get().Id))
            {
                Log("Found mapid action ...");
                return ExecuteMoveAction(mapIds[(int)Brain.State.CurrentMap.Get().Id]);
            }
            Point coord = new Point(Brain.State.CurrentMap.Get().Position.x, Brain.State.CurrentMap.Get().Position.y);
            if (coords.ContainsKey(coord))
            {
                Log("Found map coord action ...");
                return ExecuteMoveAction(coords[coord]);
            }
            Warn("No map action found on this map !");
            return Task.Delay(1);
        }

        private async Task ExecuteMoveTable(Table table)
        {
            if (table == null)
                Warn("Gather movement context need to be table !");
            //TODO maybe add a cache to avoid reloading all the table each time
            Dictionary<int, Table> mapIdActions = new Dictionary<int, Table>();
            Dictionary<Point, Table> coordActions = new Dictionary<Point, Table>();
            foreach (DynValue elem in table.Values)
            {
                if (elem.Type != DataType.Table)
                {
                    Warn("movement context element need to be table !");
                    continue;
                }
                Table action = elem.Table;
                DynValue map = action.Get("map");
                if (map.IsNilOrNan())
                {
                    Warn("invalide movement context element ! {0}", action.ToString());
                    continue;
                }
                if (map.Type == DataType.Number)
                    mapIdActions.Add((int)map.Number, table);
                else if (map.Type == DataType.String)
                {
                    if (map.String.Contains(","))
                    {
                        string[] spl = map.String.Split(',');
                        if (spl.Length != 2)
                        {
                            Warn("Invalide map coord, format need to be [x, y] ! {0}", table.ToString());
                            continue;
                        }
                        coordActions[new Point(int.Parse(spl[0].Trim()), int.Parse(spl[1].Trim()))] = action;
                    }
                    else
                        mapIdActions[int.Parse(map.String)] = table;
                }
                else
                {
                    Warn("invalide mapid or coord ! {0}", action.ToString());
                    continue;
                }
            }
            await ExecuteParsedMoveTable(mapIdActions, coordActions);
        }

        #endregion

        #endregion
    }
}
