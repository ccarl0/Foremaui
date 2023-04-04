using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foremaui.Helpers
{
    public class FormatHelper
    {
        public static string Capitalize(string stringToCapitalize)
        {
            string str = stringToCapitalize;
            if (str.Length == 0)
                str = " ";
            else if (str.Length == 1)
                str = char.ToUpper(str[0]).ToString();
            else
                str = char.ToUpper(str[0]) + str.Substring(1);

            return str;
        }
    }
}
