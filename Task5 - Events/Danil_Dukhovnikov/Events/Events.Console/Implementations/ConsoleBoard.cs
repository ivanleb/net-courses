using System;
using Events.Core.Abstractions;

namespace Events.Console.Implementations
{
    public class ConsoleBoard : IBoard
    {
        private ConsoleColor LeftVertivalBorderColor;
        private ConsoleColor RightVertivalBorderColor;
        private ConsoleColor UpperHorizontalBorderColor;
        private ConsoleColor LowerHorizontalBorderColor;

        private void ResetBorderColors() => 
            LeftVertivalBorderColor = 
                RightVertivalBorderColor =
                    UpperHorizontalBorderColor = 
                        LowerHorizontalBorderColor = ConsoleColor.White;
        
        public int Width { get; set; }
        public int Height { get; set; }

        public ConsoleBoard(int width, int height)
        {
            this.Width = width;
            this.Height = height;
            System.Console.CursorVisible = false;
            ResetBorderColors();
        }

        private static void WriteAt(int x, int y, char s)
        {
            try
            {
                System.Console.SetCursorPosition(x, y);
                System.Console.Write(s);
            }
            catch (ArgumentOutOfRangeException e)
            {
                System.Console.Clear();
                System.Console.WriteLine(e.Message);
            }
        }

        private static void DrawMines()
        {
            foreach (var mine in StaticRegistry.Model.Mines)
            {
                WriteAt(mine.X, mine.Y, mine.Mark);
            }
        }

        private static void DrawHero()
        {
            var hero = StaticRegistry.Model.Hero;
            WriteAt(hero.X, hero.Y, hero.Marker);
        }

        private void DrawCanvas()
        {
            for (var i = 1; i < Height; i++)
            {
                WriteAt(0, i, '|');
                WriteAt(Width, i, '|');
            }
            
            for (var i = 1; i < Width; i++)
            {
                WriteAt(i, 0, '-');
                WriteAt(i, Height, '-');
            }
        }

        public void Draw()
        {
            System.Console.Clear();
            DrawCanvas();
            DrawMines();
            DrawHero();
        }

        public void StartListeningInput(IUserInteraction input)
        {
            input.InputReceived += OnInputReceived;
        }
        
        private void OnInputReceived(object sender, EventArgs e)
        {
            if (StaticRegistry.Model.Hero.X == 1)
                LeftVertivalBorderColor = ConsoleColor.Red;
            if (StaticRegistry.Model.Hero.X == Width - 1)
                RightVertivalBorderColor = ConsoleColor.Red;
            if (StaticRegistry.Model.Hero.Y == 1)
                UpperHorizontalBorderColor = ConsoleColor.Red;
            if (StaticRegistry.Model.Hero.Y == Height - 1)
                LowerHorizontalBorderColor = ConsoleColor.Red;
       
            Draw();
            ResetBorderColors();
        }

    }
}