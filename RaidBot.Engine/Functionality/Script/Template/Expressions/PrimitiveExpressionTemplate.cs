using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidBot.Engine.Functionality.Script.Template.Expressions
{
    public class PrimitiveExpressionTemplate:IExpression
    {
        public string Value { get; set; }

        public PrimitiveExpressionTemplate(string value)
        {
            Value = value;
        }
    }
}
