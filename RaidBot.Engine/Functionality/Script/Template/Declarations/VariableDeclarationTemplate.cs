using RaidBot.Engine.Functionality.Script.Enums;
using RaidBot.Engine.Functionality.Script.Template.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidBot.Engine.Functionality.Script.Template.Declarations
{
    public struct VariableDeclarationTemplate:IDeclaration
    {
        public string Name { get; set; }
        public VariableTypeEnum Type { get; set; }
        public IExpression DefaultValue { get; set; }
    }
}
