using DoorsAndLevels.ConsoleApp.Implementations;
using DoorsAndLevels.Core;

namespace DoorsAndLevels.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            new GameLogic().Run(new ConsoleAppRegistry());
        }
    }
}
