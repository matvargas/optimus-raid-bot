using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MoonSharp.Interpreter;
using RaidBot.Data.IO.D2P.Map;
using RaidBot.Engine.Bot;
using RaidBot.Engine.Bot.Data;
using RaidBot.Engine.Bot.Data.Context;
using RaidBot.Engine.Bot.Extension;
using RaidBot.Engine.Bot.Managers;
using RaidBot.Engine.Bot.Managers.Extension.Extensions;
using RaidBot.Engine.Utility;
using RaidBot.Protocol.Types;
using static RaidBot.Engine.Bot.Managers.DellayManager;

namespace RaidBot.Engine.Bot.Managers.Extension
{
    public class ExtensionManager : Manager
    {
        #region Declarations

        public Random Rnd { get; }
        const uint ACCESS_TRAJET = 0x01;
        public ObservableProperty<Script> Trajet { get; set; }
        public ObservableProperty<Script> ContextualTrajet { get; set; }
        public bool NeedContextualTrajetRefresh { get; set; }
        public List<KeyValuePair<ExtensionHandlerAttribute.PriorityEnum, LoadedExtension>> Loaded { get; private set; }

        #endregion

        #region Contextual trajet

        private void LoadContextualTrajet()
        {
            try
            {
                ExtensionDomain ext = new ExtensionDomain(ExtensionType.Trajet, Brain);
                ext.Load(Brain.State.UserConfig.Get().ContextualScript);
                ContextualTrajet = new ObservableProperty<Script>(ext.Script);
            }
            catch
            {
                Error("Can't load contextual script, set it empty");
                ContextualTrajet = new ObservableProperty<Script>(new Script());
            }
        }

        public void CallContextualTrajetEvent(ContextualTrajetEvents e)
        {

            if (NeedContextualTrajetRefresh)
                LoadContextualTrajet();
            try
            {
                switch (e)
                {
                    case ContextualTrajetEvents.CharactPointAvail:
                        ContextualTrajet.Get().Call(ContextualTrajet.Get().Globals.Get("charac"), Brain.State.Player.Characteristics.Get().StatsPoints);
                        break;
                    case ContextualTrajetEvents.InventoryRefresh:
                        ContextualTrajet.Get().Call(ContextualTrajet.Get().Globals.Get("inventory"));
                        break;
                }
            }
            catch (Exception ex)
            {
                Error(ex.ToString());
            }
        }

        #endregion

        #region Main Logic

        public ExtensionManager(Brain bot) : base(bot)
        {
            Rnd = new Random();
            Trajet = new ObservableProperty<Script>(null);
            NeedContextualTrajetRefresh = true;
            LoadAllExtensions();

            Brain.State.MapReady += () => { Trajet_changed(null); };
            Trajet.changed += Trajet_changed;
        }

        public void RegisterExtension(IExtensionHost extension)
        {
            ExtensionDomain trajet = extension.ParseContent();
            if (trajet != null)
            {
                Trajet.Set(trajet.Script);
            }
            else
                Trajet.Set(null);
        }

        #endregion

        #region Handlers

        private async Task WaitRpReady()
        {

        }

        private async void CurrentMap_changed(Map data)
        {
            if (data == null)
            {
                Warn("Map is not loaded !");
                return;
            }
            await WaitRpReady();
            CallExtensionHandler("RpLoaded");
        }

        private void Trajet_changed(Script data)
        {
            CurrentMap_changed(Brain.State.CurrentMap.Get());
        }

        #endregion

        #region Loader

        // Todo maybe add a extensions cache ?
        private void LoadAllExtensions()
        {
            Loaded = new List<KeyValuePair<ExtensionHandlerAttribute.PriorityEnum, LoadedExtension>>();
            Assembly asm = Assembly.GetAssembly(typeof(Extensions.Extension));
            foreach (Type t in asm.GetTypes())
            {
                if (t.IsSubclassOf(typeof(Extensions.Extension)) && t.Name != typeof(Extensions.Extension).Name)
                {
                    ExtensionHandlerAttribute priority = t.GetCustomAttribute<ExtensionHandlerAttribute>();
                    if (priority == null)
                        priority = new ExtensionHandlerAttribute(ExtensionHandlerAttribute.PriorityEnum.Normal);
                    Loaded.Add(new KeyValuePair<ExtensionHandlerAttribute.PriorityEnum, LoadedExtension>(priority.Priority, new LoadedExtension(t, Brain)));
                    Log("{0} Loaded !", t.Name);
                }
            }
            Loaded = (from item in Loaded orderby item.Key descending select item).ToList();
        }

        List<String> cancelRequest = new List<string>();

        public void CancelHandlerExecution(string name)
        {
            if (!cancelRequest.Contains(name))
                cancelRequest.Add(name);
        }

        private async void CallExtensionHandler(string method)
        {
            for (int i = (int)ExtensionHandlerAttribute.PriorityEnum.VeryHigh; i >= 0; i--)
            {
                foreach (KeyValuePair<ExtensionHandlerAttribute.PriorityEnum, LoadedExtension> ext in Loaded)
                {
                    if (ext.Value.Methods.ContainsKey(method) && (int)ext.Value.Methods[method].Priority == i)
                    {
                        Task r = (Task)ext.Value.Instance.GetType().GetMethod(method).Invoke(ext.Value.Instance, new object[] { });
                        await r;
                    }
                    if (cancelRequest.Contains(method))
                    {
                        Warn("CallExtensionHandler cancled by extension !");
                        cancelRequest.Remove(method);
                        return;
                    }
                }
            }
        }

        #endregion

    }

    #region Generic types

    public class LoadedExtension
    {
        public Extensions.Extension Instance { get; set; }
        public Dictionary<string, ExtensionHandlerAttribute> Methods { get; set; }

        public LoadedExtension(Type t, Brain brain)
        {
            Instance = (Extensions.Extension)Activator.CreateInstance(t, brain);
            Methods = new Dictionary<string, ExtensionHandlerAttribute>();
            foreach (MethodInfo method in t.GetMethods())
            {
                ExtensionHandlerAttribute attr = method.GetCustomAttribute<ExtensionHandlerAttribute>();
                if (attr != null)
                {
                    Methods.Add(method.Name, attr);
                }
            }
        }
    }

    public enum ContextualTrajetEvents
    {
        InventoryRefresh,
        CharactPointAvail,
    }

    public class BasicExtensionHost : IExtensionHost
    {

        Brain Brain;
        public BasicExtensionHost(string content, Brain brain)
        {
            Brain = brain;
            mContent = content;
        }

        private string mContent;
        public string GetContent()
        {
            return mContent;
        }

        private ExtensionDomain mScript = null;
        public ExtensionDomain ParseContent()
        {
            if (mContent == null || mContent == string.Empty)
                return null;
            mScript = new ExtensionDomain(ExtensionType.Trajet, Brain);
            mScript.Load(mContent);
            return mScript;
        }
    }

    public interface IExtensionHost
    {
        String GetContent();
        ExtensionDomain ParseContent();
    }

    #endregion

}
