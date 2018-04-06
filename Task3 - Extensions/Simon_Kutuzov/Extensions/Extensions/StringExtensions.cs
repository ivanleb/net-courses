using System;

namespace Extensions
{
    public static class StringExtensions
    {
        public static String SetFirstLetterToUpperCase(this String str)
        {
            if (String.IsNullOrEmpty(str))
            {
                return String.Empty;
            }

            char[] a = str.ToCharArray();
            a[0] = char.ToUpper(a[0]);
            return new String(a);
        }

        public static String ApplyBraces(this String str)
        {
            return '{' + str + '}';
        }

        public static String ApplySpaces(this String str)
        {
            return ' ' + str + ' ';
        }

        public static String AppendNumbers(this String str)
        {
            return str + " - 12345";
        }

        public static String IncludeCurrentYear(this String str)
        {
            return str + " : " + DateTime.Today.Year;
        }

        public static String AppendSmile(this String str)
        {
            return ":3 " + str;
        }
    }
}
