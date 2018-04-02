using System;

namespace Delegates
{
    internal class Board : IBoard
    {
        private int height;
        private int width;
        public string Name { get; } = "Доска";

        public int Height
        {
            get { return height; }
            set { height = value > 0 ? value : -value; }
        }

        public int Width
        {
            get { return width; }
            set { width = value > 0 ? value : -value; }
        }

        public virtual string GetName()
        {
            return Name;
        }

        public void SetSize()
        {
            IUserAction temp = new UserIntaraction();
            Height = temp.GetInt("Укажите высоту доски");
            Width = temp.GetInt("Укажите ширину доски");
            Console.Clear();
        }

        public int GetHeight()
        {
            return Height;
        }

        public int GetWidth()
        {
            return Width;
        }

        public virtual void Draw(IBoard board)
        {
            var utils = new Utils();
            utils.WriteAt("+", 0, 0);
            utils.WriteAt("+", board.GetWidth(), board.GetHeight());
            utils.WriteAt("+", board.GetWidth(), 0);
            utils.WriteAt("+", 0, board.GetHeight());

            for (var i = 1; i < board.GetWidth(); i++)
            {
                utils.WriteAt("-", i, 0);
                utils.WriteAt("-", i, board.GetHeight());
            }
            for (var i = 1; i < board.GetHeight(); i++)
            {
                utils.WriteAt("|", 0, i);
                utils.WriteAt("|", board.GetWidth(), i);
            }
        }

        public void ClearBoard(IBoard board)
        {
            Console.Clear();
            Draw(board);
        }
    }
}