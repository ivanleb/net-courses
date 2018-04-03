using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPAM_homework_extensions
{
    public static class StringExtensionMethods
    {
        public static string SetFirstLetterToUpperCase(this string str)
        {
            return char.ToUpper(str[0]) + (str.Substring(1));
        }

        public static string AppendSmile(this string str)
        {
            return ":)" + str;
        }

        public static string ApplyBraces(this string str)
        {
            return "{ " + str + " }";
        }

        public static string ApplySpaces(this string str)
        {
            return " " + str + " ";
        }
        public static string AppendNumber(this string str, int num)
        {
            return str + ": " + num;
        }

        public static string IncludeCurrentTime(this string str)
        {
            return str + " " + System.DateTime.Now.ToString();
        }

    }
}
