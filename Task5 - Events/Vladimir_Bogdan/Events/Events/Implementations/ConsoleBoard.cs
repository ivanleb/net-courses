using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventsCore.Abstractions;

namespace Events.Implementations
{
    class ConsoleBoard : IBoard
    {
        private const ConsoleColor defaultColor = ConsoleColor.White;
        private ConsoleColor 
            leftColor = defaultColor, 
            rightColor = defaultColor, 
            topColor = defaultColor, 
            bottomColor = defaultColor;
        private int height;
        private int width;
        public int Height { get { return height; } set { height = value; } }
        public int Width { get { return width; } set { width = value; } }
        private IModel model;

        public ConsoleBoard(int width, int height)
        {
            this.width = width;
            this.height = height;
        }

        public void Draw(IModel model)
        {
            Console.Clear();
            DrawCanvas();
            DrawModel(model);
        }
        public void Initialize(IModel model)
        {
            this.model = model;
            foreach (var hero in model.Heroes)
            {
                hero.OnMove += Hero_OnMove;
            }
        }

        private void Hero_OnMove(IHero sender, MovingArgs args)
        {
            
            if (sender.PosX == 0)
            {
                leftColor = (sender as ColoredConsoleHero)?.Color?? defaultColor;
            }
            if (sender.PosX == this.Width)
            {
                rightColor = (sender as ColoredConsoleHero)?.Color ?? defaultColor;
            }
            if (sender.PosY == 0)
            {
                topColor = (sender as ColoredConsoleHero)?.Color ?? defaultColor;
            }
            if (sender.PosY == this.Height)
            {
                bottomColor = (sender as ColoredConsoleHero)?.Color ?? defaultColor;
            }
        }

        public void ListenToTheInput(IUserInput input)
        {
            input.OnInput += Input_OnInput;
        }

        private void Input_OnInput(IUserInput sender, InputEventArgs args)
        {
            Draw(this.model);
        }

        private void WriteAt(string s, int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(s);
        }
        private void DrawCanvas()
        {
            DrawBottomEdge();
            DrawLeftEdge();
            DrawRightEdge();
            DrawTopEdge();
        }
        private void DrawModel(IModel model)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            foreach (var mine in model.Mines)
            {
                WriteAt("*", mine.PosX, mine.PosY);
            }

            foreach (var hero in model.Heroes)
            {
                Console.ForegroundColor = (hero as ColoredConsoleHero)?.Color ?? defaultColor;
                WriteAt(hero.Mark.ToString(), hero.PosX, hero.PosY);
            }
            Console.ForegroundColor = defaultColor;
        }
        private void DrawTopEdge()
        {
            Console.ForegroundColor = this.topColor;
            for (int i = 1; i < this.Width; i++)
            {
                this.WriteAt("-", i, 0);
            }
            Console.ForegroundColor = defaultColor;
        }
        private void DrawBottomEdge()
        {
            Console.ForegroundColor = this.bottomColor;
            for (int i = 1; i < this.Width; i++)
            {
                this.WriteAt("-", i, this.height);
            }
            Console.ForegroundColor = defaultColor;
        }
        private void DrawLeftEdge()
        {
            Console.ForegroundColor = this.leftColor;
            for (int i = 1; i < this.Height; i++)
            {
                this.WriteAt("|", 0, i);
            }
            Console.ForegroundColor = defaultColor;
        }
        private void DrawRightEdge()
        {
            Console.ForegroundColor = this.rightColor;
            for (int i = 1; i < this.Height; i++)
            {
                this.WriteAt("|", this.width, i);
            }
            Console.ForegroundColor = defaultColor;
        }
    }
}
