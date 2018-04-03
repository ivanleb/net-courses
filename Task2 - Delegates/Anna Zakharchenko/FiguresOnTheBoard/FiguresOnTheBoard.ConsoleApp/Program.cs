using FiguresOnTheBoard.ConsoleApp.Implementations;
using FiguresOnTheBoard.Core;

namespace FiguresOnTheBoard.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            new Logic().Run(new ConsoleAppRegistry());
        }
    }
}
