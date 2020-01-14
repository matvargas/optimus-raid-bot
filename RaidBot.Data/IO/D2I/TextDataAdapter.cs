using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidBot.Data.IO.D2I
{
    public class TextDataAdapter
    {
        private static I18nFileAccessor mAcessor;

        static TextDataAdapter()
        {
            mAcessor = new I18nFileAccessor(DataSetting.Default.D2IFilePath);
        }

        public static string GetText(int index)
        {
            return mAcessor.GetText(index);
        }

        public static string GetText(string index)
        {
            return mAcessor.GetText(index);
        }
    }
}
