using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events
{
    class Board : IBoard
    {
        private ConsoleColor leftBorderColor;
        private ConsoleColor rightBorderColor;
        private ConsoleColor topBorderColor;
        private ConsoleColor downBorderColor;

        public int Width { get; set; }
        public int Height { get; set; }
        public object Reset { get; private set; }

        private void ResetBorderColors()
        {
            leftBorderColor = ConsoleColor.White;
            rightBorderColor = ConsoleColor.White;
            topBorderColor = ConsoleColor.White;
            downBorderColor = ConsoleColor.White;
        }

        private void WriteAt(string s, int x, int y)
        {
                Console.SetCursorPosition(x, y);
                Console.Write(s);
        }

        private void DrawProblems()
        {
            foreach (var prob in StaticRegistry.model.problem)
            {
                WriteAt("X", prob.Xpos, prob.Ypos);
            }
        }

        private void DrawMover()
        {
            var mover = StaticRegistry.model.Mover;
            WriteAt("@", mover.Xpos, mover.Ypos);
        }

        private void DrawShape()
        {
            Console.ForegroundColor = leftBorderColor;
            for (int i = 1; i < Height; i++)
            {
                WriteAt("|", 0, i);
            }

            Console.ForegroundColor = rightBorderColor;
            for (int i = 1; i < Height; i++)
            {
                WriteAt("|", Width, i);
            }

            Console.ForegroundColor = topBorderColor;
            for (int i = 1; i < Width; i++)
            {
                WriteAt("-", i, 0);
            }

            Console.ForegroundColor = downBorderColor;
            for (int i = 1; i < Width; i++)
            {
                WriteAt("-", i, Height);
            }

            Console.ForegroundColor = ConsoleColor.White;
        }

        private void OnInputReceived(object sender, EventArgs e)
        {
            if (StaticRegistry.model.Mover.Xpos == 1)
                leftBorderColor = ConsoleColor.Green;
            if (StaticRegistry.model.Mover.Xpos == Width - 1)
                rightBorderColor = ConsoleColor.Green;
            if (StaticRegistry.model.Mover.Ypos == 1)
                topBorderColor = ConsoleColor.Green;
            if (StaticRegistry.model.Mover.Ypos == Height - 1)
                downBorderColor = ConsoleColor.Green;

            Draw();
            ResetBorderColors();
        }

        public Board(int width, int height)
        {
            this.Width = width;
            this.Height = height;
            Console.CursorVisible = false;
            ResetBorderColors();
        }

        public void Draw()
        {
            Console.Clear();
            DrawShape();
            DrawProblems();
            DrawMover();
        }

        public void StartListenInput(IUserInput input)
        {
            input.InputReceived += OnInputReceived;
        }
    }
}
