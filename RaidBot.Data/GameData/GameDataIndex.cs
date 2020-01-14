using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidBot.Data.GameData
{
    public class GameDataIndex:Dictionary<Type,string[]>
    {
        public GameDataIndex() : base() { }

        public void AddDirectory(Type t ,string path)
        {
            if (t != null)
            Add(t, Directory.GetFiles(path));
        }
    }
}
