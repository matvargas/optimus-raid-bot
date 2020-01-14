using RaidBot.Common.IO;
using RaidBot.Data.IO.D2I;
using RaidBot.Data.IO.D2O.DataCenter;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RaidBot.Data.IO.D2O
{
    public class GameDataFileAccessor
    {
        #region Declaration

        private string FilePath;
        private Stream stream;
        private BigEndianReader reader;
        private Dictionary<int, int> indexes = new Dictionary<int, int>();
        public Dictionary<int, GameDataClassDefiniton> ClasseDefinitions { get; private set; }

        #endregion

        #region Metode

        #region Public

        public static GameDataClassDefiniton[] ReadClassDefinitions(string path)
        {
            
            GameDataFileAccessor acessor = new GameDataFileAccessor(path);
            return acessor.ClasseDefinitions.Values.ToArray();
        }

        public GameDataFileAccessor(string Path)
        {
            FilePath = Path;
            ClasseDefinitions = new Dictionary<int, GameDataClassDefiniton>();
            stream = new FileStream(Path, FileMode.Open);
            reader = new BigEndianReader(stream);
            string header = Encoding.Default.GetString(reader.ReadBytes(3));
            int indexPos = reader.ReadInt();
            reader.SetPosition(indexPos);
            int indexCount = reader.ReadInt();
            for (int i = 1; i < indexCount; i += 8)
            {
                int index = reader.ReadInt();
                int pos = reader.ReadInt();
                indexes.Add(index, pos);
            }
            int classesCount = reader.ReadInt();
            for (int i = 0; i < classesCount; i++)
            {
                int classId = reader.ReadInt();
                ClasseDefinitions.Add(classId, ReadClassDefinition());
            }
            foreach (GameDataClassDefiniton def in ClasseDefinitions.Values)
            {
                def.SetField(ClasseDefinitions);
            }
        }

        private GameDataClassDefiniton ReadClassDefinition()
        {
            string className = reader.ReadUTF();
            string classNamespace = reader.ReadUTF();
            GameDataClassDefiniton @class = new GameDataClassDefiniton(className);

            int fieldCount = reader.ReadInt();
            for (int i = 0; i < fieldCount; i++)
            {
                string fieldName = reader.ReadUTF();
                @class.AddField(fieldName, reader);
            }
            return @class;
        }

        #endregion

        #region Privée


        public T ReadClass<T>(int index)
        {
            reader.SetPosition(indexes[index]);
            int classId = reader.ReadInt();
            T def =  (T)ClasseDefinitions[classId].Read(reader);
            return def;
        }
        
        public object ReadClass(int index)
        {
            return ReadClass<object>(index);
        }
        
        public List<T> ReadAllClass<T>()
        {
            List<T> allContent = new List<T>();
            foreach (int key in indexes.Keys)
            {
                reader.SetPosition(indexes[key]);
                int classId = reader.ReadInt();
                allContent.Add((T)ClasseDefinitions[classId].Read(reader));
            }
            return allContent;
        }

        public List<int> GetAllIndex()
        {
            List<int> ret = indexes.Keys.ToList();
            return ret;
        }

        public void Close()
        {
            stream.Close();
            reader.Dispose();
        }

        #endregion

        #endregion
    }
}