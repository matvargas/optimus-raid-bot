using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidBot.Engine.Functionality.Script.Machine.Link
{
    public class EventDeclenchedEventArgs:EventArgs
    {
        public List<Variable> Args { get; private set; }
        public EventDeclenchedEventArgs(List<Variable> args)
        {
            Args = args;
        }
    }
}
