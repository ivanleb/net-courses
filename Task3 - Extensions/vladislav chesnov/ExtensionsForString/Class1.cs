using System;

namespace ExtensionsForString
{
    public static class Class1
    {
        public static string SetFirstLetterToUpperCase(this string str)
        {
            char[] tmp = str.ToCharArray();
            tmp[0] = char.ToUpper(tmp[0]);
            return string.Join("", tmp);
        }

        public static string ApplyBraces(this string str)
        {
            //return string.Format("{0}{1}{2}", "{", str, "}");
            return $"{{{str}}}";
        }

        public static string ApplySpaces(this string str)
        {
            return $"{{{string.Join(" ", str.Split('{', '}'))}}}";
        }

        public static string AppendSmile(this string str)
        {
            return $":){str}";
        }

        public static string AppendNumbers(this string str)
        {
            return $"{str} - 42";
        }

        public static string IncludeCurrentYear(this string str)
        {
            int currentYear = DateTime.Now.Year;
            return $"{str}: {currentYear}";
        }
    }
}
