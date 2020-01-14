using RaidBot.Engine.Model.Game.World.Actor.Fight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RaidBot.Engine.Controler.Game.Player;
using RaidBot.Protocol.Messages;
using RaidBot.Engine.Model.Game.Player.Spells;
using RaidBot.Engine.Utility.Pathfinding;
using RaidBot.Protocol.Enums;
using RaidBot.Engine.Model.Game.World.Fight.Shapes;
using RaidBot.Engine.Model.Game.Player.Fight;
using RaidBot.Engine.Model.Game.World.Fight.Enums;
namespace RaidBot.Engine.Model.Game.World.Fight.IA
{
  public  class CastingSpells
    {
		public Brain mBrain;
		public List<SpellData> Spells
		{
			get{ return Fight.playedFighter.PlayerSpells.Spells; }
		}
       public List<UsableSpellModel> UsableSpells
        {
           get
            {
                return Player.PlayerFightInformations.UsableSpells;
            }
        }


		RaidFight Fight
		{
			get
			{
				return mBrain.Fight;
			}
		}
		PlayerControler Player
		{
			get
			{
				return Fight.playedFighter;
			}
		}
        FighterModel mFocusedFighter;
        FighterModel FocusedFighter
        {
            get
            {
                if (mFocusedFighter != null && mFocusedFighter.Alive)
                    return mFocusedFighter;
                do
                    mFocusedFighter = Fight.GetNearestEnnemie();
                while (!mFocusedFighter.Alive);
                return mFocusedFighter;
            }
        }
		public CastingSpells(Brain brain)
		{
			mBrain = brain;
		}
		public void StartFight()
		{
            List<SpellData> AttackingSpells = GetAttackingSpell();
            if (AttackingSpells != null)
            {
                foreach (SpellData spell in AttackingSpells)
                {
                    if (CanUseSpell(spell))
                        Attack(spell);
                }
            }


		}

		public void LaunchSpell(ushort SpellId , short Cellid)
		{
			Fight.mHost.SendMessage (new GameActionFightCastRequestMessage (SpellId, Cellid));
		}
       

      public bool Attack(SpellData spell)
        {
          if (isInRange(spell , FocusedFighter.CellId))
          {
              int neededPm = GetNeededMp(spell, FocusedFighter.CellId);
              if (neededPm > Player.PlayerFightInformations.Stats.movementPoints)
              {
                  mBrain.Movement.sink();
                  return false;
              }
              mBrain.Movement.sink(neededPm);
              LaunchSpell((ushort)spell.SpellId, FocusedFighter.CellId);
              return true;    
          }
            return false;
        }
	 public List<SpellData> GetAttackingSpell(int priority = 0)
		{
            if (UsableSpells.Count > 0 && UsableSpells.Any(SpellData => SpellData.Cible == Frames.Game.World.Fight.CibleEnum.ENNEMY))
            return UsableSpells.Where(SpellData => SpellData.Cible == Frames.Game.World.Fight.CibleEnum.ENNEMY).ToList().OrderByDescending(spell => spell.Priority).ToList().Select(spell => spell.Spell).ToList(); 
            mBrain.Fight.mHost.logger.Log("Aucun sort d'attaque n'est configuré !", Common.Default.Loging.LogLevelEnum.Error);
			return null;
		}
      public bool isInRange(SpellData spell , int CellId)
     {
         if (!mBrain.Fight.finder.matrix.Any(cell => cell.Key == CellId))
             return false;

          if(mBrain.Fight.finder.matrix[Player.PlayerBaseInformations.CellId].ManhattanDistanceTo(mBrain.Fight.finder.matrix[CellId]) < spell.SpellLevel.Range)
          {
              return true;
          }
          return false;
     }
      public int GetNeededMp(SpellData spell , int CellId) // nombre de pm pour atteindre la portée du sort
      {
          if (isInRange(spell, CellId) || !mBrain.Fight.finder.matrix.Any(cell => cell.Key == CellId))
          return -1;

          return (int)Fight.finder.matrix[Player.PlayerBaseInformations.CellId].ManhattanDistanceTo(Fight.finder.matrix[FocusedFighter.CellId]) - (int)spell.SpellLevel.Range; 
      }
      public int GetNeededPMToFocusedFighter() //nombre de PM pour atteindre un player
      {
          if (mBrain.Fight.finder.matrix[Player.PlayerBaseInformations.CellId].GetConnectedCells().Any(id => id == FocusedFighter.CellId) || !mBrain.Fight.finder.matrix.Any(cell => cell.Key == FocusedFighter.CellId))
              return -1;
          return (int)Fight.finder.matrix[Player.PlayerBaseInformations.CellId].ManhattanDistanceTo(Fight.finder.matrix[FocusedFighter.CellId]);
      }
     public bool CanUseSpell(SpellData spell)
      {
          if (Player.PlayerFightInformations.Stats.actionPoints < spell.SpellLevel.ApCost)
              return false;
          else if (isInRange(spell , FocusedFighter.CellId))
          {
              int needpm = GetNeededMp(spell, FocusedFighter.CellId);
              if (needpm > Player.PlayerFightInformations.Stats.movementPoints)
                  return false;
              else
                  return true;
          }
          return true;
      }

    }
}
