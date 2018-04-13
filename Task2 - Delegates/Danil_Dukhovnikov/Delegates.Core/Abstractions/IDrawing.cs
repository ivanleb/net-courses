namespace Delegates.Core.Abstractions
{
    public interface IDrawing
    {
        void DrawPoint(IBoard board);
        void DrawHorizontalLine(IBoard board);
        void DrawVerticalLine(IBoard board);
        void DrawBoard(IBoard board);
    }
}