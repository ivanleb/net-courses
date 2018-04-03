using Delegates.ConsoleApp.Implementations;
using Delegates.Core.Abstractions;

namespace Task2_Delegates_ConsoleApplication.Implementations
{
    public class ConsoleDrawPoint : IDraw
    {
        void IDraw.Draw(IBoard board)
        {
            board.BoardPoints.Add(new ConsolePoint(board.StartX + board.BoardSizeX / 5,
                board.StartY + board.BoardSizeY / 5, "+"));
        }

    }
}