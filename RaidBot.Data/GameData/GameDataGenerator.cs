using RaidBot.Data.IO.D2O;
using RaidBot.Protocol.DataCenter;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace RaidBot.Data.GameData
{
    public class GameDataGenerator
    {
        private GameDataIndex index = new GameDataIndex();
        public GeneratingStats Stats { get; private set; }
        public event EventHandler DataGenerated;
        private void OnDataGenerated()
        {
            if (DataGenerated != null)
                DataGenerated(this, new EventArgs());
        }
        public GameDataGenerator()
        {
            Stats = new GeneratingStats();
        }

        public void Generate(string[] inputFiles)
        {
            
            Stats.Generating = true;
            Stats.MainValue = 0;
            Stats.SubValue = 0;
            Stats.MainMaximum = inputFiles.Count();
            Stats.SubMaximum = 0;
            foreach(string file in inputFiles)
            {
                if (file.Contains("Idols") || file.Contains("Playlists"))
                    continue;
                if (file.Split('.').Reverse().ToArray()[0] == "d2o")
                {
                    Stats.MainStats = "Decompression de : " + file;
                    ParseFile(file);
                }
                Stats.MainValue++;
            }
            OnDataGenerated();
        }

        private void ParseFile(string file)
        {
            string output = string.Format("{0}\\{1}", DataSetting.Default.D2OFolderPath, file.Split('\\').Reverse().ToArray()[0].Replace(".d2o", ""));
            if (Directory.Exists(output))
                Directory.Delete(output, true);
            Directory.CreateDirectory(output);
                GameDataFileAccessor accesor = new GameDataFileAccessor(file);
            List<int> indexes = accesor.GetAllIndex();
            Type t = null;
            foreach (int i in indexes)
            {
                IData c = (IData)accesor.ReadClass(i);
				if (c == null)
					continue;
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(File.Create(output + "\\" + i.ToString() + ".rmf"), c);
                t = c.GetType();
            }
            index.AddDirectory(t, output);
        }
    }
}
