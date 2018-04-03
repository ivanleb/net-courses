using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            String sampleStr = "sample texts";
            var a = sampleStr
                .SetFirstLetterToUpperCase()
                .ApplyBraces()
                .ApplySpaces()
                .AppendNumbers()
                .IncludeCurrentYear()
                .AppendSmile();
            Console.WriteLine(a);
        }
    }
}
