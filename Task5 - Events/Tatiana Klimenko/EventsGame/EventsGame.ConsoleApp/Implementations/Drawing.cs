using System;
using EventsGame.Core.Abstractions;

namespace EventsGame.ConsoleApp.Implementations
{
    class Drawing : IDrawing
    {
        private IBoard board;
        private IHero hero;

        public void SetupDrawing(IBoard board, IHero hero)
        {
            this.board = board;
            this.hero = hero;
        }

        public void StartListenInput(IUserInteraction input)
        {
            input.InputReceived += OnInputReceived;
        }

        private void OnInputReceived(object sender, IGameEventArgs eventArgs)
        {
            DrawAll();
        }

        public void DrawAll()
        {
            Console.Clear();

            DrawBoard();
            DrawHero();
            DrawRestriction();
        }        

        private void DrawBoard()
        {
            //corners
            WriteAt("+", 0, 0);
            WriteAt("+", 0, board.SizeY - 1);
            WriteAt("+", board.SizeX - 1, 0);
            WriteAt("+", board.SizeX - 1, board.SizeY - 1);
            //top line
            for (int x = 1; x < board.SizeX - 1; x++)
            {
                WriteAt("-", x, 0);
            }
            //bottom line
            for (int x = 1; x < board.SizeX - 1; x++)
            {
                WriteAt("-", x, board.SizeY - 1);
            }
            //left line
            for (int y = 1; y < board.SizeY - 1; y++)
            {
                WriteAt("|", 0, y);
            }
            //right line
            for (int y = 1; y < board.SizeY - 1; y++)
            {
                WriteAt("|", board.SizeX - 1, y);
            }
            Console.WriteLine();
        }

        private void DrawHero()
        {
            int currentRow = Console.CursorTop;
            WriteAt("+", hero.PosX, hero.PosY);
            Console.SetCursorPosition(0, currentRow);
        }

        private void DrawRestriction()
        {
            Console.ForegroundColor = ConsoleColor.Red;

            if (hero.PosY == 1)
            {
                for (int x = 1; x < board.SizeX - 1; x++)
                {
                    WriteAt("-", x, 0);
                }
            }
            if (hero.PosY == board.SizeY - 2)
            {
                for (int x = 1; x < board.SizeX - 1; x++)
                {
                    WriteAt("-", x, board.SizeY - 1);
                }
            }
            if (hero.PosX == 1)
            {
                for (int y = 1; y < board.SizeY - 1; y++)
                {
                    WriteAt("|", 0, y);
                }
            }
            if (hero.PosX == board.SizeX - 2)
            {
                for (int y = 1; y < board.SizeY - 1; y++)
                {
                    WriteAt("|", board.SizeX - 1, y);
                }
            }

            Console.ResetColor();
        }

        private static void WriteAt(string s, int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(s);
        }

    }
}
