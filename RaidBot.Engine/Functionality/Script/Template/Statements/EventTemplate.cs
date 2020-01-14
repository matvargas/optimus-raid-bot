using RaidBot.Engine.Functionality.Script.Template.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidBot.Engine.Functionality.Script.Template.Statements
{
    public class EventTemplate : IStatement
    {
        public List<IExpression> Expressions { get; set; }
        public string Name { get; set; }

        public EventTemplate()
        {
            Expressions = new List<IExpression>();
        }

        public EventTemplate(List<IExpression> expression,string name)
        {
            Expressions = expression;
            Name = name;
        }
    }
}
