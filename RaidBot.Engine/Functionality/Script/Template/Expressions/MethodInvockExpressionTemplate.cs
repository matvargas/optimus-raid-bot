using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidBot.Engine.Functionality.Script.Template.Expressions
{
    public class MethodInvockExpressionTemplate:IExpression
    {
        public string Name { get; set; }
        public List<IExpression> Args { get; set; }

        public MethodInvockExpressionTemplate(string name, List<IExpression> args)
        {
            Name = name;
            Args = args;
        }
    }
}
