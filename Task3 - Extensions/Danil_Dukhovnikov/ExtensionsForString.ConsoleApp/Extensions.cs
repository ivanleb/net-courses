using System;

namespace ExtensionsForString.ConsoleApp
{
    public static class Extensions
    {
        private static readonly Random Random = new Random();
        
        public static string SetFirstLetterToUpperCase(this string str)
        {
            return char.ToUpper(str[0]) + str.Substring(1);
        }

        public static string ApplyBraces(this string str)
        {
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
            return $"{str} - {Random.Next()}";
        }
        
        public static string IncludeCurrentYear(this string str)
        {
            var currentYear = DateTime.Now.Year;
            return $"{str}: {currentYear}";
        }
    }
}