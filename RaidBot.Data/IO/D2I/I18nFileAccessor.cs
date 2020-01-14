using RaidBot.Common.Extensions;
using RaidBot.Common.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RaidBot.Data.IO.D2I
{
    public sealed class I18nFileAccessor : SafeSingelton
    {
        private static I18nFileAccessor instance = new I18nFileAccessor(@"data\i18n\i18n_fr.d2i");

        public static string SafeGetText(int id)
        {
            string txt = null;
            instance.SafeRun(() =>
            {
                txt = instance.GetText(id);
            });
            return txt;
        }

        public bool IsInitialized { get; private set; }
        public string FilePath { get; private set; }
        private BigEndianReader reader;
        private Dictionary<int, int> indexes;
        private Dictionary<string, int> textIndexes;

        public static implicit operator I18nFileAccessor(string filepath)
        {
            return new I18nFileAccessor(filepath);
        }
        public static implicit operator string(I18nFileAccessor filepath)
        {
            return filepath.FilePath;
        }
        public I18nFileAccessor(string filePath)
        {
            if (!File.Exists(filePath))
                throw new Exception("Unknown d2i file in " + filePath);
            FilePath = filePath;
            reader = new BigEndianReader(new MemoryStream(File.ReadAllBytes(filePath), false));
            indexes = new Dictionary<int, int>();
            textIndexes = new Dictionary<string, int>();
            Initialize();
        }

        private Mutex mutex = new Mutex();

        public string GetText(int index)
        {
            mutex.WaitOne();
            if (indexes.ContainsKey(index))
            {
                reader.Seek(indexes[index], SeekOrigin.Begin);
                string res = reader.ReadUTF();
                mutex.ReleaseMutex();
                return (res);
            }
            mutex.ReleaseMutex();
            return "undefined text";
        }

        public string GetText(string index)
        {
            mutex.WaitOne();
            if (textIndexes.ContainsKey(index))
            {
                reader.Seek(textIndexes[index], SeekOrigin.Begin);
                string res = reader.ReadUTF();
                mutex.ReleaseMutex();
                return res;
            }
            mutex.ReleaseMutex();
            return "undefined text";
        }

        private void Initialize()
        {
            var indexPos = reader.ReadInt();
            reader.Seek(indexPos, SeekOrigin.Begin);
            var indexLen = reader.ReadInt();
            int addOffset = 0;
            for (int i = 0; i < indexLen; i += 9)
            {
                var key = reader.ReadInt();
                byte nbAdditionnalStrings = reader.ReadByte();
                var dataPos = reader.ReadInt();
                var pos = (int)reader.Position;
                indexes.Add(key, dataPos);
                while (nbAdditionnalStrings-- > 0)
                {
                    dataPos = reader.ReadInt();
                    pos = (int)reader.Position;
                    reader.Seek(dataPos + addOffset, SeekOrigin.Begin);
                    string unusedString = reader.ReadUTF();
                    reader.Seek(pos, SeekOrigin.Begin);
                    i += 4;
                }
            }
            int lastOffset = reader.ReadInt() + (int)reader.Position;

            int locpos = (int)reader.Position;

            /* while (locpos < lastOffset)   le text index ne sont pas chargé, sa prent trop longtemps :3
             {
                 string key = reader.ReadUTF();
                 int dataPos = reader.ReadInt();
                 if (!textIndexes.ContainsKey(key))
                 textIndexes.Add(key, dataPos);
             }*/
        }
    }
}
