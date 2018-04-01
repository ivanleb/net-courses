using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExtensionsForString;

namespace ExtensionMethodsString
{
    class Program
    {
        static void Main(string[] args)
        {
            string a = "hello world";
            a = a.SetFirstLetterToUpperCase().ApplyBraces().ApplySpaces().AppendNumbers().IncludeCurrentYear().AppendSmile();
            Console.WriteLine(a);
            Console.ReadLine();
        }
    }
}
