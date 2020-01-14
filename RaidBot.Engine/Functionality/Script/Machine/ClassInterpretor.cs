using RaidBot.Engine.Functionality.Script.Machine.Link;
using RaidBot.Engine.Functionality.Script.Template;
using RaidBot.Engine.Functionality.Script.Template.Declarations;
using RaidBot.Engine.Functionality.Script.Template.Expressions;
using RaidBot.Engine.Functionality.Script.Template.Statements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidBot.Engine.Functionality.Script.Machine
{
    public class ClassInterpretor
    {
        public List<ILink> Links { get;  set; }
        private ClassTemplate currentClass;
        public Memory Memory { get; private set; }
        public void SetClass(ClassTemplate @class)
        {
            currentClass = @class;
        }

        public ClassInterpretor()
        {
            Memory = new Memory();
            Links = new List<ILink>();
        }

        public void Execute()
        {
            foreach (IDeclaration variable in currentClass.Declarations)
            {
                if (variable is VariableDeclarationTemplate)
                {
                    VariableDeclarationTemplate cVar = (VariableDeclarationTemplate)variable;
                    Memory.Add(cVar.Name, new MemoryVar(cVar.Name, GetValue(cVar.DefaultValue)));
                }
            }
            RunMembers(currentClass.Statements);
        }

        public void Stop()
        {

        }

        public object GetValue(IExpression value)
        {
            if(value is PrimitiveExpressionTemplate)
            {
                return ((PrimitiveExpressionTemplate)value).Value;
            }
            else if(value is VariableReferenceExpressionTemplate)
            {
                if(Memory.ContainsKey(((VariableReferenceExpressionTemplate)value).Name))
                {
                    return Memory[((VariableReferenceExpressionTemplate)value).Name].Value;
                }
                else
                {
                    ShowError("Undefined variable for " + ((VariableReferenceExpressionTemplate)value).Name);
                }
            }
            return null;
        }

        private void RunMembers(List<IStatement> statements)
        {
            foreach(var statement in statements)
            {
                if(statement is EventTemplate)
                {
                    RunEvent((EventTemplate)statement);
                }
                if(statement is MouvementTemplate)
                {
                    RunMouvements((MouvementTemplate)statement);
                }
            }
        }

        private void RunMouvements(MouvementTemplate mouvements)
        {
            foreach(ILink link in Links)
            {
                if(link is MouvementsLink)
                {
                    ((MouvementsLink)link).AddMouvement(new MouvementAddedEventArgs(mouvements.Expressions));
                }
            }
        }

        private void RunEvent(EventTemplate statement)
        {
            foreach(ILink link in Links)
            {
                if (link is EventLink)
                {
                    if(((EventLink)link).Name==statement.Name)
                    {
                        ((EventLink)link).Expressions.Add(statement.Expressions);
                        ((EventLink)link).ExecutRequest += ClassInterpretor_ExecutRequest;
                        return;
                    }
                }
            }
        }

        void ClassInterpretor_ExecutRequest(object sender, ExecutRequestEventArgs e)
        {
            foreach(List<IExpression> expression in e.Expressions)
            {
                RunExpressions(expression, e.Arguments);
            }
        }

        private void RunExpressions(List<IExpression> expressions,List<Variable> blockConstantes)
        {
            foreach(IExpression expression in expressions)
            {
                if(expression is MethodInvockExpressionTemplate)
                    RunMethodInvockExpression((MethodInvockExpressionTemplate)expression, blockConstantes);
            }
        }

        private void RunMethodInvockExpression(MethodInvockExpressionTemplate expression,List<Variable> blockConstantes)
        {
            foreach (ILink link in Links)
            {
                if (link is MethodLink)
                {
                    MethodLink l = (MethodLink)link;
                    if (l.Name == expression.Name)
                    {
                        List<object> objs = new List<object>();
                        foreach (IExpression ex in expression.Args)
                        {
                            if (ex is PrimitiveExpressionTemplate)
                            {
                                objs.Add(((PrimitiveExpressionTemplate)ex).Value);
                                break;
                            }
                            if (ex is VariableReferenceExpressionTemplate)
                            {
                                object value = null;
                                string name = ((VariableReferenceExpressionTemplate)ex).Name;
                                if (Memory.ContainsKey(name))
                                    value = Memory[name].Value;
                                else if (blockConstantes.ToDictionary<Variable, string>(GetName).ContainsKey(name))
                                    value = blockConstantes.ToDictionary<Variable, string>(GetName)[name].Value;
                                objs.Add(value);
                                break;
                            }
                        }
                        l.Invock(objs.ToArray());
                        return;
                    }
                }
            }
            ShowError(string.Format("Unknow function for {0}", expression.Name));
        }

        private string GetName(Variable name)
        {
            return name.Name;
        }

        private void ShowError(string error)
        {
            Console.WriteLine(error);
        }
    }
}
