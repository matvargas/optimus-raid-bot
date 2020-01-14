using RaidBot.Common.Extensions;
using RaidBot.Data.IO.D2P.File;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidBot.Data.IO.D2P
{
    public class GfxManager : SafeSingelton
    {
        private static GfxManager instance = new GfxManager("gfx/items");

        public static Image SafeGetItem(string id)
        {
            Byte[] data = new byte[0];
            instance.SafeRun(() =>
            {
                foreach (File.File f in instance.itemsFolder.Files)
                {
                    if (f.LoadedImages.ContainsKey(id))
                    {
                        data = f.LoadedImages[id].GetData();
                    }
                }
            });
            Bitmap img =  new Bitmap(new MemoryStream(data));
            img.SetResolution(img.HorizontalResolution / 2, img.VerticalResolution / 2);
            return img;
        }

        private Folder itemsFolder;

        private GfxManager(String itemsFolderPath)
        {
            itemsFolder = new Folder(itemsFolderPath);
        }
    }
}
