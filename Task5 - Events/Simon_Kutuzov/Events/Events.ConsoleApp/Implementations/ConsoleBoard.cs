using System;
using Events.Core.Abstractions;

namespace Events.ConsoleApp.Implementations
{
    public class ConsoleBoard : IBoard
    {
        private ConsoleColor leftVertivalBorderColor;
        private ConsoleColor rightVertivalBorderColor;
        private ConsoleColor upperHorizontalBorderColor;
        private ConsoleColor lowerHorizontalBorderColor;

        public int Width { get; set; }
        public int Height { get; set; }
        public object Reset { get; private set; }

        private void ResetBorderColors()
        {
            leftVertivalBorderColor = ConsoleColor.White;
            rightVertivalBorderColor = ConsoleColor.White;
            upperHorizontalBorderColor = ConsoleColor.White;
            lowerHorizontalBorderColor = ConsoleColor.White;
        }

        private void WriteAt(char s, int x, int y)
        {
            try
            {
                Console.SetCursorPosition(x, y);
                Console.Write(s);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.Clear();
                Console.WriteLine(e.Message);
            }
        }

        private void DrawMines()
        {
            foreach (var mine in StaticRegistry.model.Mines)
            {
                WriteAt(mine.Mark, mine.X, mine.Y);
            }
        }

        private void DrawHero()
        {
            var hero = StaticRegistry.model.Hero;
            WriteAt(hero.Marker, hero.X, hero.Y);
        }

        private void DrawCanvas()
        {
            Console.ForegroundColor = leftVertivalBorderColor;
            for (int i = 1; i < Height; i++)
            {
                WriteAt('|', 0, i);
            }

            Console.ForegroundColor = rightVertivalBorderColor;
            for (int i = 1; i < Height; i++)
            {
                WriteAt('|', Width, i);
            }

            Console.ForegroundColor = upperHorizontalBorderColor;
            for (int i = 1; i < Width; i++)
            {
                WriteAt('-', i, 0);
            }

            Console.ForegroundColor = lowerHorizontalBorderColor;
            for (int i = 1; i < Width; i++)
            {
                WriteAt('-', i, Height);
            }

            Console.ForegroundColor = ConsoleColor.White;
        }

        private void OnInputReceived(object sender, EventArgs e)
        {
            if (StaticRegistry.model.Hero.X == 1)
                leftVertivalBorderColor = ConsoleColor.Red;
            if (StaticRegistry.model.Hero.X == Width - 1)
                rightVertivalBorderColor = ConsoleColor.Red;
            if (StaticRegistry.model.Hero.Y == 1)
                upperHorizontalBorderColor = ConsoleColor.Red;
            if (StaticRegistry.model.Hero.Y == Height - 1)
                lowerHorizontalBorderColor = ConsoleColor.Red;
       
            Draw();
            ResetBorderColors();
        }

        public ConsoleBoard(int width, int height)
        {
            this.Width = width;
            this.Height = height;
            Console.CursorVisible = false;
            ResetBorderColors();
        }

        public void Draw()
        {
            Console.Clear();
            DrawCanvas();
            DrawMines();
            DrawHero();
        }

        public void StartListeningInput(IUserInteraction input)
        {
            input.InputReceived += OnInputReceived;
        }
    }
}
