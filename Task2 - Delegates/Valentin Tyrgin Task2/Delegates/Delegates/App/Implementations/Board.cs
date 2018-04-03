using System;
using Delegates.Core.Abstractions;

namespace Delegates.App.Implementations
{
    internal class Board : IBoard
    {
        private int height;
        private int width;
        public string Name { get; set; } = "Доска";

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

        //public virtual string GetName()
        //{
        //    return Name;
        //}

        public void SetSize()
        {
            IUserAction temp = new UserIntaraction();
            Height = temp.GetInt("Укажите высоту доски");
            Width = temp.GetInt("Укажите ширину доски");
            Console.Clear();
        }
        
        public virtual void Draw(IBoard board)
        {
            Draw(new[] {0,0}, new[] {board.Width, board.Height});
        }

        public void Draw(int[] firstCorner, int[] secondCorner)
        {
            var point = new Point();
            var hLine = new HorizontalLine();
            var vLine = new VerticalLine();
            foreach (var x in new[] { firstCorner[0], secondCorner[0] })
            {
                foreach (var y in new[] { firstCorner[1], secondCorner[1] })
                {
                    point.Draw(x, y);
                }
            }
            hLine.Draw(firstCorner[0]+1, secondCorner[0], firstCorner[1]);
            hLine.Draw(firstCorner[0]+1, secondCorner[0], secondCorner[1]);
            vLine.Draw(firstCorner[1]+1, secondCorner[1], firstCorner[0]);
            vLine.Draw(firstCorner[1]+1, secondCorner[1], secondCorner[0]);
        }
    }
}