using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Security.Cryptography;
using System.Text;
using RaidBot.Data.IO.D2P.File;
using RaidBot.Common.IO;


namespace RaidBot.Data.IO.D2P.Map
{
    [Serializable()]
    public class Map
    {

        #region Declarations
        [NonSerialized()]
        private BigEndianReader mRaw;
        public BigEndianReader Raw
        {
            get { return mRaw; }
            set { mRaw = value; }
        }

        public const string DefaultEncryptionKeyString = "649ae451ca33ec53bbcbcc33becf15f4";
        public const int MAPCELLSCOUNT = 560;
        public byte[] EncryptionKey { get; set; }
        public int MapVersion { get; set; }
        public bool Encrypted { get; set; }
        public uint EncryptionVersion { get; set; }
        public int GroundCRC { get; set; }

        public int ZoomScale = 1;
        public int ZoomOffsetX { get; set; }
        public int ZoomOffsetY { get; set; }

        public int GroundCacheCurrentlyUsed = 0;
        public uint Id { get; set; }
        public uint RelativeId { get; set; }
        public int MapType { get; set; }
        public int BackgroundsCount { get; set; }
        public List<Fixture> BackgroundFixtures { get; set; }
        public int ForegroundsCount { get; set; }
        public List<Fixture> ForegroundFixtures { get; set; }
        public int SubareaId { get; set; }
        public int ShadowBonusOnEntities { get; set; }
        public uint BackgroundColor { get; set; }
        public int BackgroundRed { get; set; }
        public int TacticalModelTemplateId { get; set; }
        public int BackgroundGreen { get; set; }
        public int BackgroundBlue { get; set; }
        public int TopNeighbourId { get; set; }
        public int BottomNeighbourId { get; set; }
        public int LeftNeighbourId { get; set; }
        public int RightNeighbourId { get; set; }
        public Dictionary<int, byte> ArrowsCells { get; set; }
        public bool UseLowPassFilter { get; set; }
        public bool UseReverb { get; set; }
        public int PresetId { get; set; }
        public int CellsCount { get; set; }
        public int LayersCount { get; set; }
        public bool IsUsingNewMovementSystem = false;
        public List<Layer> Layers { get; set; }
        public List<CellData> Cells { get; set; }
        public WorldPoint Position { get; set; }

        public string HashCode { get; set; }
        [NonSerialized()]
        private byte[] data;

        #endregion

        #region Constructeur

        public Map()
        {

        }
        public Map(LoadedD2pChunk loadedMap, byte[] key = null)
        {
            EncryptionKey = key;
            Initializeraw(loadedMap);
            Start();
        }

        public Map(BigEndianReader raw, byte[] key = null)
        {
            EncryptionKey = key;
            Raw = raw;
            Start();
        }

        private void Start()
        {
            if (EncryptionKey == null)
                EncryptionKey = Encoding.UTF8.GetBytes(DefaultEncryptionKeyString);
            InitializeMap(EncryptionKey);
            Raw.Close();
        }

        #endregion

        #region Methodes Publiques

        private void Initializeraw(LoadedD2pChunk LoadedMap)
        {
            FileStream LoadedMapStream = new FileStream(LoadedMap.Path, FileMode.Open, FileAccess.Read);
            BigEndianReader LoadedMapraw = new BigEndianReader(LoadedMapStream);
            LoadedMapraw.BaseStream.Position = LoadedMap.Offset;

            byte[] LoadedMapBuffer = LoadedMapraw.ReadBytes((int)LoadedMap.BytesCount);
            byte[] LoadedMapBufferWithoutHeader = new byte[LoadedMapBuffer.Length - 2];
            Array.Copy(LoadedMapBuffer, 2, LoadedMapBufferWithoutHeader, 0, LoadedMapBuffer.Length - 2);

            StringBuilder mapHashCodeBuilder = new StringBuilder();
            byte[] LoadedMapMd5Buffer = MD5.Create().ComputeHash(LoadedMapBufferWithoutHeader);

            for (int i = 0; i < LoadedMapMd5Buffer.Length; i++)
                mapHashCodeBuilder.Append(LoadedMapMd5Buffer[i].ToString("X2"));

            HashCode = mapHashCodeBuilder.ToString();


            MemoryStream decompressMapStream = new MemoryStream(LoadedMapBufferWithoutHeader);
            DeflateStream mapDeflateStream = new DeflateStream(decompressMapStream, CompressionMode.Decompress);
            Raw = new BigEndianReader(mapDeflateStream);
            MemoryStream stream = new MemoryStream();
            Raw.BaseStream.CopyTo(stream);
            data = stream.ToArray();
            Raw = new BigEndianReader(data);
            LoadedMapStream.Close();
        }

