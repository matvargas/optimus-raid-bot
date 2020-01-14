using RaidBot.Engine.Functionality.Script.Template.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidBot.Engine.Functionality.Script.Template.Statements
{
    public class MouvementTemplate:IStatement
    {
        public Dictionary<IExpression, ValuePairExpression> Expressions { get;   set; }
        public List<IExpression> Conditions { get; set; }
        public MouvementTemplate(Dictionary<IExpression, ValuePairExpression> expressions,List<IExpression> conditions)
        {
            Expressions = expressions;
            Conditions = conditions; 
        }

        public MouvementTemplate()
        {
            Expressions = new Dictionary<IExpression, ValuePairExpression>();
            Conditions = new List<IExpression>();
        }
    }
}
