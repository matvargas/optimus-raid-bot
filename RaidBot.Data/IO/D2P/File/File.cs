using RaidBot.Common.IO;
using System;
using System.Collections.Generic;
using System.IO;

namespace RaidBot.Data.IO.D2P.File
{
    public class File
    {
        #region Declarations

        public string Path { get; set; }
        public BigEndianReader raw { get; set; }

        public Dictionary<uint, LoadedD2pChunk> LoadedMaps { get; set; }
        public Dictionary<string, LoadedD2pChunk> LoadedImages { get; set; }

        #endregion

        #region Constructeur

        public File(string _path)
        {
            LoadedMaps = new Dictionary<uint, LoadedD2pChunk>();
            LoadedImages = new Dictionary<string, LoadedD2pChunk>();
            Path = _path;
            raw = new BigEndianReader(System.IO.File.ReadAllBytes(Path));
            CheckHeader();
                GetFileContents();
        }

        #endregion

        #region Methode privee

        private uint Position;
        private uint LoadedMapsCount;

        private void CheckHeader()
        {
            byte param1 = raw.ReadByte();
            byte param2 = raw.ReadByte();

            if ((param1 != 2) || (param2 != 1))
            {
                throw new ArgumentException("[RaidBot.Data.IO.D2P.File] Invalid D2P File");
            }
            raw.SetPosition((int)raw.BaseStream.Length - 16);
            Position = raw.ReadUInt();
            LoadedMapsCount = raw.ReadUInt();
            raw.SetPosition((int)Position);
        }

        public void ReadIndex(int index)
        {
            LoadedD2pChunk LoadedMap = null;
            for (int i = 0; i <= LoadedMapsCount; i++)
            {
                LoadedMap = new LoadedD2pChunk(raw, Path);
                if (LoadedMap.IsInvalid)
                    continue;
                LoadedMaps.Add(LoadedMap.GetId(), LoadedMap);
            }
            raw.Close();
        }


        private void GetFileContents()
        {
            LoadedD2pChunk chunk = null;
            for (int i = 0; i <= LoadedMapsCount; i++)
            {
                chunk = new LoadedD2pChunk(raw, Path);
                if (chunk.IsInvalid)
                    continue;
                else if (chunk.IsMap)
                    LoadedMaps.Add(chunk.GetId(), chunk);
                else if (chunk.IsImage)
                    LoadedImages[chunk.GetStrId()] = chunk;
            }
            raw.Close();
        }

        #endregion

    }
}
