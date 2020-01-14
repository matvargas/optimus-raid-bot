using RaidBot.Engine.Bot.Data;
using RaidBot.Engine.Utility;
using RaidBot.Engine.Utility.Pathfinding;
using RaidBot.Protocol.DataCenter;
using RaidBot.Protocol.Messages;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RaidBot.Engine.Bot.Managers.FightContext.FightManager;
using Point = System.Drawing.Point;

namespace RaidBot.Engine.Bot.Managers.GameContext
{
    public class Ia : Manager
    {

        public Ia(Brain bot) : base(bot)
        {
            Log("Ia loaded !");
        }

        #region Utils

        #region Properties

        public bool IsStunt
        {
            get
            {
                return NbrCellAvaibleAround(Brain.FightManager.Player.CellId) <= 0;
            }
        }

        public bool IsCac
        {
            get
            {
                Point c = PathingUtils.CellIdToCoord(Brain.FightManager.Player.CellId);
                short[] other = PathingUtils.PointsToCellids(new Point[] { new Point(c.X + 1, c.Y), new Point(c.X - 1, c.Y), new Point(c.X, c.Y - 1), new Point(c.X, c.Y + 1) });
                int baseLen = other.Length;
                foreach (short cell in Brain.FightManager.EntitiesDispositons.Get().Values)
                {
                    foreach (short o in other)
                        if (o == cell)
                            return true;
                }
                return false;
            }
        }

        #endregion

        #region Filtering cellls

        private short[] FilterInvalideCells(short[] cells)
        {
            List<short> ret = new List<short>();
            foreach (short cell in cells)
            {
                if (cell < 0 || cell >= 560)
                    continue;
                ret.Add(cell);
            }
            return ret.ToArray();
        }

        private List<Point> FilterInvalideCells(Point[] ce)
        {
            //TODO some cells is out of map range ...
            List<Point> ret = new List<Point>();
            short[] cells = PathingUtils.PointsToCellids(ce);
            foreach (short cell in cells)
            {
                if (cell < 0 || cell >= 560)
                    continue;
                ret.Add(PathingUtils.CellIdToCoord(cell));
            }
            return ret;
        }


        private List<Point> FilterUsedCells(Point[] ce)
        {
            List<Point> ret = new List<Point>();
            short[] cells = PathingUtils.PointsToCellids(ce);
            foreach (short cell in cells)
            {
                if (!Brain.FightManager.EntitiesDispositons.Get().Values.Contains(cell))
                    ret.Add(PathingUtils.CellIdToCoord(cell));
            }
            return ret;
        }


        private short[] FilterUsedCells(short[] cells)
        {
            List<short> ret = new List<short>();
            foreach (short cell in cells)
            {
                if (!Brain.FightManager.EntitiesDispositons.Get().Values.Contains(cell))
                    ret.Add(cell);
            }
            return ret.ToArray();
        }

        private List<Point> FilterUsedCells(List<Point> ce)
        {
            List<Point> ret = new List<Point>();
            short[] cells = PathingUtils.PointsToCellids(ce.ToArray());
            foreach (short cell in cells)
            {
                if (!Brain.FightManager.EntitiesDispositons.Get().Values.Contains(cell))
                    ret.Add(PathingUtils.CellIdToCoord(cell));
            }
            return ret;
        }

        #endregion

        #region Basics

        bool CellIsMov(short cell)
        {
            return (Brain.State.CurrentMap.Get().Cells[cell].mov && Brain.State.CurrentMap.Get().Cells[cell].allowWalkFight);
        }

        bool CellIsMov(Point c)
        {
            short cell = PathingUtils.CoordToCellId(c);
            return (Brain.State.CurrentMap.Get().Cells[cell].mov && Brain.State.CurrentMap.Get().Cells[cell].allowWalkFight);
        }

        #endregion

        #region Positions utils

