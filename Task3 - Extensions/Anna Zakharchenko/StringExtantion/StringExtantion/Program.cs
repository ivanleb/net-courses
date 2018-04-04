using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Extensions
{
    class Program
    {
        static void Main(string[] args)
        {
            var a = "simple text";
            var result = a
                .SetFirstLetterToUpperCase()
                .ApplySpaces()
                .ApplyBraces()
                .AppendNumbers(12345)
                .IncludeCurrentYear()
                .AppendSmile();
            Console.WriteLine(result);
            Console.ReadKey();
        }
    }

    public static class StringExtensions
    {
        public static String SetFirstLetterToUpperCase(this String str)
        {
            char[] inCharacter = str.ToCharArray();
            inCharacter[0] = char.ToUpper(inCharacter[0]);
            return str;
        }

        public static String ApplyBraces(this String str)
        {
            return "{" + str + "}";
        }

        public static String ApplySpaces(this String str)
        {
            return " " + str + " ";
        }

        public static String AppendNumbers(this String str, int number)
        {
            return str + " - " + number;
        }

        public static String IncludeCurrentYear(this String str)
        {
            return str + ": " + DateTime.Today.Year;
        }

        public static String AppendSmile(this String str)
        {
            return ":)" + str;
        }
    }
}
