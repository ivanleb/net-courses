namespace Delegates.Core.Abstractions
{
    public interface IDrawing
    {
        void DrawVecticalLine(IBoard board);
        void DrawHorizontalLine(IBoard board);
        void DrawDot(IBoard board);
        void Reset(IBoard board);
        void SetInputLine();
        void ReturnCursor();
    }
}