        public int NbrCellAvaibleAround(short x, bool diag = false)
        {
            Point curr = PathingUtils.CellIdToCoord(x);
            short[] other;
            if (!diag)
                other = PathingUtils.PointsToCellids(SpellShape.GetInstance().ShapeCross(curr.X, curr.Y, 0, 1));
            else
                other = PathingUtils.PointsToCellids(SpellShape.GetInstance().ShapeRing(curr.X, curr.Y, 0, 1));
            int baseLen = other.Length;
            int filled = 0;
            foreach (short c in other)
            {
                if (!CellIsMov(c))
                {
                    filled++;
                    continue;
                }
                foreach (KeyValuePair<double, short> cell in Brain.FightManager.EntitiesDispositons.Get())
                {
                    if (cell.Value == c && cell.Key != Brain.FightManager.Player.Fighter.ContextualId)
                    {
                        filled++;
                        continue;
                    }
                }
            }
            return baseLen - filled;
        }


        private List<short> GetEntitiesCells(bool ennemy)
        {
            int currentTeam = Brain.FightManager.Player.Fighter.TeamId;
            List<short> dest = new List<short>();
            foreach (FighterDetails fight in Brain.FightManager.Fighters.Get().Values)
            {
                if (ennemy ? (Brain.FightManager.Player.Fighter.TeamId != fight.Fighter.TeamId) : (Brain.FightManager.Player.Fighter.TeamId == fight.Fighter.TeamId))
                    dest.Add(fight.Fighter.Disposition.CellId);
            }
            return dest;
        }

        private List<short> GetEntitiesCells()
        {
            int currentTeam = Brain.FightManager.Player.Fighter.TeamId;
            List<short> dest = new List<short>();
            foreach (FighterDetails fight in Brain.FightManager.Fighters.Get().Values)
            {
                dest.Add(fight.Fighter.Disposition.CellId);
            }
            return dest;
        }

        /// <summary>
        /// Return all ennemies cells
        /// </summary>
        /// <returns></returns>
        private short[] GetEnnemiesCellsByDistToPlayer()
        {
            int currentTeam = Brain.FightManager.Player.Fighter.TeamId;
            List<short> dest = GetEntitiesCells(true);
            return PathingUtils.CellsByDistance(dest.ToArray(), Brain.FightManager.Player.CellId);
        }

        private int DistanceWithNearsetEntities(short cellId, bool ennemy)
        {
            int min = int.MaxValue;
            foreach (short fighter in GetEntitiesCells(ennemy))
                min = Math.Min(min, (int)PathingUtils.DistanceToPoint(fighter, cellId));
            return min;
        }

        #endregion

        #endregion

        #region Movement

        #region Tacle

        #endregion

        #region Cells Finding
        /// <summary>
        /// Get a list of cells, sorted descending by te distance with the nearset ennemy.
        /// we aslo add a "filter" to prevent going in wholl,
        /// we add the number of cells used around target cell to te distance and we make the sort by it
        /// </summary>
        /// <returns></returns>
        public List<short> GetFarsetReachable()
        {
            short[] targets = GetEnnemiesCellsByDistToPlayer();
            short[] reach = GetReascheableCells();
            Dictionary<short, int> reachDist = new Dictionary<short, int>();
            int min;
            for (int x = 0; x < reach.Length; x++)
            {
                if (x < 0 || x >= 560)
                    continue;
                min = int.MaxValue;
                foreach (short c in targets)
                {
                    int dis = (int)PathingUtils.DistanceToPoint(PathingUtils.CellIdToCoord(c), PathingUtils.CellIdToCoord(reach[x]));
                    min = Math.Min(dis, min);
                }
                reachDist[reach[x]] = min + NbrCellAvaibleAround(reach[x], true);
                reachDist[reach[x]] -= MovementManager.GetChangeMapDirection(reach[x], uint.MaxValue) == Protocol.Enums.DirectionsEnum.NONE ? 0 : 1;
            }
            return (from entry in reachDist orderby entry.Value descending select entry.Key).ToList();
        }

        public List<short> GetNearsetFromEnnemyReachable()
        {
            short[] targets = GetEnnemiesCellsByDistToPlayer();
            short[] reach = GetReascheableCells();
            Dictionary<short, int> reachDist = new Dictionary<short, int>();
            int min;
            int globMin = int.MaxValue;
            for (int x = 0; x < reach.Length; x++)
            {
                if (x < 0 || x >= 560)
                    continue;
                min = int.MaxValue;
                foreach (short c in targets)
                {
                    int dis = (int)PathingUtils.DistanceToPoint(PathingUtils.CellIdToCoord(c), PathingUtils.CellIdToCoord(reach[x]));
                    min = Math.Min(dis, min);
                }
                reachDist[reach[x]] = min;
                globMin = Math.Min(min, globMin);
            }
            return (from item in reachDist orderby item.Value select item.Key).ToList();
        }