        public byte[] GetRMFile()
        {
            return data;
        }

        private void InitializeMap(byte[] encryptionKey)
        {
            int header = Raw.ReadByte();
            int dataLen = 0;
            int i = 0;
            byte[] decryptionKey = encryptionKey;

            if (header != 77)
                throw new FormatException("Unknown file header, first byte must be 77");


            MapVersion = this.Raw.ReadSByte();
            Id = this.Raw.ReadUInt();

            if (MapVersion >= 7)
            {
                Encrypted = Raw.ReadBoolean();
                EncryptionVersion = (uint)Raw.ReadSByte();
                dataLen = this.Raw.ReadInt();

                if (Encrypted == true)
                {
                    byte[] encryptedData = Raw.ReadBytes(dataLen);
                    for (i = 0; i < encryptedData.Length; i++)
                        encryptedData[i] = (byte)(encryptedData[i] ^ decryptionKey[i % decryptionKey.Length]);
                    Raw = new BigEndianReader(new MemoryStream(encryptedData));
                }
            }

            RelativeId = Raw.ReadUInt();
            Position = new WorldPoint(RelativeId);

            MapType = Raw.ReadSByte();
            SubareaId = Raw.ReadInt();
            TopNeighbourId = Raw.ReadInt();
            BottomNeighbourId = Raw.ReadInt();
            LeftNeighbourId = Raw.ReadInt();
            RightNeighbourId = Raw.ReadInt();
            ShadowBonusOnEntities = Raw.ReadInt();

            if (MapVersion >= 9)
            {
                Raw.ReadInt(); // ARGB
                Raw.ReadUInt(); // Grid ARGB
            }
            else if (MapVersion >= 3)
            {
                BackgroundRed = Raw.ReadSByte();
                BackgroundGreen = Raw.ReadSByte();
                BackgroundBlue = Raw.ReadSByte();

                BackgroundColor = (uint)((BackgroundRed & 255) << 16 | (BackgroundGreen & 255) << 8 | BackgroundBlue & 255);
            }

            if (MapVersion >= 4)
            {
                ZoomScale = (ushort)(Raw.ReadUShort() / 100);
                ZoomOffsetX = Raw.ReadShort();
                ZoomOffsetY = Raw.ReadShort();
            }

            if (MapVersion > 10)
            {
                this.TacticalModelTemplateId = Raw.ReadInt();
            }


            UseLowPassFilter = Raw.ReadSByte() == 1;
            UseReverb = Raw.ReadSByte() == 1;

            if (UseReverb == true)
                PresetId = Raw.ReadInt();
            else
                PresetId = -1;


            BackgroundsCount = Raw.ReadSByte();

            BackgroundFixtures = new List<Fixture>();

            for (i = 0; i < BackgroundsCount; i++)
            {
                Fixture backgroundFixture = new Fixture(Raw);
                BackgroundFixtures.Add(backgroundFixture);
            }


            ForegroundsCount = Raw.ReadSByte();

            ForegroundFixtures = new List<Fixture>();

            for (i = 0; i < ForegroundsCount; i++)
            {
                Fixture foregroundFixture = new Fixture(Raw);
                ForegroundFixtures.Add(foregroundFixture);
            }


            Raw.ReadInt();
            GroundCRC = Raw.ReadInt();
            LayersCount = Raw.ReadSByte();
            Layers = new List<Layer>();

            for (i = 0; i < LayersCount; i++)
            {
                Layer layer = new Layer(Raw, (sbyte)MapVersion);
                Layers.Add(layer);
            }


            CellsCount = MAPCELLSCOUNT;
            Cells = new List<CellData>();

            ArrowsCells = new Dictionary<int, byte>();
            for (i = 0; i < CellsCount; i++)
            {
                CellData cell = new CellData(Raw, (sbyte)MapVersion, i, this);
                Cells.Add(cell);
                if (cell.leftArrow) ArrowsCells[i]= 1;
                if (cell.rightArrow) ArrowsCells[i] = 2;
                if (cell.topArrow) ArrowsCells[i] = 3;
                if (cell.bottomArrow) ArrowsCells[i] = 4;
            }
        }

        #endregion

    }
}
