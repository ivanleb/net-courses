using System;

namespace Extensions
{
    class Program
    {
        static void Main(string[] args)
        {
            string s = "simple text";
            string result = s
                .SetFirstLetterToUpperCase()
                .ApplySpaces()
                .ApplyBraces()
                .AppendNumbers()
                .IncludeCurrentYear()
                .AppendSmile();

            Console.WriteLine(result);
        }
    }
}
