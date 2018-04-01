using System;

namespace Extension
{
    public static class Extension
    {
        public static string SetFirstLetterToUpperSpace(this string msg)
        {
            var result = msg[0].ToString().ToUpper() + msg.Substring(1);
            Console.WriteLine(result);
            return result;
        }

        public static string ApplyBraces(this string msg)
        {
            var result = "{" + msg + "}";
            Console.WriteLine(result);
            return result;
        }

        public static string ApplySpaces(this string msg)
        {
            var result = string.Empty;
            foreach (var ch in msg)
            {
                if (ch == ' ') continue;
                result += ch + " ";
            }
            Console.WriteLine(result);
            return result;
        }

        public static string AppendNumbers(this string msg)
        {
            var result = msg + " - 12345";
            Console.WriteLine(result);
            return result;
        }

        public static string IncludeCurrentYear(this string msg)
        {
            var result = msg + ": " + DateTime.Now.Year;
            Console.WriteLine(result);
            return result;
        }

        public static string AppendSmile(this string msg)
        {
            var result = ":)" + msg;
            Console.WriteLine(result);
            return result;
        }
    }
}