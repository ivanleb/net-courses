using Delegates.Core.Abstractions;
using Task2_Delegates_ConsoleApplication.Implementations;

namespace Delegates.ConsoleApp.Implementations
{
    public class ConsoleDrawVerticalLine
    {
        void Draw(IBoard board)
        {
            for (var i = 1; i < board.BoardSizeY; i++)
            {
                board.BoardPoints.Add(new ConsolePoint(board.StartX + board.BoardSizeX / 2, board.StartY + i, "|"));
            }
        }
    }
}