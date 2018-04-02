using Delegates.Core;
using Delegates.ConsoleApp.Implementations;

namespace Delegates.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            new GameLogic().Run(new Registry());
        }
    }
}
