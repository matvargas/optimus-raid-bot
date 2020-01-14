using RaidBot.Common.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidBot.Data.IO.ELE
{
    public class ElementsAdapter
    {
        private static ElementsAdapter instance;

        public static ElementsAdapter GetInstance()
        {
            if (instance == null)
                instance = new ElementsAdapter(@"maps\elements.ele");
            return instance;
        }

        public Elements Elem { get; }

        public ElementsAdapter(String path)
        {
            byte[] buff = File.ReadAllBytes(path);
            BigEndianReader reader = new BigEndianReader(buff);
            byte[] raw;
            if (reader.ReadByte() != 69)
            {
                MemoryStream st = new MemoryStream(buff);
                st.Seek(2, SeekOrigin.Begin);
                DeflateStream ds = new DeflateStream(st, CompressionMode.Decompress);
                reader = new BigEndianReader(ds);
                if (reader.ReadByte() != 69)
                    throw new Exception("Malformated and uncompressed elements file !");
                MemoryStream mst = new MemoryStream();
                ds.CopyTo(mst);
                mst.Seek(0, SeekOrigin.Begin);
                reader = new BigEndianReader(mst);
            }
            Elem = new Elements(reader);
        }
    }
}
