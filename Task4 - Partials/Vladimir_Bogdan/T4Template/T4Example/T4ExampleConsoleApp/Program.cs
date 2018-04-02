using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T4ExampleConsoleApp.Implementations;

namespace T4ExampleConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var registry = new MyRegistry();
            var catalog = registry.catalogManager.CreateNewCatalog();
            registry.catalogManager.FillLiteratureContent(catalog);
            registry.catalogPrinter.PrintLiteratureContent(catalog);
            registry.catalogPrinter.PrintMusicContent(catalog);
            registry.catalogManager.FillMusicContent(catalog);
            registry.catalogPrinter.PrintMusicContent(catalog);
            Console.ReadKey();
        }
    }
}
