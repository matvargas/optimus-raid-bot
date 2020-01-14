using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidBot.Engine.Functionality.Script.Template.Expressions
{
    public class VariableReferenceExpressionTemplate:IExpression
    {
        public string Name { get; set; }

        public VariableReferenceExpressionTemplate(string name)
        {
            Name = name;
        }
    }
}
