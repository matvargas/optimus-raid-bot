using RaidBot.Data;
using RaidBot.Data.GameData;
using RaidBot.Data.MapData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataExtractor
{
    class Program
    {
        private static MapDataGenerator mMapsGenerator;
        private static GameDataGenerator mGameDataGenerator;
        static void Main(string[] args)
        {
            //GenerateD2p(@"C:\Users\Zakaria\AppData\Local\Ankama\Dofus2\app\content\maps");
			//GenerateD2o(@"C:\Users\Zakaria\AppData\Local\Ankama\Dofus2\app\data\common");
        }
        private static void GenerateD2p(string path)
        {
            mMapsGenerator = new MapDataGenerator(path);
            mMapsGenerator.Generate();
            mMapsGenerator.Stats.PropertyChanged += ActualizeInterface;
        }
        private static void GenerateD2o(string path)
        {
            mGameDataGenerator = new GameDataGenerator();
            mGameDataGenerator.Stats.PropertyChanged += ActualizeInterface;
            mGameDataGenerator.Generate(Directory.GetFiles(path));
        }
        private static void ActualizeInterface(Object e, PropertyChangedEventArgs arg)
        {
            switch (arg.PropertyName)
            {
                case "MainStats":
                       Console.WriteLine(((GeneratingStats)e).MainStats);
                    break;
                case "SubStats":
                      Console.WriteLine(((GeneratingStats)e).SubStats);
                    break;
            }
        }
    }
}
