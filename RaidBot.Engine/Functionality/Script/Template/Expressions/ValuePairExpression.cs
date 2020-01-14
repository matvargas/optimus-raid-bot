using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidBot.Engine.Functionality.Script.Template.Expressions
{
    public class ValuePairExpression:IExpression
    {
        public IExpression LeftExpression { get; set; }
        public IExpression RightExpression { get; set; }

        public ValuePairExpression(IExpression left, IExpression right)
        {
            LeftExpression = left;
            RightExpression = right;
        }

        public ValuePairExpression()
        {

        }
    }
}
