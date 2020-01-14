using RaidBot.Engine.Functionality.Script.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidBot.Engine.Functionality.Script.Template.Expressions
{
    public class ConditionExpression:IExpression
    {
        public IExpression LeftExpression { get; set; }
        public IExpression RightExpression { get; set; }
        public OperatorEnum Operation { get; set; }

        public ConditionExpression(IExpression leftExpression, IExpression rightExpression, OperatorEnum operation)
        {
            LeftExpression = leftExpression;
            RightExpression = rightExpression;
            Operation = operation;
        }
    }
}
