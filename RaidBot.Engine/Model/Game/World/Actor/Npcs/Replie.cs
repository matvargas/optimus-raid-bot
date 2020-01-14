using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidBot.Engine.Model.Game.World.Actor.Npcs
{
   public class Replie
    {
    public int ReplieId;
    public string ReplieText;

    public Replie(int id, string text)
    {
      ReplieId = id;
      ReplieText = text;
    }
    }
}
