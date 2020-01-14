using RaidBot.Data.IO.D2O.DataCenter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using RaidBot.Protocol.DataCenter;

namespace RaidBot.Data.IO.D2O
{
    public class ProtocolManager
    {
		
        #region Declarations

        private static Dictionary<string, Type> DataClasses;
        private static bool DataClassesInitialized = false;

        #endregion

        #region Public methode

        public static IData GetDataClass(string name)
        {
            if (!DataClassesInitialized)
                Ini();
			if (DataClasses.Any(key => key.Key == name))
				return (IData)Activator.CreateInstance (DataClasses [name]);
			else
				return (IData)null;
        }

        #endregion

        #region Private methode

        private static void Ini()
        {
            DataClasses = new Dictionary<string, Type>();
            Assembly assembly = Assembly.GetAssembly(typeof(IData));
            foreach (Type t in assembly.GetTypes())
            {
         
                if (ImplementsInterface(t,typeof(IData)))
                {
                    string className = t.ToString();
                    string[] splited = className.Split('.');
                    className = splited[splited.Length-1];
                    DataClasses.Add(className, t);
                }
                else
                    continue;
            }
            DataClassesInitialized = true;
        }

        private static bool ImplementsInterface( Type type, Type ifaceType)
        {
            Type[] intf = type.GetInterfaces();
            for (int i = 0; i < intf.Length; i++)
            {
                if (intf[i] == ifaceType)
                {
                    return true;
                }
            }
            return false;
        }

        #endregion
    }
}
