using System;
using System.Collections.Generic;
using Extensions;

namespace RuslanKudaybergenovTask3
{
    class Program
    {
        static void Main(string[] args)
        {
            var a = "simple text";
            var numbers = new List<int>(){1,3,4,5,6};

            var result = a
                .SetFirstLetterToUpperCase()
                .ApplyBraces("[","}")
                .ApplySpaces(3,5)
                .AppendNumbers(numbers)
                .IncludeCurrentYear()
                .AppendSmile();
            Console.ReadKey();
        }
    }
}
