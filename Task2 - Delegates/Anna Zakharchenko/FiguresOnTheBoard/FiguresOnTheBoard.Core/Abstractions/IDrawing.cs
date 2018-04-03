
namespace FiguresOnTheBoard.Core.Abstractions
{
    public interface IDrawing
    {
        void DrawBoard(IBoard board);
        void DrawDot(IBoard board);
        void DrawHorizontalLine(IBoard board);
        void DrawVerticalLine(IBoard board);
    }
}
