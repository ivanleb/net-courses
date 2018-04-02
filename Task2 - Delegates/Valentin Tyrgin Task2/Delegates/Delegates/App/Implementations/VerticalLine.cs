using Delegates.Core.Abstractions;

namespace Delegates.App.Implementations
{
    internal class VerticalLine : Board
    {
        //public string Name { get; } = "Вертикальная линия";
        private const string symbol = "|";
        Utils utils = new Utils();

        public VerticalLine()
        {
            Name = "Вертикальная линия";
        }
        public override void Draw(IBoard board)
        {
            Draw(1,board.Height,board.Width/2);
        }
        public void Draw(int yStart, int yEnd, int x)
        {
            if (yStart > yEnd)
            {
                var t = yStart;
                yStart = yEnd;
                yEnd = t;
            }
            for (var i = yStart; i < yEnd; i++)
            {
                utils.WriteAt(symbol, x, i);
            }
        }

        public override string GetName()
        {
            return Name;
        }
    }
}