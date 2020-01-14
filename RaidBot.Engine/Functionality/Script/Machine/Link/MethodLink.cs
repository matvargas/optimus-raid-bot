using RaidBot.Engine.Functionality.Script.Template.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RaidBot.Engine.Functionality.Script.Machine.Link
{
    public class MethodLink:ILink
    {
        public string Name { get; private set; }

        private Action<object[]> Method;

        public MethodLink(string name,Action<object[]> method)
        {
            Method = method;
            Name = name;
        }
        public void Invock(object[] args)
        {
            Method.Invoke(args);
        }
  
    }
}
