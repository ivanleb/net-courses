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
        public static String SetFirstLetterToUpperCase(this String modifyString)
        {
            char[] modifyArray = modifyString.ToCharArray();
            int firstLetterPosition = Array.FindIndex(modifyArray, character => Char.IsLetter(character));

            if (firstLetterPosition == -1)
            {
                return modifyString;
            }

            modifyArray[firstLetterPosition] -= (char)32; // ASCII
            return new string (modifyArray);
        }
        
        public static String ApplyBraces(this String modifyString)
        {
            return "{" + modifyString + "}";
        }
        
        public static String ApplySpaces(this String modifyString)
        {
            return " " + modifyString + " ";
        }

        public static String AppendNumbers(this String modifyString, int number)
        {
            return modifyString + " - " + number;
        }

        public static String IncludeCurrentYear(this String modifyString)
        {
            return modifyString + ": " + DateTime.Today.Year;
        }

        public static String AppendSmile(this String modifyString)
        {
            return ":)" + modifyString;
        }
    }
}
