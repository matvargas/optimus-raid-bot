using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RaidBot.Tools.CodeTraductor.Enums;

namespace RaidBot.Tools.CodeTraductor.Template
{
    public struct Variable
    {
        public string Name { get; set; }
        public VarType TypeOfVar { get; set; }
        public ReadMethodeType MethodeType { get; set; }
        public Type @Type { get; set; }
        public string ObjectType { get; set; }
        public string ReadMethode { get; set; }
        public string WriteMethode { get; set; }
    }
}