using BoardGame.ConsoleApp.Implementations;
using BoardGame.Core;
using BoardGame.Core.Models;

namespace BoardGame.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var messager = new ConsoleMessager();
            var processor = new ConsoleProcessUserInput(messager);
            var board = new Board { SizeX = 100, SizeY = 20 };
            var handler = ConsoleBoardHandler.GetInstance();
 
            new AppLogic(messager, handler, processor).Run(board);
        }
    }
}
