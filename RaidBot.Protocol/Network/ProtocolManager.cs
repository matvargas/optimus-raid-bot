using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RaidBot.Common.IO;
using System.Reflection;
using RaidBot.Protocol.Messages;
using RaidBot.Protocol.Types;
using System.Threading;

namespace RaidBot.Protocol.Types
{
    public class ProtocolManager
    {
        #region Declarations

        private static Dictionary<uint, Type> types;
        private static Dictionary<uint, Type> messages;
        private static bool typesInitialized = false;
        private static bool messagesInitializd = false;

        #endregion

        #region Metode public

        private static Mutex mutex = new Mutex();

        public static T GetTypeInstance<T>(uint ProtocolId) where T : class
        {
            return getTypeInstance<T>(ProtocolId);
        }

        public static T GetTypeInstance<T>(int ProtocolId) where T : class
        {
            return getTypeInstance<T>((uint)ProtocolId);
        }

        public static NetworkMessage GetPacket(byte[] data, uint id)
        {
            mutex.WaitOne();
            if (!messagesInitializd)
                InitializeMessages();

            if (messages.ContainsKey(id))
            {
                NetworkMessage message = (NetworkMessage)Activator.CreateInstance(messages[id]);
                message.Data = data;
                mutex.ReleaseMutex();
                return message;
            }
            else
            {
                Console.WriteLine("Warning, protocolManager can't find packet " + id);
                mutex.ReleaseMutex();
                return null;
            }
        }

        public static NetworkMessage GetPacket(byte[] buffer)
        {
            BigEndianReader reader = new BigEndianReader(buffer);
            uint header = reader.ReadUShort();
            uint id = header >> 2;

            int length = 0;

            switch (header & 3)
            {
                case 1:
                    length = reader.ReadByte();
                    break;
                case 2:
                    length = reader.ReadUShort();
                    break;
                case 3:
                    length = (reader.ReadByte() << 16) + (reader.ReadByte() << 8) + reader.ReadByte();
                    break;
            }

            byte[] data = (length > 0) ? reader.ReadBytes(length) : new byte[] { };
            return GetPacket(data, (uint)id);
        }

        #endregion

        #region Metode privée

        private static T getTypeInstance<T>(uint ProtocolId) where T : class
        {
            mutex.WaitOne();
            if (!typesInitialized)
                InitializeTypes();

            if (types.ContainsKey(ProtocolId))
            {
                T ins = Activator.CreateInstance(types[ProtocolId]) as T;
                mutex.ReleaseMutex();
                return ins;
            }
            else
            {
                Console.WriteLine("Warning, protocolManager can't find type " + ProtocolId);
                mutex.ReleaseMutex();
                return null;
            }
        }


        private static void InitializeTypes()
        {
            typesInitialized = true;
            types = new Dictionary<uint, Type>();
            Assembly assembly = Assembly.GetAssembly(typeof(NetworkType));
            foreach (Type t in assembly.GetTypes())
            {
                if (t.IsSubclassOf(typeof(NetworkType)))
                {
                    FieldInfo f = t.GetField("Id");
                    if (f != null)
                    {
                        uint id = (uint)f.GetValue(t);
                        types.Add(id, t);
                    }
                }
            }
        }

        private static void InitializeMessages()
        {
            messagesInitializd = true;
            messages = new Dictionary<uint, Type>();
            Assembly assembly = Assembly.GetAssembly(typeof(NetworkMessage));
            foreach (Type t in assembly.GetTypes())
            {
                if (t.IsSubclassOf(typeof(NetworkMessage)))
                {
                    FieldInfo f = t.GetField("Id");
                    if (f != null)
                    {
                        uint id = (uint)f.GetValue(t);
                        messages.Add(id, t);
                    }
                }
            }
        }

        #endregion

    }
}
