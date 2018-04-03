using System;
using FiguresOnTheBoard.Core.Abstractions;

namespace FiguresOnTheBoard.ConsoleApp.Implementations
{
    public class NumericExecuteUserChoice : IExecuteUserChoice
    {
        private enum Action {Exit, Dot, HorizontalLine, VerticalLine}
        private Draw draw;
        public bool IsExit(int action)
        {
            return action == (int)Action.Exit;       
        }

        public bool IsValid(int action)
        {
            return Enum.IsDefined(typeof(Action), action);
        }

        public void MakeFigure(IBoard board, IDrawing drawing, int action)
        {
            draw = null;

            switch (action)
            {
                case (int)Action.Dot:
                    draw = drawing.DrawDot;
                    break;
                case (int)Action.HorizontalLine:
                    draw = drawing.DrawHorizontalLine;
                    break;
                case (int)Action.VerticalLine:
                    draw = drawing.DrawVerticalLine;
                    break;
                default:
                    break;
            }
            int currentY = Console.CursorTop;
            draw?.Invoke(board);
            Console.SetCursorPosition(0, currentY-1);

        }

    }
}
