using System;

namespace Extension
{
    internal class Program
    {
        private static void Main()
        {
            Console.WriteLine("simple text".SetFirstLetterToUpperSpace()
                .ApplyBraces()
                .ApplySpaces()
                .AppendNumbers()
                .IncludeCurrentYear()
                .AppendSmile());
            Console.ReadLine();
        }
    }
}