using Delegates.Core.Abstractions;
using Task2_Delegates_ConsoleApplication.Implementations;

namespace Delegates.ConsoleApp.Implementations
{
    public class ConsoleDrawHorizontLine : IDraw
    {
        void IDraw.Draw(IBoard board)
        {
            for (var i = 1; i < board.BoardSizeX; i++)
            {
                board.BoardPoints.Add(new ConsolePoint(board.StartX + i, board.StartY + board.BoardSizeY / 2, "-"));
            }
        }
    } 
}