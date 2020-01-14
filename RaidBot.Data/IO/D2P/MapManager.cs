using RaidBot.Common.Extensions;
using RaidBot.Data.IO.D2P.File;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RaidBot.Data.IO.D2P
{
    public class MapManager : SafeSingelton
    {
        static MapManager manager = new MapManager();

        public static Map.Map SafeGetMap(uint mapId, byte[] key)
        {
            Map.Map ret = null;
            manager.SafeRun(() =>
            {
                ret = manager.GetMap(mapId, key);
            });
            return ret;
        }

        public Folder Folder { get; private set; }

        private MapManager()
        {
            Folder = new Folder(@"C:\Users\acorbeau\AppData\Local\Ankama\Dofus\app\content\maps");
        }


        Mutex mutex = new Mutex();

        public Map.Map GetMap(uint mapId, byte[] key)
        {
            mutex.WaitOne();
            foreach (File.File f in Folder.Files)
            {
                if (f.LoadedMaps.ContainsKey(mapId))
                {
                    Map.Map m = new Map.Map(f.LoadedMaps[mapId], key);
                    mutex.ReleaseMutex();
                    return m;
                }
            }
            mutex.ReleaseMutex();
            throw new NotImplementedException();
        }
    }
}
