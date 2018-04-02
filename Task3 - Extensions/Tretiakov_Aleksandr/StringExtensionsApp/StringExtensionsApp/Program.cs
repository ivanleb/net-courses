using System;

namespace ExtensionMethodApp
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Input your sample:");
                var sample = Console.ReadLine();
                Console.WriteLine($"sample: {sample}");
                var result = sample
                    .SetFirstLetterToUpperCase()
                    .ApplyBraces()
                    .ApplySpaces()
                    .AppendNumbers()
                    .IncludeCurrentYear()
                    .AppendSmile();
                Console.WriteLine($"result: {result}");
                Console.ReadLine();
            } 
        }
    }
}
