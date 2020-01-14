using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.CodeDom;
using Microsoft.CSharp;
using RaidBot.Tools.CodeTraductor.Template;

namespace RaidBot.Tools.CodeTraductor.Generator
{
    public class EnumGenerator
    {
        private CodeCompileUnit classTranslated;
        private Template.Enum classToTranslat;

        public EnumGenerator(Template.Enum enumToGenerate, string folderPath)
        {
            classToTranslat = enumToGenerate;
            classTranslated = new CodeCompileUnit();
            Generate();
            GeneratorUtility.GenerateCode(new CSharpCodeProvider(), classTranslated, folderPath + "\\Enums\\" + classToTranslat.Name + ".cs");
        }

        private void Generate()
        {
            CodeNamespace newNameSpace = new CodeNamespace();
            newNameSpace.Name = "RaidBot.Protocol.Enums";
            CodeTypeDeclaration newEnum = new CodeTypeDeclaration();
            newEnum.Name = classToTranslat.Name;
            newEnum.IsEnum = true;
            newEnum.Members.AddRange(GenerateEnumStatements());

            newNameSpace.Types.Add(newEnum);
            classTranslated.Namespaces.Add(newNameSpace);
        }

        private  CodeTypeMember[] GenerateEnumStatements()
        {
            List<CodeTypeMember> retVal = new List<CodeTypeMember>();
            foreach (EnumItem item in classToTranslat.Items)
            {
                CodeMemberField newField = new CodeMemberField();
                newField.Name = item.Name;
                newField.InitExpression = new CodePrimitiveExpression(Convert.ToInt32(item.Value));
                retVal.Add(newField);
            }
            return retVal.ToArray();
        }
    }
}