        /// <summary>
        /// Get a list of cells, sorted ascending by te distance with the nearset ennemy and us (it's mean that it sorted by pm use so the first elements use less pm thant next element)
        /// </summary>
        /// <returns></returns>
        public List<short> GetNearsetFromMeAndEnnemyReachable()
        {
            short[] targets = GetEnnemiesCellsByDistToPlayer();
            short[] reach = GetReascheableCells();
            Dictionary<short, int> reachDist = new Dictionary<short, int>();
            int min;
            int globMin = int.MaxValue;
            for (int x = 0; x < reach.Length; x++)
            {
                if (x < 0 || x >= 560)
                    continue;
                min = int.MaxValue;
                foreach (short c in targets)
                {
                    int dis = (int)PathingUtils.DistanceToPoint(PathingUtils.CellIdToCoord(c), PathingUtils.CellIdToCoord(reach[x]));
                    min = Math.Min(dis, min);
                }
                reachDist[reach[x]] = min;
                globMin = Math.Min(min, globMin);
            }

            // Now we got something like this
            // ..........
            // .X........
            // ....21012.
            // ....10X01.
            // ....21012.
            // But that means the for cell at 0 dist with ennemies is randomley sorted by distance to our player
            // What we want now is something like this
            // ..........
            // .X........
            // ......1...
            // .....2X3..
            // ......4...
            // For each chunk, so when we try to use a cell we got the nearset from us and that avoid turning around ennemies or using too many PM

            Dictionary<int, List<KeyValuePair<int, short>>> chunkedList = new Dictionary<int, List<KeyValuePair<int, short>>>();
            List<KeyValuePair<short, int>> baseSort = (from entry in reachDist orderby entry.Value ascending select entry).ToList();
            int last = -1;
            List<KeyValuePair<int, short>> currentChunk = null;
            for (int i = 0; i < baseSort.Count; i++)
            {
                KeyValuePair<short, int> cellWithDist = baseSort[i];
                // If the distance to monster change or it's first or it's last item add the chunk to the chunk dictionary
                if (last != cellWithDist.Value || i == baseSort.Count - 1)
                {
                    if (currentChunk != null)
                        chunkedList.Add(last, currentChunk);
                    currentChunk = new List<KeyValuePair<int, short>>();
                }
                last = cellWithDist.Value;
                currentChunk.Add(new KeyValuePair<int, short>((int)PathingUtils.DistanceToPoint(PathingUtils.CellIdToCoord(Brain.FightManager.Player.CellId), PathingUtils.CellIdToCoord(cellWithDist.Key)), cellWithDist.Key));
            }
            // Now every things is sorted zip all collections to a single one
            List<short> retval = new List<short>();
            foreach (List<KeyValuePair<int, short>> chunk in chunkedList.Values)
                retval.AddRange((from entry in chunk orderby entry.Key select entry.Value));
            return retval;
        }

        /// <summary>
        /// Return all cells reaschable using PM, incluing current position at index 0 Important !!
        /// </summary>
        /// <returns></returns>
        private short[] GetReascheableCells()
        {
            Point pos = PathingUtils.CellIdToCoord(Brain.FightManager.Player.CellId);
            short[] ids = PathingUtils.PointsToCellids(SpellShape.GetInstance().ShapeRing(pos.X, pos.Y, 0, Brain.FightManager.Player.Fighter.Stats.MovementPoints));
            List<short> ret = new List<short>();
            bool check(short cell)
            {
                if (cell < 0 || cell >= 560 || !Brain.State.CurrentMap.Get().Cells[cell].mov || !Brain.State.CurrentMap.Get().Cells[cell].allowWalkFight)
                    return false;
                foreach (short entity in Brain.FightManager.EntitiesDispositons.Get().Values)
                    if (cell == entity)
                        return false;
                return true;
            }
            foreach (short cell in ids)
                if (check(cell))
                    ret.Add(cell);
            return PathingUtils.CellsByDistance(ret.ToArray(), Brain.FightManager.Player.CellId);
        }

        #endregion

