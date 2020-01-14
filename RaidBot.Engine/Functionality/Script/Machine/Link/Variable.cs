using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidBot.Engine.Functionality.Script.Machine.Link
{
    public class Variable
    {
        
        public string Name { get; set; }
        public object Value { get; set; }
        public Variable()
        {
        }
        public Variable(string name, object value)
        {
            Name = name;
            Value = value;
        }
    }
}