using Delegates.Core.Abstractions;

namespace Delegates.App.Implementations
{
    internal class HorizontalLine : Board
    {
        //public string Name { get; } = "Горизонтальная линия";
        private const string symbol = "-";
        Utils utils = new Utils();

        public HorizontalLine()
        {
            Name = "Горизонтальная линия";
        }
        public override void Draw(IBoard board)
        {
            Draw(1,board.Width,board.Height/2);
        }
        public void Draw(int xStart, int xEnd, int y)
        {
            if (xStart > xEnd)
            {
                var t = xStart;
                xStart = xEnd;
                xEnd = t;
            }
            for (var i = xStart; i < xEnd; i++)
            {
                utils.WriteAt(symbol, i, y);
            }
        }

        public override string GetName()
        {
            return Name;
        }
    }
}