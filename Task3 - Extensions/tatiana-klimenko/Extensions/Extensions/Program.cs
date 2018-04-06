using System;

namespace Extensions
{
    class Program
    {
        static void Main(string[] args)
        {
            var a = "simple text";

            var result = a
                .SetFirstLetterToUpperCase()
                .ApplyBraces()
                .ApplySpaces()
                .AppendNumbers(12345)
                .IncludeCurrentYear()
                .AppendSmile();

            Console.WriteLine(result);
        }
    }

    public static class StringExension
    {
        public static String SetFirstLetterToUpperCase(this String s)
        {
            char[] sArray = s.ToCharArray();

            if (sArray.Length == 0)
                return s;
            else
            {
                sArray[0] = Char.ToUpper(sArray[0]);
                return new string(sArray);
            }
        }

        public static String ApplyBraces(this String s)
        {
            return "{" + s + "}";
        }

        public  static String ApplySpaces(this String s)
        {
            return " " + s + " ";
        }

        public static String AppendNumbers(this String s, int numbers)
        {
            return s + " - " + numbers;
        }

        public static String IncludeCurrentYear(this String s)
        {
            return s + ": " + DateTime.Today.Year;
        }

        public static String AppendSmile(this String s)
        {
            return ":)" + s;
        }
    }
}
