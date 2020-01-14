using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidBot.Engine.Functionality.Script.Parsing
{
    public class CharDefinitions
    {
        public const char StartBlock = '{';
        public const char EndBlock = '}';
        public const char StartExpression = '(';
        public const char EndExpression = ')';
        public const string IfKeyword = "if";
        public const string WhileKeyWord = "while";
        public const string ClassKeyWord = "class";
        public const string MouvementKeyWord = "mouvements";
        public const string VariableWord = "var";
        public const char VariableExpression = '$';
        public const string EventKeyword = "event";
        public const char EndDirective = ';';
        public const char NextExpression = ',';
        public const char EqualsOperator = '=';
        public const char StringDeclarator = '"';
    }
}
