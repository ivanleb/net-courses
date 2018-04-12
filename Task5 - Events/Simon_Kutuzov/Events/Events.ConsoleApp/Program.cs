using Events.Core;
using Events.Core.Abstractions;
using Events.ConsoleApp.Implementations;

namespace Events.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            StaticRegistry.board = new ConsoleBoard(50, 20);
            StaticRegistry.model = new ConsoleModel();
            StaticRegistry.input = new ConsoleUserInteraction();
            GameLogic.Run();
        }
    }
}
