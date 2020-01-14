using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidBot.Engine.Functionality.Script.Template.Expressions
{
    public class IfExpression:IExpression
    {
        public List<IExpression> ConditionExpressions { get; set; }
        public List<IExpression> Expressions { get; set; }

        public IfExpression(List<IExpression> conditionExpressions, List<IExpression> expressions)
        {
            ConditionExpressions = conditionExpressions;
            Expressions = expressions;
        }

        public IfExpression()
        {

        }
    }
}