        private async Task<PerformMovementResult> PerformMovement(bool wantPo)
        {
            if (Brain.CurrentState.Get() != Brain.BrainState.Fight || Brain.FightManager.Player == null || Brain.FightManager.Player.Fighter.Stats.MovementPoints == 0 || IsStunt)
                return PerformMovementResult.NothingTodo;
            List<short> reach;
            if (IsCac)
                return PerformMovementResult.NothingTodo;
            else if ((Brain.State.UserConfig.Get().Ia == FightIaType.Fuiyard || Brain.FightManager.Player.IsCriticalState) && !wantPo)
                reach = GetFarsetReachable();
            else
                reach = GetNearsetFromEnnemyReachable();
            for (int i = 0; i < reach.Count; i++)
            {
                if (reach[i] != Brain.FightManager.Player.CellId && !Brain.FightManager.EntitiesDispositons.Get().Values.Contains(reach[i]))
                {
                    if (await Brain.FightManager.Move(reach[i]))
                        return PerformMovementResult.Success;
                }
            }
            return PerformMovementResult.Error;
        }

        #endregion

        #region Spell

        private bool SpellIsAvaiableForCast(SpellStackItem spell)
        {
            if (spell.Detail.CurrentLevel.ApCost > Brain.FightManager.Player.Fighter.Stats.ActionPoints)
                return false;
            if (!spell.CanBeCasted(Brain.FightManager.Player.InvockationRemaning))
                return false;
            //Lot of TODO here lol
            return true;
        }

        private bool SpellCastTestLost(short self, short dest)
        {
            Point selfPoint = PathingUtils.CellIdToCoord(self);
            Point destPoint = PathingUtils.CellIdToCoord(dest);
            short[] line = PathingUtils.PointsToCellids(LineOfSight.GetLine(selfPoint.X, selfPoint.Y, destPoint.X, destPoint.Y));
            for (int i = 0; i < line.Length - 1; i++)
            {
                short cell = line[i];
                if (!Brain.State.CurrentMap.Get().Cells[cell].los)
                    return false;
                foreach (short entity in Brain.FightManager.EntitiesDispositons.Get().Values)
                    if (cell == entity && (cell != dest || cell != self))
                        return false;
            }
            return true;
        }


        /// <summary>
        /// Check if we can cast @spell at @selfCellId to @targetCellid
        /// if selfCelli dis not our real cellid we performe the movement first
        /// </summary>
        /// <returns></returns>
        private async Task<bool> TryCastMayMove(SpellStackItem item, short selfCellId, short targetCell)
        {
            if (Brain.CurrentState.Get() != Brain.BrainState.Fight)
                return false;
            if (item.Detail.CurrentLevel.CastTestLos && !SpellCastTestLost(selfCellId, targetCell))
                return false;
            if (!await Brain.FightManager.Move(selfCellId))
                return false;
            if (!await Brain.FightManager.CastSpell(item, targetCell))
            {
                Error("Failed to cast spell !");
                return false;
            }
            Log("Spell casted !");
            return true;
        }


        /// <summary>
        /// Get all possible target cells for a given spell
        /// </summary>
        /// <param name="spell"></param>
        /// <returns></returns>
        private short[] GetTargetCells(SpellStackItem spell, short curr)
        {
            switch (spell.Dest)
            {
                case SpellCastType.Self:
                    return new short[] { curr};
                case SpellCastType.Allies:
                case SpellCastType.Ennemies:
                    return PathingUtils.CellsByDistance(GetEntitiesCells(spell.Dest == SpellCastType.Ennemies).ToArray(), curr);
                case SpellCastType.AroundSelf:
                    return PathingUtils.PointsToCellids(FilterUsedCells(FilterInvalideCells(SpellShape.GetInstance().GetSpellRange(curr, spell.Detail.CurrentLevel)).ToArray()).ToArray());
                case SpellCastType.AroundEnnemies:
                    short[] possibleCells = PathingUtils.PointsToCellids(SpellShape.GetInstance().GetSpellRange(curr, spell.Detail.CurrentLevel).ToArray());
                    Dictionary<short, int> dict = new Dictionary<short, int>();
                    possibleCells = (from item in possibleCells where Brain.State.CurrentMap.Get().Cells[item].mov && Brain.State.CurrentMap.Get().Cells[item].allowWalkFight && !GetEntitiesCells().Contains(item) select item).ToArray();
                    foreach (short cell in possibleCells)
                        dict[cell] = DistanceWithNearsetEntities(cell, true);
                    return (from item in dict orderby item.Value select item.Key).ToArray();
                default:
                    throw new NotImplementedException();
            }
        }


