using System;

namespace ExtensionsForString.ConsoleApp
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            var a = "hello world";
            a = a.SetFirstLetterToUpperCase()
                .ApplyBraces()
                .ApplySpaces()
                .AppendNumbers()
                .IncludeCurrentYear()
                .AppendSmile();
            Console.WriteLine(a);
            Console.ReadLine();
        }
    }
}