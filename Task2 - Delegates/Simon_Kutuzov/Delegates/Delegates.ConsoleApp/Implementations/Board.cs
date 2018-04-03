using Delegates.Core.Abstractions;

namespace Delegates.ConsoleApp.Implementations
{
    public class Board : IBoard
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public char VerticalLineChar { get; set; }
        public char HorizontalLineChar { get; set; }

        public Board(int width = 10, int height = 10)
        {
            this.Width = width;
            this.Height = height;
            this.VerticalLineChar = '|';
            this.HorizontalLineChar = '-';
        }
    }
}
