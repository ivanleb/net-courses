using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extensions
{
    public static class MyExtensions
    {
        public static string ApplySpaces(this String str)
        {
            return " " + str + " ";
        }
        public static string ApplyBraces(this String str)
        {
            return "{" + str + "}";
        }
        public static string AppendSmile(this String str)
        {
            return ":)" + str;
        }
        public static string SetFirstLetterToUpperCase(this String s)
        {

            if (char.IsUpper(s[0]))
                return s;
            else
             return ((s.Substring(0, 1)).ToUpper() + s.Substring(1, s.Length - 1));
        }
        public static string AppendNumbers(this String s)
        {
            return s + " - 12345 ";
        }
        public static string IncludeCurrentYear(this String s)
        {
            DateTime localDate = DateTime.Now;
            return  s + localDate.Year.ToString();
        }
	


    }

    class Program
    {
        static void Main(string[] args)
        {

            var a = "simple text";
            var result = a .SetFirstLetterToUpperCase()
                .ApplyBraces()
                .ApplySpaces()
                .AppendNumbers()
                .IncludeCurrentYear()
                .AppendSmile();
   
            Console.WriteLine(result);
            Console.ReadKey();
        }
    }
}
