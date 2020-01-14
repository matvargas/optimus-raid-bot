using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidBot.Engine.Functionality.Script.Enums
{
    public class  Operator
    {
        public const string EqualsOperator = "=";
        public const string NotEqualsOperator = "!=";
        public const string LargerTanOperator = ">";
        public const string SmallerTanOperator = "<";
        public const string LargerTanOrEqualsOperator = ">=";
        public const string SmallerTanOrEqualsOperator = "<=";
        public const char AndOperator ='&';
    }

    public enum OperatorEnum
    {
        Equals,
        NotEquals,
        LargerTan,
        SmallerTan,
        LargerTanOrEquals,
        SmallerTanOrEquals,
        And,
        Or,
    }
}
