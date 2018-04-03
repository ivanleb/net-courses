using BoardGame.Core.Models;

namespace BoardGame.Core.Abstractions
{
    public delegate void Draw(Board board);
    public interface IBoardHandler
    {
        Draw DrawBoard { get; }
    }
}