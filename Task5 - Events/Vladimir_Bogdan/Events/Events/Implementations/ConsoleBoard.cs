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
        private int height;
        private int width;
        public int Height { get { return height; } set { height = value; } }
        public int Width { get { return width; } set { width = value; } }
        private IModel model;

        public void Draw(IModel model)
        {
            Console.Clear();
            DrawCanvas();
            DrawModel(model);
        }
        public void Initialize()
        {

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
            for (int i = 1; i < this.Width; i++)
            {
                this.WriteAt("-", i, 0);
            }

            for (int i = 1; i < this.Height; i++)
            {
                this.WriteAt("|", 0, i);
            }

            for (int i = 1; i < this.Width; i++)
            {
                this.WriteAt("-", i, this.Height);
            }

            for (int i = 1; i < this.Height; i++)
            {
                this.WriteAt("|", this.Width, i);
            }
        }
        private void DrawModel(IModel model)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            foreach (var mine in model.Mines)
            {
                WriteAt("*", mine.PosX, mine.PosY);
            }
            Console.ForegroundColor = ConsoleColor.White;
            foreach (var hero in model.Heroes)
            {
                WriteAt(hero.Mark, hero.PosX, hero.PosY);
            }
        }
    }
}
