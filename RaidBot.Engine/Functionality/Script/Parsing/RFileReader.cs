using RaidBot.Engine.Functionality.Script.Enums;
using RaidBot.Engine.Functionality.Script.Template;
using RaidBot.Engine.Functionality.Script.Template.Declarations;
using RaidBot.Engine.Functionality.Script.Template.Expressions;
using RaidBot.Engine.Functionality.Script.Template.Statements;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidBot.Engine.Functionality.Script.Parsing
{
    public class RFileReader
    {
        #region Declarations

        private string mFileStr;
        private StringReader mReader;
        private ClassTemplate mClass;

        #endregion

        #region Constructor

        public RFileReader()
        {

        }

        #endregion

        #region Public method

        public void SetFile(string fileStr)
        {
            mReader = new StringReader(fileStr);
        }

        public void ReadFile()
        {
            if (mReader != null)
                ReadCurrentFile();
            else
                throw new Exception("No class to translate");
        }

        public ClassTemplate GetReadedClass()
        {
            if (mClass != null)
                return mClass;
            else
                throw new Exception("Class is not parseed");
        }

        #endregion

        #region Privat method

        private void ReadCurrentFile()
        {
            mClass = new ClassTemplate();
            while (mReader.Peek() != -1)
            {
                char fristChar = (char)mReader.Peek();

                if (char.IsLetterOrDigit(fristChar))//si non si le character et une lettre on lit le mot et on voie ce qu'on en fait
                {
                    ReadKeyWord();
                }
                else
                {
                    mReader.Read();
                }
            }
        }


        #region Read script members
        private void ReadClass()
        {
            StringBuilder className = new StringBuilder();
            char currentChar = (char)mReader.Peek();

            if (!char.IsLetter(currentChar))//si le character n'est pas une lettre on fait une boucle j'usque a trouver une lettre et on lit le nom
            {
                while (!char.IsLetter(currentChar))
                {
                    mReader.Read();
                    currentChar = (char)mReader.Peek();
                }
            }

            bool continu = true;
            while (continu)
            {
                if (char.IsLetterOrDigit(currentChar))
                {
                    className.Append(currentChar);
                }
                else
                {
                    continu = false;
                }
                mReader.Read();
                currentChar = (char)mReader.Peek();
            }

            mClass.Name = className.ToString();
            mReader = ReadBlock(mReader);//on lit le block de la class
        }
        private void ReadKeyWord()
        {
            StringBuilder keyword = new StringBuilder();
            char currentChar = (char)mReader.Peek();

            while (!char.IsWhiteSpace(currentChar) && currentChar != CharDefinitions.StartExpression)//on lit le mot clef jusque a ce que on tombe sur un espace 
            {
                keyword.Append(currentChar);
                mReader.Read();
                currentChar = (char)mReader.Peek();
            }

            UseKeyword(keyword.ToString());//on voie ce qu'on fait de ce mot 
        }
        private void UseKeyword(string keyword)
        {
            switch (keyword.ToLower())
            {
                case CharDefinitions.ClassKeyWord:
                    ReadClass();
                    break;
                case CharDefinitions.VariableWord:
                    ReadVariableDeclaration();
                    break;
                case CharDefinitions.EventKeyword:
                    ReadEventIteration();
                    break;
                case CharDefinitions.MouvementKeyWord:
                    ReadMouvementIteration();
                    break;
            }
        }

        #region Mouvement
        private void ReadMouvementIteration()
        {
            MouvementTemplate temp = new MouvementTemplate();
            temp.Conditions = ReadConditions(mReader);
            StringReader block = ReadBlock(mReader);
            ValuePairExpression key;
            do
            {
                key = ReadValuePairExpression(block);
                temp.Expressions.Add(key.LeftExpression, key);
            } while (block.Peek() != -1);
            mClass.Statements.Add(temp);
        }

        private ValuePairExpression ReadValuePairExpression(StringReader block)
        {
            ReadSpaces(block);
            char currentChar = (char)block.Peek();

            StringBuilder left = new StringBuilder();
            while (currentChar != ',')//on lit l'expression j'usque a la virgule
            {
                block.Read();
                left.Append(currentChar);
                currentChar = (char)block.Peek();
            }

            block.Read();//on lit la virgule
            currentChar = (char)block.Peek();

            StringBuilder right = new StringBuilder();
            while (currentChar != ';')//on lit l'expression j'usque au point virgule
            {
                block.Read();
                right.Append(currentChar);
                currentChar = (char)block.Peek();
            }
            block.Read();
            currentChar = (char)block.Peek();
            while (char.IsWhiteSpace(currentChar))
            {
                block.Read();
                currentChar = (char)block.Peek();
            }
            return new ValuePairExpression(ReadArgValue(left.ToString()), ReadArgValue(right.ToString()));
        }

        #endregion

        #region Event

        private void ReadEventIteration()
        {
            ReadSpaces(mReader);
            char currentChar = (char)mReader.Peek();
            StringBuilder eventName = new StringBuilder();
            while (char.IsLetterOrDigit(currentChar))
            {
                eventName.Append(currentChar);
                mReader.Read();
                currentChar = (char)mReader.Peek();
            }
            mClass.Statements.Add(new EventTemplate(ReadBlockExpressions(ReadBlock(mReader)), eventName.ToString()));
        }

        #endregion

        #region Condition

        private List<IExpression> ReadConditions(StringReader block)
        {
            List<IExpression> retVal = new List<IExpression>();
            List<string> conditionsStrSplitted = new List<string>();
            StringReader condBlock = ReadArgsBlock(block);
            string[] argsSplited = condBlock.ReadToEnd().Split('&');
            for (int i = 0; i < argsSplited.Length; i++)
            {
                argsSplited[i] = argsSplited[i].Trim();
                retVal.Add(ReadConditionValue(new StringReader(argsSplited[i])));
            }
            return retVal;
        }

        private ConditionExpression ReadConditionValue(StringReader block)
        {
            char currentChar = (char)block.Peek();

            StringBuilder leftStr = new StringBuilder();
            StringBuilder rightStr = new StringBuilder();
            StringBuilder symbole = new StringBuilder();
            bool ReadLeft = true;
            while (block.Peek() != -1)
            {
                currentChar = (char)block.Read();

                if (char.IsLetterOrDigit(currentChar) | currentChar == '"' | currentChar == '$')
                {
                    if (ReadLeft)
                    {
                        leftStr.Append(currentChar);
                    }
                    else
                    {
                        rightStr.Append(currentChar);
                    }
                }
                else
                {
                    char cc = (char)block.Peek();
                    if (cc == '=' || cc == '!' || cc == '<' || cc == '>')
                    {
                        while ((char)block.Peek() == '=' || (char)block.Peek() == '!' || (char)block.Peek() == '<' || (char)block.Peek() == '>')
                        {
                            symbole.Append((char)block.Read());
                        }
                        ReadLeft = false;
                    }
                    else
                    {
                        block.Read();
                    }
                }
            }

            return new ConditionExpression(ReadArgValue(leftStr.ToString()), ReadArgValue(rightStr.ToString()), ReadOperation(symbole.ToString()));
        }

        private OperatorEnum ReadOperation(string str)
        {
            return OperatorEnum.Equals;
        }

        #endregion

        #region Expressions

        private IExpression ThreathExpressionKeyword(string keyword, StringReader block)
        {
            switch (keyword)
            {
                case CharDefinitions.IfKeyword:
                    return new IfExpression(ReadConditions(block), ReadBlockExpressions(ReadBlock(block)));
                case CharDefinitions.WhileKeyWord:
                    return new PrimitiveExpressionTemplate("");
                default:
                    return new MethodInvockExpressionTemplate(keyword, ReadExpressionArgs(ref block));
            }
        }
        private List<IExpression> ReadBlockExpressions(StringReader block)
        {
            List<IExpression> retVal = new List<IExpression>();
            StringBuilder currentKeyWord = new StringBuilder();
            char currentChar = (char)block.Peek();
            while (!char.IsLetterOrDigit(currentChar) && currentChar != CharDefinitions.VariableExpression)
            {
                block.Read();
                currentChar = (char)block.Peek();
            }
            while (block.Peek() != -1)
            {
                if (char.IsLetterOrDigit(currentChar))
                {
                    currentKeyWord.Append(currentChar);
                    block.Read();
                    currentChar = (char)block.Peek();
                }
                else if (currentKeyWord.ToString().Trim() != string.Empty)
                {
                    retVal.Add(ThreathExpressionKeyword(currentKeyWord.ToString(), block));
                    currentKeyWord = new StringBuilder();
                }
                else
                {
                    currentChar = (char)block.Read();
                }
            }
            return retVal;
        }
        private IExpression ReadVariableAssignations(StringReader block)
        {
            if ((char)block.Read() != CharDefinitions.VariableExpression)
                return null;
            StringBuilder left = new StringBuilder();
            char currentChar = (char)mReader.Peek();
            while (currentChar != '=')
            {
                left.Append(currentChar);
                currentChar = (char)mReader.Read();
            }
            StringBuilder right = new StringBuilder();
            while (currentChar != CharDefinitions.EndDirective)
            {
                currentChar = (char)mReader.Read();
                right.Append(currentChar);
            }
            return new VariableAssignementExpression(new VariableReferenceExpressionTemplate(left.ToString().Trim()), ReadArgValue(right.ToString().Trim()));
        }

        #endregion

        #region Variable

        private void ReadVariableDeclaration()
        {
            char currentChar = (char)mReader.Peek();
            VariableDeclarationTemplate variableDeclaration = new VariableDeclarationTemplate();
            variableDeclaration.Name = string.Empty;
            StringBuilder variableType = new StringBuilder();

            while (currentChar != CharDefinitions.EndDirective)
            {
                currentChar = (char)mReader.Read();
                if (currentChar == CharDefinitions.EqualsOperator)
                {
                    string valueStr = string.Empty;
                    bool stop = false;
                    while (!stop)
                    {
                        currentChar = (char)mReader.Read();
                        if (currentChar == CharDefinitions.VariableExpression)
                        {
                            stop = true;
                        }
                        if (char.IsDigit(currentChar))
                        {
                            stop = true;
                        }
                        if (currentChar == CharDefinitions.StringDeclarator)
                        {
                            stop = true;
                        }
                    }
                    while (currentChar != CharDefinitions.EndDirective)
                    {
                        valueStr += currentChar;
                        currentChar = (char)mReader.Peek();
                        mReader.Read();
                    }

                    variableDeclaration.DefaultValue = ReadArgValue(valueStr);
                }
                else if (char.IsLetter(currentChar))
                {
                    variableDeclaration.Name += currentChar;
                    variableType.Append(currentChar);
                }
            }

            mClass.Declarations.Add(variableDeclaration);
        }

        #endregion

        #endregion

        #endregion

        #region Utility method

        private List<IExpression> ReadExpressionArgs(ref StringReader block)
        {
            List<IExpression> args = new List<IExpression>();
            StringReader argsStr = ReadArgsBlock(block);
            string[] argStrSplited = argsStr.ReadToEnd().ToString().Split(CharDefinitions.NextExpression);
            if (argStrSplited.Length == 1)
                if (argStrSplited[0] == "")
                    return args;
            char currentChar = (char)block.Peek();
            for (int i = 0; i < argStrSplited.Length; i++)
                args.Add(ReadArgValue(argStrSplited[i]));
            return args;
        }

        private IExpression ReadArgValue(string arg)
        {
            arg = arg.Trim();
            StringReader reader = new StringReader(arg);
            char currentChar = (char)reader.Peek();
            reader.Read();
            IExpression retVal;

            if (currentChar == CharDefinitions.StringDeclarator)
            {
                string valueStr = string.Empty;
                currentChar = (char)mReader.Read();
                while (currentChar != CharDefinitions.StringDeclarator)
                {
                    valueStr += currentChar;
                    currentChar = (char)reader.Read();
                }
                retVal = new PrimitiveExpressionTemplate(valueStr);
            }
            else if (currentChar == CharDefinitions.VariableExpression)
            {
                retVal = new VariableReferenceExpressionTemplate(arg.Remove(0, 1));
            }
            else if (char.IsDigit(currentChar))
            {
                retVal = new PrimitiveExpressionTemplate(arg);
            }
            else
            {
                retVal = new PrimitiveExpressionTemplate(arg);
            }

            return retVal;
        }

        private StringReader ReadBlock(StringReader currentBlock)
        {
            char currentChar = (char)currentBlock.Peek();

            while (currentChar != CharDefinitions.StartBlock)
            {
                currentBlock.Read();
                currentChar = (char)currentBlock.Peek();
            }

            bool stop = false;
            int openBarakCount = 0;
            StringBuilder str = new StringBuilder();

            while (!stop)
            {
                if (currentChar == CharDefinitions.StartBlock)
                {
                    openBarakCount += 1;
                }
                else if (currentChar == CharDefinitions.EndBlock)
                {
                    openBarakCount -= 1;
                }

                if (currentChar != CharDefinitions.EndBlock || currentChar != CharDefinitions.StartBlock)
                {
                    str.Append(currentChar);
                }

                if (openBarakCount <= 0)
                    stop = true;

                currentBlock.Read();
                currentChar = (char)currentBlock.Peek();
            }
            str = str.Remove(0, 1);
            str = str.Remove(str.Length - 1, 1);
            return new StringReader(str.ToString());
        }

        private StringReader ReadArgsBlock(StringReader block)
        {
            char currentChar = (char)block.Read();
            StringBuilder currentStr = new StringBuilder();
            while (currentChar != CharDefinitions.StartExpression)
            {
                currentChar = (char)block.Read();
            }
            currentChar = (char)block.Read();
            while (currentChar != CharDefinitions.EndExpression)
            {
                currentStr.Append(currentChar);
                currentChar = (char)block.Read();
            }
            block.Read();
            return new StringReader(currentStr.ToString().Trim());
        }

        private void ReadSpaces(StringReader block)
        {
            char currentChar = (char)block.Peek();
            while (char.IsWhiteSpace(currentChar))
            {
                block.Read();
                currentChar = (char)block.Peek();
            }
        }

        #endregion
    }
}