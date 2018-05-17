using EventsGame.Core;
using EventsGame.ConsoleApp.Implementations;

namespace EventsGame.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            new GameLogic().Run(new Registry());
        }
    }
}
