using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public static class StringExtensions
    {
        public static String SetFirstLetterToUpperCase(this String str)
        {
            return new StringBuilder(str).Replace(str[0], char.ToUpper(str[0]), 0, 1).ToString();
        }

        public static String ApplyBraces(this String str)
        {
            return new StringBuilder(str).Insert(0, "{").Insert(str.Length+1 , "}").ToString();
        }
        public static String ApplySpaces(this String str)
        {
            return new StringBuilder(str).Insert(0, " ").Insert(str.Length + 1, " ").ToString();
        }
        public static String AppendNumbers(this String str)
        {
            return new StringBuilder(str).Append(" - 12345").ToString(); //  - 12345
        }
        public static String IncludeCurrentYear(this String str)
        {
            return new StringBuilder(str).Append(" :" +DateTime.Now.Year).ToString();
        }
        public static String AppendSmile(this String str)
        {
            return new StringBuilder(str).Insert(0,")").Insert(0,':').ToString();
        }

    }
}
