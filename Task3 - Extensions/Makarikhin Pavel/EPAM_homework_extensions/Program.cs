using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPAM_homework_extensions
{
    class Program
    {
        static void Main(string[] args)
        {
            var a = "simple text";

            var result = a.SetFirstLetterToUpperCase().AppendNumber(248).AppendSmile().ApplyBraces().IncludeCurrentTime();

            Console.WriteLine(a);
            Console.WriteLine(result);

            Console.ReadKey();
        }
    }
}
