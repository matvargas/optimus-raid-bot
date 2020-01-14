using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidBot.Engine.Functionality.Script.Machine
{
    public class MemoryVar
    {
        public string Name { get; set; }
        public object Value { get; set; }                      
        
        public MemoryVar(string name,object value)
        {
            Name = name;
            Value = value;
        }                                                                                                         
    }
}
