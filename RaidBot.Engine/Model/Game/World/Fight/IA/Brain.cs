using System;
using RaidBot.Engine.Manager;
using RaidBot.Engine.Controler.Game.Player;
using RaidBot.Engine.Model.Game.World.Fight.Shapes;
namespace RaidBot.Engine.Model.Game.World.Fight.IA
{
	public class Brain
	{
		public Movement Movement;
		public CastingSpells SpellsLauncher;
		public Placement Placement;
		public RaidFight Fight;
		public Cross cross = new Cross (0, 3);
		public DiagonalCross dcross = new DiagonalCross(0,3);


		PlayerControler Player
		{
			get
			{
				if (Fight.playedFighter == null)
					return null;
				return Fight.playedFighter;
			}
		}
		public Brain (RaidFight fight)
		{
			Fight = fight;
			Movement = new Movement (this);
			SpellsLauncher = new CastingSpells(this);
			Placement = new Placement (this);
		}
	}
}

