using RaidBot.Engine.Functionality.Script.Template.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidBot.Engine.Functionality.Script.Machine.Link
{
    public class ExecutRequestEventArgs:EventArgs
    {
        public List<List<IExpression>> Expressions { get; private set; }
        public List<Variable> Arguments { get; private set; }
        
        public ExecutRequestEventArgs(List<List<IExpression>> expressions ,List<Variable> arguments)
        {
            Expressions = expressions;
            Arguments = arguments;
        }
    }
}
