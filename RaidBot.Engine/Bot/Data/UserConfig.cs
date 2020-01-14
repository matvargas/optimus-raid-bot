using RaidBot.Data.IO.D2O;
using RaidBot.Engine.Bot.Extension;
using RaidBot.Protocol.DataCenter;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Starksoft.Aspen.Proxy;
using RaidBot.Common.Network.Client;

namespace RaidBot.Engine.Bot.Data
{

    public enum FightIaType
    {
        Fuiyard,
        Tank
    }



    public enum FightPlacementType
    {
        None,
        Near,
        Far,
    }

    public enum SpellCastType
    {
        Self,
        Ennemies,
        Allies,
        AroundSelf,
        AroundAllies,
        AroundEnnemies,
    }


    [Serializable]
    public class UserConfig
    {
        const string ConfigDir = "config";
        public String FilePath { get; set; }

        public FightPlacementType FightPlacement { get; set; }
        public FightIaType Ia { get; set; }

        public bool AllowWatcher { get; set; }
        public bool OnlyGroup { get; set; }
        public bool RegenEmote { get; set; }
        public bool RegenObject { get; set; }

        public int GroupLevelMax { get; set; }
        public int GroupLevelMin { get; set; }
        public int MonsterLevelMin { get; set; }
        public int MonsterLevelMax { get; set; }

        public String ContextualScript { get; set; }

    
        public List<SpellStackItem> SpellStack { get; set; }

        public UserConfig()
        {

        }

        public UserConfig(String username, String characterId)
        {
            String dName = Path.Combine(ConfigDir, username);
            if (!Directory.Exists(dName))
                Directory.CreateDirectory(dName);
            FilePath = Path.Combine(ConfigDir, username, String.Format("{0}.xml", characterId));
            AllowWatcher = false;
            OnlyGroup = true;
            RegenEmote = true;
            GroupLevelMax = 10000;
            MonsterLevelMax = 10000;
            SpellStack = new List<SpellStackItem>();
            ContextualScript = "function charac(avail)\nend\n\nfunction inventory()\nend\n";
        }

        public void Save(String path = null)
        {
            if (path == null)
                path = FilePath;
            FilePath = path;
            XmlSerializer serializer = new XmlSerializer(typeof(UserConfig));
            Stream fs = File.Open(path, FileMode.Create);
            serializer.Serialize(fs, this);
            fs.Close();
        }

        public static UserConfig Load(String username, String characterId)
        {
            UserConfig cfg = Load(Path.Combine(ConfigDir, username, String.Format("{0}.xml", characterId)));
            if (cfg == null)
                cfg = new UserConfig(username, characterId);
            return cfg;
        }

        public static UserConfig Load(String path = null)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(UserConfig));
                return (UserConfig)serializer.Deserialize(File.Open(path, FileMode.Open));
            }
            catch
            {
                return null;
            }
        }
    }

    [Serializable]
    public class SpellStackItem
    {
        public SpellCastType Dest { get; set; }
        public int SpellId { get; set; }
        public string Name { get; set; }
        public int Priority { get; set; }
        public bool Failed { get; set; }

        [NonSerialized]
        ExtensionDomain Predicate;

        public String PredicateStr { get; set; }

        /// <summary>
        /// Key is actorId and value the number of cast
        /// </summary>
        Dictionary<double, int> CastHistory;
        int Delay;
        int NbrCast;

        public void Clear()
        {
            NbrCast = 0;
            CastHistory = new Dictionary<double, int>();
            Delay = 0;
        }

        public void MyTurn()
        {
            Failed = false;
            CastHistory = new Dictionary<double, int>();
            Delay = Delay > 0 ? Delay - 1 : 0;
            NbrCast = 0;
        }

        public bool IsInvocke { get; set; }
        
        public bool CanBeCasted(int invockRemaning)
        {
            if (Failed)
                return false;
            if (IsInvocke && invockRemaning <= 0)
                return false;
            if (Delay > 0)
                return false;
            if (NbrCast >= Detail.CurrentLevel.MaxCastPerTurn && Detail.CurrentLevel.MaxCastPerTurn != 0)
                return false;
            if (!Predicate.Script.Call(Predicate.Script.Globals["check"]).Boolean)
                return false;
            return true;
        }

        public bool CanBeCasted(double dest, int invockRemaning)
        {
            if (!CanBeCasted(invockRemaning))
                return false;
            if (CastHistory.ContainsKey(dest) && CastHistory[dest] >= Detail.CurrentLevel.MaxCastPerTarget && Detail.CurrentLevel.MaxCastPerTarget != 0)
                return false;
            return true;
        }

        public bool Casted(double dest, int invockRemaning)
        {
            if (!CanBeCasted(dest, invockRemaning))
                return false;
            if (!Casted(invockRemaning))
                return false;
            if (CastHistory.ContainsKey(dest))
                CastHistory[dest]++;
            else
                CastHistory[dest] = 1;
            return true;
        }

        public bool Casted(int invockRemaning)
        {
            if (Detail.CurrentLevel.MinCastInterval > 0)
                Delay = (int)Detail.CurrentLevel.MinCastInterval;
            NbrCast++;
            return true;
        }


        public SpellDetail Detail;

        public SpellStackItem()
        {
            IsInvocke = false;
            CastHistory = new Dictionary<double, int>();
            Delay = 0;
        }

        public SpellStackItem(SpellDetail spell, int priority, SpellCastType dest, string predicated)
        {
            IsInvocke = false;
            CastHistory = new Dictionary<double, int>();
            Delay = 0;
            Dest = dest;
            SpellId= spell.Item.SpellId;
            Priority = priority;
            Name = spell.Name;
            Detail = spell;
            PredicateStr = predicated;
        }

        public void LoadPredicate(Brain brain)
        {
            Predicate = new ExtensionDomain(ExtensionType.SpellPredicate, brain);
            Predicate.Load(PredicateStr);
        }

        public override string ToString()
        {
            return String.Format("{0} Cast on {1} (Priority : {2})", Name, Dest, Priority);
        }
    }
}
