using MoonSharp.Interpreter;
using RaidBot.Engine.Bot.Extension.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidBot.Engine.Bot.Extension
{
    public enum ExtensionType
    {
        Trajet,
        SpellPredicate,
        FightIA,
    }

    public class ExtensionDomain
    {
        private Brain brain;
        public ExtensionType Extension { get; set; }
        public Script Script { get; set; }
        public String ScriptStr { get; set; }

        static bool isInit = false;
        static void init()
        {
            if (isInit)
                return;
            isInit = true;
            UserData.RegisterAssembly();
        }

        public ExtensionDomain(ExtensionType type, Brain brain)
        {
            init();
            Script = new Script();
            Extension = type;
            this.brain = brain;
            if (type == ExtensionType.Trajet)
            {
                Script.Globals["rp"] = new RolePlayExtensionContext(brain);
            }
            else if (type == ExtensionType.Trajet || type == ExtensionType.SpellPredicate)
            {
                Script.Globals["fight"] = new FightExtensionContext(brain);
            }
            Script.Globals["shared"] = new SharedExtensionsContext(brain);
        }

        public void Load(string script)
        {
            ScriptStr = script;
            Script.DoString(script);
        }
    }
}
