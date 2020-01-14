using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace RaidBot.Data.GameData
{
    public class GameDataAdapter
    {
        public static T GetClass<T>(int id)
        {
            if (typeof(T).Name == "Npc")
                return default(T);
            var str = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\"+ DataSetting.Default.D2OFolderPath + @"\" +typeof(T).Name + @"s\" + id.ToString()  + ".rmf";
            if (File.Exists(str))
            {
                BinaryFormatter deserializer = new BinaryFormatter();
                return (T)deserializer.Deserialize(File.Open(str, FileMode.OpenOrCreate , FileAccess.Read , FileShare.Read )) ;
            }
            else
            {
                throw new Exception("Unknow data class for id : " + id.ToString());
            }
        }

        public static T[] GetAllClass<T>()
        {
            var str = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\" + DataSetting.Default.D2OFolderPath + @"\" + GetDirectory(typeof(T).FullName) + "s" ;
            if (Directory.Exists(str))
            {
                string[] files = Directory.GetFiles(str);
                List<T> retval = new List<T>();
                BinaryFormatter deserializer = new BinaryFormatter();
                foreach (string file in files)
                {
                    retval.Add((T)deserializer.Deserialize(File.Open(file, FileMode.Open)));
                }
                return retval.ToArray();
            }
            else
                throw new Exception("Unknow data classes for " + typeof(T).ToString());
        }

        private static string GetDirectory(string t)
        {
            return string.Format(  t.Split('.').Reverse().ToArray()[0]);
        }
    }
}
