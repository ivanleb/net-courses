using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventsCore;
using Events.Implementations;

namespace Events
{
    class Program
    {
        static void Main(string[] args)
        {
            Random rnd = new Random();
            Game game = new Game();
            ConsoleBoard board = new ConsoleBoard(40, 40);
            Model model = new Model(
                new ConsoleMine[] {
                    new ConsoleMine(rnd.Next(board.Width),rnd.Next(board.Height)),
                    new ConsoleMine(rnd.Next(board.Width),rnd.Next(board.Height)),
                    new ConsoleMine(rnd.Next(board.Width),rnd.Next(board.Height)),
                    new ConsoleMine(rnd.Next(board.Width),rnd.Next(board.Height)),
                    new ConsoleMine(rnd.Next(board.Width),rnd.Next(board.Height))
                },
                new ColoredConsoleHero[] {
                    new ColoredConsoleHero(board, rnd.Next(board.Width),rnd.Next(board.Height), ConsoleColor.Green, ConsoleKey.UpArrow, ConsoleKey.DownArrow, ConsoleKey.LeftArrow, ConsoleKey.RightArrow),
                    new ColoredConsoleHero(board, rnd.Next(board.Width),rnd.Next(board.Height), ConsoleColor.Red, ConsoleKey.W, ConsoleKey.S, ConsoleKey.A, ConsoleKey.D),
                    new ColoredConsoleHero(board, rnd.Next(board.Width),rnd.Next(board.Height), ConsoleColor.Blue, ConsoleKey.Home, ConsoleKey.End, ConsoleKey.Delete, ConsoleKey.PageDown)});
            ConsoleUserInput input = new ConsoleUserInput();
            game.Start(model, board, input);
        }
    }
}
