using RaidBot.Common.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidBot.UI
{
    public class ScriptsManager: SafeSingelton
    {
        private static ScriptsManager instance = new ScriptsManager();
        public static void SafeWith(Action<ScriptsManager> fnc)
        {
            instance.SafeRun(() =>
            {
                fnc(instance);
            });
        }

        public static string Load(String name)
        {
            string txt = null;
            instance.SafeRun(() =>
            {
                txt = File.ReadAllText(instance.Trajets[name]);
            });
            return txt;
        }

        public static void Save(string name, string content)
        {
            instance.SafeRun(() =>
            {
                instance.Trajets[name] = String.Format("trajets\\{0}.lua", name);
                File.WriteAllText(instance.Trajets[name], content);
            });
        }

        public Dictionary<String, String> Trajets { get; private set; }

        public event Action<Dictionary<String, String>> TrajetsUpdated;
        private void OnTrajetsUpdated()
        {
            if (TrajetsUpdated != null)
                TrajetsUpdated(Trajets);
        }

        private FileSystemWatcher watcher = new FileSystemWatcher();

        private ScriptsManager()
        {
            if (!Directory.Exists("trajets"))
                Directory.CreateDirectory("trajets");
            watcher = new FileSystemWatcher();
            watcher.Path = "trajets";
            watcher.Changed += Watcher_Changed;
            watcher.Created += Watcher_Created;
            watcher.Deleted += Watcher_Deleted;
            watcher.Renamed += Watcher_Renamed;
            watcher.EnableRaisingEvents = true;
            Refresh();
        }

        private void Watcher_Renamed(object sender, RenamedEventArgs e)
        {
            Refresh();
        }

        private void Watcher_Deleted(object sender, FileSystemEventArgs e)
        {
            Refresh();
        }

        private void Watcher_Created(object sender, FileSystemEventArgs e)
        {
            Refresh();
        }

        private void Watcher_Changed(object sender, FileSystemEventArgs e)
        {
            Refresh();
        }

        private void Refresh()
        {
            Trajets = new Dictionary<string, string>();
            foreach (string path in Directory.GetFiles("trajets"))
            {
                Trajets[Path.GetFileNameWithoutExtension(path)] = path;
            }
            OnTrajetsUpdated();
        }
    }
}
