using Events.Console.Implementations;
using Events.Core.Abstractions;
using Task5_Events;
using Task5_Events.Implementations;

namespace Events.Console
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            StaticRegistry.Board = new ConsoleBoard(40, 15);
            StaticRegistry.UserInteraction = new ConsoleUserInteraction();
            StaticRegistry.Model = new ConsoleModel();
            
            AppLogic.Run();
        }
    }
}