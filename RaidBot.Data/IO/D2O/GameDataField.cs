using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RaidBot.Common.IO;

namespace RaidBot.Data.IO.D2O
{
    public class GameDataField
    {
        #region Declarations

        public object Value { get; private set; }
        public Type Type { get; private set; }
        public string Name { get; private set; }
        private Func<string, BigEndianReader, uint, object> readMethod;
        private List<string> Classes = new List<string>();
        private List<Func<string, BigEndianReader, uint, object>> listMethod = new List<Func<string,BigEndianReader,uint,object>>();
        private List<int> typeId = new List<int>();
        private Dictionary<int, GameDataClassDefiniton> classes;
        static int nullId = -1431655766;

        #endregion

        #region Metode

        #region public

        public GameDataField(string name, BigEndianReader reader)
        {
            Name = name;
            Initiate(reader);
        }

        public void SetClasses(Dictionary<int, GameDataClassDefiniton> Classes)
        {  
            classes = Classes;
        }

        public void Read(BigEndianReader reader, string className)
        {
            Value = readMethod(className, reader, 0);
        }

        #endregion

        #region Privée

        private object ReadInt(string className, BigEndianReader reader, uint dimention)
        {
            return reader.ReadInt();
        }

        private object ReadBoolean(string className, BigEndianReader reader, uint dimention)
        {
            return reader.ReadBoolean();
        }

        private object ReadUTF(string className, BigEndianReader reader, uint dimention)
        {
            return reader.ReadUTF();
        }

        private object ReadDouble(string className, BigEndianReader reader, uint dimention)
        {
            return reader.ReadDouble();
        }

        private object ReadI18nIndex(string className, BigEndianReader reader, uint dimention)
        {
            return reader.ReadInt();
        }

        private object ReadUint(string className, BigEndianReader reader, uint dimention)
        {
            return reader.ReadUInt();
        }

        private List<object> ReadVector(string className, BigEndianReader reader, uint dimention)
        {
            int listCount = reader.ReadInt();
            List<object> list = new List<object>();
            for (int i = 0; i < listCount; i++)
            {
                list.Add(listMethod[(int)(dimention)](className, reader, dimention + 1));
            }
            return list;
        }

        private object ReadObject(string className, BigEndianReader stream, uint dimention)
        {
            int classId = stream.ReadInt();
            if (classId == nullId)
            {
                return null;
            }
            return classes[classId].Read(stream);
        }

        private void Initiate(BigEndianReader stream)
        {
            int readType = stream.ReadInt();
            readMethod = GetMethod(readType, stream);
            Type = GetType(readType);
        }

        private Type GetType(int TypeId)
        {
            switch (TypeId)
            {
                case -1:
                    return typeof(int);
                case -2:
                    return typeof(bool);
                case -3:
                    return typeof(string);
                case -4:
                    return typeof(double);
                case -5:
                    return typeof(int);
                case -6:
                    return typeof(uint);
                case -99:
                    return GetList();
                default :
                    return typeof(object);
            }
        }

        private Type GetList()
        {
            switch (typeId[0])
            {
                case -1:
                    return typeof(List<int>);
                case -2:
                    return typeof(List<bool>);
                case -3:
                    return typeof(List<string>);
                case -4:
                    return typeof(List<double>);
                case -5:
                    return typeof(List<int>);
                case -6:
                    return typeof(List<uint>);
                case -99:
                    switch (typeId[1])
                    {
                        case -1:
                            return typeof(List<List<int>>);
                        case -2:
                            return typeof(List<List<bool>>);
                        case -3:
                            return typeof(List<List<string>>);
                        case -4:
                            return typeof(List<List<double>>);
                        case -5:
                            return typeof(List<List<int>>);
                        case -6:
                            return typeof(List<List<uint>>);
                        case -99:
                            return typeof(List<object>);
                        default:
                            return typeof(List<List<object>>);
                    }
            }
            return typeof(List<object>);
        }

        private Func<string, BigEndianReader, uint, object> GetMethod(int method, BigEndianReader reader)
        {
            switch (method)
            {
                case -1:
                    return this.ReadInt;
                case -2:
                    return this.ReadBoolean;
                case -3:
                    return this.ReadUTF;
                case -4:
                    return this.ReadDouble;
                case -5:
                    return this.ReadI18nIndex;
                case -6:
                    return this.ReadUint;
                case -99:
                    Classes.Add(reader.ReadUTF());
                    typeId.Add(reader.ReadInt());
                    listMethod.Insert(0,GetMethod(typeId.Last(), reader));
                    return this.ReadVector;
                default:
                    if (method > 0)
                        return this.ReadObject;
                    throw new Exception("unknow type");
            }
        }

        #endregion

        #endregion
    }
}