        private IDictionary<CastableSpellType, List<CastableSpell>> GetCastableSpells(out int AvaibleSpell)
        {
            AvaibleSpell = 0;
            List<CastableSpell> castable = new List<CastableSpell>();
            foreach (SpellStackItem spell in Brain.State.UserConfig.Get().SpellStack)
            {
                if (!SpellIsAvaiableForCast(spell)) continue; else AvaibleSpell++;
                // Need a custom movement function
                List<short> reashable = GetNearsetFromMeAndEnnemyReachable();
                //Brain.State.DebugCellRequest(targets, 1000, 0);
                SpellLevel spellLevel = Brain.State.Player.Spells.Get()[spell.SpellId].CurrentLevel;
                reashable.Insert(0, Brain.FightManager.Player.CellId);

                foreach (short pos in reashable)
                {
                    short[] targets = GetTargetCells(spell, pos);

                    List<Point> spellRange = (from item in FilterInvalideCells(SpellShape.GetInstance().GetSpellRange(pos, spellLevel)) where CellIsMov(item) select item).ToList(); 
                    if (spell.Dest == SpellCastType.Ennemies || spell.Dest == SpellCastType.Allies)
                    {
                        foreach (short target in targets)
                        {
                            bool found = false;
                            foreach (Point range in spellRange)
                                if (PathingUtils.CoordToCellId(range) == target)//TODO cast around cible
                                    found = true;
                            if (!found || !Brain.FightManager.CanCastSpell(spell, target))
                                continue;
                            castable.Add(new CastableSpell(pos, target, spell, spell.Dest == SpellCastType.Ennemies ? CastableSpellType.Attack : CastableSpellType.Heal, Brain));
                        }
                    }
                    else if (spell.Dest == SpellCastType.AroundSelf)
                    {
                        foreach (Point cell in spellRange)
                        {
                            if (!Brain.FightManager.CanCastSpell(spell, PathingUtils.CoordToCellId(cell)))
                                continue;
                            castable.Add(new CastableSpell(pos, PathingUtils.CoordToCellId(cell), spell, CastableSpellType.Invok, Brain));
                        }
                    }
                    else if (spell.Dest == SpellCastType.Self)
                    {
                        if (!Brain.FightManager.CanCastSpell(spell, Brain.FightManager.Player.CellId))
                            continue;
                        castable.Add(new CastableSpell(pos, Brain.FightManager.Player.CellId, spell, CastableSpellType.Boost, Brain));
                        break;// TODO check for zone effects
                    }
                    else if (spell.Dest == SpellCastType.AroundEnnemies)
                    {
                        targets = FilterUsedCells(targets);
                        foreach (Point cell in spellRange)
                        {
                            if (!Brain.FightManager.CanCastSpell(spell, PathingUtils.CoordToCellId(cell)))
                                continue;
                            //For now all around ennemies is counted as jump but a lot of todo here LOL
                            castable.Add(new CastableSpell(pos, PathingUtils.CoordToCellId(cell), spell, CastableSpellType.Jump, Brain));
                        }
                    }
                }
            }
            // this long line return castable spell grouped by type (attack, heal ect), all group items are sorted by it priority, each item withe same priority is sorted by movement cost ;)
            return (from item in castable group item by item.CastType into grouped select grouped).ToDictionary(item => item.Key, item => item.OrderBy(elem => (elem.MovementCost * 100000) * elem.Spell.Priority).ToList());
        }

        #endregion

        #region Main Logic

