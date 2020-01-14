using RaidBot.Engine.Functionality.Script.Template.Declarations;
using RaidBot.Engine.Functionality.Script.Template.Statements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidBot.Engine.Functionality.Script.Template
{
    public class ClassTemplate
    {
        public string Name { get; set; }
        public List<IDeclaration> Declarations { get; set; }
        public List<IStatement> Statements { get; set; }

        public ClassTemplate()
        {
            this.Declarations = new List<IDeclaration>();
            this.Statements = new List<IStatement>();
        }
    }
}
