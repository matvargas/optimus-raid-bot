using RaidBot.Engine.Functionality.Script.Template.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidBot.Engine.Functionality.Script.Machine.Link
{
     public class MouvementAddedEventArgs:EventArgs
    {
         public Dictionary<IExpression, ValuePairExpression> NewMouvements { get; private set; }

         public MouvementAddedEventArgs(Dictionary<IExpression, ValuePairExpression> newMouvements)
         {
             NewMouvements = newMouvements;
         }
    }
}
