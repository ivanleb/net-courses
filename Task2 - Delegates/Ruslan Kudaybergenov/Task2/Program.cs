using System;
using Task2.Implementations;
using Task2.Abstractions;

namespace Task2
{
    class Program
    {
        delegate void DrawingLines(IBoard board);
        static DrawingLines drawingLines = null;
        static void Main(string[] args)
        {
            var board = new Board();
            var drawing = new Drawing();
            var show = new ShowMessage();
            Input input = new Input();
            int menuChoice = 1;
            while (menuChoice != 0)
            {
                drawing.DrawBoard(board);
                menuChoice = input.SettedOperation;
                switch (menuChoice)
                {
                    case 1:
                        {
                            drawing.DrawAtBoard(board, "*", board.BoardSizeX / 4, board.BoardSizeY / 4);
                            break;
                        }
                    case 2:
                        {
                            drawing.DrawHorizontalLine(board, board.BoardSizeY / 2);
                            break;
                        }
                    case 3:
                        {
                            drawing.DrawVerticalLine(board, board.BoardSizeX / 2);
                            break;
                        }
                    case 4:
                        {
                            drawingLines += drawing.DrawCentralHorizontalLine;
                            drawingLines += drawing.DrawCentralVerticalLine;
                            drawingLines(board);
                            break;
                        }
                }
            }
        }
    }
}



