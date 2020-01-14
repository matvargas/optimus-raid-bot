﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using RaidBot.Tools.CodeTraductor.Template;
using RaidBot.Tools.CodeTraductor.Dictionary;

namespace RaidBot.Tools.CodeTraductor.Parsing
{
    public class EnumParser
    {
        private string classToTranslat;
        private RaidBot.Tools.CodeTraductor.Template.Enum classTranslated;

        public EnumParser(string str)
        {
            classToTranslat = str;
            Translat();
        }

        public RaidBot.Tools.CodeTraductor.Template.Enum GetEnum()
        {
            return classTranslated;
        }

        private void Translat()
        {
            classTranslated = new Template.Enum();
            ParseClassInformations();
        }

        private void ParseClassInformations()
        {
            Match m = RegularExpression.GetRegex(RegexEnum.Enum).Match(classToTranslat);
            if (!m.Success)
                throw new Exception("The class is not a valide as3 class");
            classTranslated.Name = m.Groups["name"].Value;
            m = RegularExpression.GetRegex(RegexEnum.EnumItem).Match(classToTranslat);
            List<EnumItem> newItems = new List<EnumItem>();
            while (m.Success)
            {
                EnumItem newItem = new EnumItem();
                newItem.Name = m.Groups["name"].Value;
                newItem.Value = m.Groups["value"].Value;
                newItems.Add(newItem);
                m = m.NextMatch();
            }
            classTranslated.Items = newItems.ToArray();
        }
    }
}
