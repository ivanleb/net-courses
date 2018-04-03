
namespace FiguresOnTheBoard.Core.Abstractions
{
    public delegate void Draw(IBoard board);
    public interface IBoard
    {
        int BoardSizeX { get; set; }
        int BoardSizeY { get; set; }

        void SetBoardSize(int x, int y);
    }
}
