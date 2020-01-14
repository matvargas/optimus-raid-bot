using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace RaidBot.Common.Config
{
    public class XmlUtility
    {
        private static string configDir = Environment.CurrentDirectory + @"\config";

        static XmlUtility()
        {
            if (!Directory.Exists(configDir))
                Directory.CreateDirectory(configDir);
        }
        public static bool XmlExiste(string name)
        {
            return File.Exists(string.Format("{0}\\{1}.xml", configDir, name));
        }

        public static XmlWriter CreateXml(string name)
        {
            return XmlWriter.Create(File.Create(string.Format("{0}\\{1}.xml", configDir, name)));
        }

        public static XmlDocument LoadXml(string name)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(File.Open(string.Format("{0}\\{1}.xml", configDir, name), FileMode.Open));
            return doc;
        }
        public static void SaveXml(XmlDocument document, string name)
        {
            document.Save(File.Create(string.Format("{0}\\{1}.xml", configDir, name)));
        }

        public static string GetXmlPath(string name)
        {
            return string.Format("{0}\\{1}.xml",configDir,name);
        }
    }
}
