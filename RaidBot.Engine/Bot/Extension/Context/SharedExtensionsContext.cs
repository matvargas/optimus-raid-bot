using MoonSharp.Interpreter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidBot.Engine.Bot.Extension.Context
{
    [MoonSharpUserData]
    public class SharedExtensionsContext : ExtensionContext
    {
        Dictionary<DynValue, DynValue> values;
        public SharedExtensionsContext(Brain brain) : base(brain)
        {
            values = new Dictionary<DynValue, DynValue>();
        }

        public void Log(string msg)
        {
            Brain.Logger.Log(msg);
        }
        public void Log(string msg, params object[] obj)
        {
            if (obj == null)
                Brain.Logger.Log(msg);
            else
                Brain.Logger.Log(String.Format(msg, obj));
        }

        public DynValue this[DynValue idx]
        {
            get { return values[idx]; }
            set { values[idx] = value; }
        }
    }
}
