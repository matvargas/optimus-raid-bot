using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidBot.Engine.Functionality.Script.Template.Expressions
{
    public class VariableAssignementExpression:IExpression
    {
        public VariableReferenceExpressionTemplate LeftExpression { get; set; }
        public IExpression RightExpression { get; set; }
        public VariableAssignementExpression(){}

        public VariableAssignementExpression(VariableReferenceExpressionTemplate left, IExpression right)
        {
            LeftExpression=left;
            RightExpression = right;
        }
    }
}
