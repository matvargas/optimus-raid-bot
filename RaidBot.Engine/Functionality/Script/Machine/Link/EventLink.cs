using RaidBot.Engine.Functionality.Script.Template.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidBot.Engine.Functionality.Script.Machine.Link
{
    public class EventLink:ILink
    {
        public string Name{get;private set;}
        public event EventHandler<ExecutRequestEventArgs> ExecutRequest;
        public List<List<IExpression>> Expressions { get;  set; }
        
        public EventLink(string name)
        {
            Name = name;
            Expressions = new List<List<IExpression>>();
        }

        public void OnDeclenched(object sender,EventDeclenchedEventArgs e)
        {
            OnExecutRequest(new ExecutRequestEventArgs(Expressions, e.Args));
        }
        private void OnExecutRequest(ExecutRequestEventArgs e)
        {
            ExecutRequest(this, e);
        }
    }
}
