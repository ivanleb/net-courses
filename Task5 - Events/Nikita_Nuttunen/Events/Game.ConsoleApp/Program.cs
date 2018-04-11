using Game.ConsoleApp.Implementations;
using Game.Core;
using Game.Core.Abstractions;

namespace Game.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            new GameLogic().Run(new Registry());
        }
    }
}