        int LastBoostTurn = 0;
        private async Task<bool> Next()
        {
            Log("{0} AP and {1} MP avaible, looking for something to do ...", Brain.FightManager.Player.Fighter.Stats.ActionPoints, Brain.FightManager.Player.Fighter.Stats.MovementPoints);
            // Looking for spell, in the stack order
            bool NoAttackPerformed = true;
            int AvaibleSpell;
            IDictionary<CastableSpellType, List<CastableSpell>> castable = GetCastableSpells(out AvaibleSpell);
            if (castable.Count > 0)
            {
                CastableSpell toCast = default(CastableSpell);

                //
                // In order
                //  Looking for attack or heal
                //  Looking for invkcation or boost if last turn we do not performe invockation or boost
                //  Looking for teleportation or something like this
                //  Re look for boost or invockation event if we performe it last turn
                if (castable.ContainsKey(CastableSpellType.Attack) || castable.ContainsKey(CastableSpellType.Heal))
                {
                    toCast = castable.ContainsKey(CastableSpellType.Heal) ? castable[CastableSpellType.Heal][0] : castable[CastableSpellType.Attack][0];
                    Log("Found possible attack (or heal), let try it target cell : {0} target spell : {1}", toCast.CellId, toCast.Spell.Name);
                }
                else if ((castable.ContainsKey(CastableSpellType.Invok) || castable.ContainsKey(CastableSpellType.Boost)) && LastBoostTurn-- == 0)
                {
                    toCast = castable.ContainsKey(CastableSpellType.Invok) ? castable[CastableSpellType.Invok][0] : castable[CastableSpellType.Boost][0];
                    Log("Found possible preparation (heal or invok), let it target cell : {0} target spell : {1}", toCast.CellId, toCast.Spell.Name);
                    LastBoostTurn = 1;
                }
                else if (castable.ContainsKey(CastableSpellType.Jump))
                {
                    toCast = (from item in castable[CastableSpellType.Jump] where !GetEntitiesCells().Contains(item.TargetCellId) orderby DistanceWithNearsetEntities(item.TargetCellId, true) select item).ElementAt(0);
                    Log("Found jump, let try it target cell {0}", toCast.CellId);
                }
                else if (castable.ContainsKey(CastableSpellType.Invok) || castable.ContainsKey(CastableSpellType.Boost))
                {
                    toCast = castable.ContainsKey(CastableSpellType.Invok) ? castable[CastableSpellType.Invok][0] : castable[CastableSpellType.Boost][0];
                    Log("Found possible preparation and no jump, let it target cell : {0} target spell : {1}", toCast.CellId, toCast.Spell.Name);
                    LastBoostTurn++;
                }
                else
                {
                    Error("No action found but castable list is not empty, maybe a CastableSpellType implementation is missing ?");
                    return false;
                }
                if (!await TryCastMayMove(toCast.Spell, toCast.CellId, toCast.TargetCellId))
                {
                    toCast.Spell.Failed = true;
                    Log("Error when processing castable spell action !");
                    return false;
                }
                if (toCast.Spell.Dest == SpellCastType.Ennemies || toCast.Spell.Dest == SpellCastType.AroundEnnemies)
                    NoAttackPerformed = false;
                return true;
            }
            // If no spell cast is possible performe a movement
            Log("No castable spell, looking for movement ...");
            return await PerformMovement(castable.Count == 0 && (AvaibleSpell > 0 || NoAttackPerformed)) == PerformMovementResult.Success;
        }

        public async Task ProcessTurn()
        {
            if (Brain.CurrentState.Get() != Brain.BrainState.Fight || Brain.FightManager.Player == null || Brain.State.CurrentMap.Get() == null)
            {
                Error("Player not loaded !");
                return;
            }
            while ( Brain.CurrentState.Get() == Brain.BrainState.Fight)
            {
                bool res;
                res = await Next();
                if (res)
                    await Task.Delay(DellayManager.GetInstance().Get(DellayManager.DellayType.FightTurnStepDellay));
                else
                    break;
            }
            Brain.FightManager.EndTurn();
        }

        #endregion

        #region Generic types

        enum PerformMovementResult
        {
            NothingTodo,
            Success,
            Error,
        }

        enum CastableSpellType
        {
            Attack,
            Heal,
            Boost,
            Jump,
            Invok,
        }

        struct CastableSpell
        {
            public short CellId { get; set; }
            public short TargetCellId { get; set; }
            public SpellStackItem Spell { get; set; }
            public int MovementCost { get; set; }
            public CastableSpellType CastType { get; set; }

            public CastableSpell(short cellid, short target, SpellStackItem spell, CastableSpellType castType, Brain bot)
            {
                CellId = cellid;
                TargetCellId = target;
                Spell = spell;
                CastType = castType;
                MovementCost = (int)PathingUtils.DistanceToPoint(PathingUtils.CellIdToCoord(cellid), PathingUtils.CellIdToCoord(bot.FightManager.Player.CellId));
            }
        }

        #endregion

    }
}
