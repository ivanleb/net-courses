using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extensions
{
    public static class Program
    {
        const String initalLine= "simple text";

        static void Main(string[] args)
        {
            var a = initalLine;
            var result = a.SetFirstLetterToUpperCase()
                .ApplyBraces()
                .ApplySpaces()
                .AppendNumbers(023123)
                .IncludeCurrentYear()
                .AppendSmile();

            Console.WriteLine(result);
            Console.ReadKey();
                
        }

        public static String SetFirstLetterToUpperCase(this String lineToChange)
        {
            char firstSumbol = Char.ToUpper(lineToChange.First());
            return lineToChange.Remove(0, 1).Insert(0, firstSumbol.ToString());
        }

        public static String ApplyBraces(this String lineToChange)
        {
            return "{" + lineToChange + "}";
        }

        public static String ApplySpaces(this String lineToChange)
        {
            return " " + lineToChange + " ";
        }

        public static String AppendNumbers(this String lineToChange, int num)
        {
            return lineToChange + " - " + num;
        }

        public static String IncludeCurrentYear(this String lineToChange)
        {
            return lineToChange + " : " + DateTime.Now.Year;
        }

        public static String AppendSmile(this String lineToChange)
        {
            return " :) " + lineToChange ;
        }
    }
}
