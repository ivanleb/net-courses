using System;
using Task2.Abstractions;

namespace Task2.Implementations
{
    public class Drawing : IDrawing
    {
        
        static void Draw(string symbol, int positionX, int positionY)
        {
            try
            {
                Console.SetCursorPosition(positionX, positionY);
                Console.Write(symbol);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.Clear();
                Console.WriteLine(e.Message);
            }
            Console.WriteLine("\n\n");
        }
       
        public void DrawAtBoard(IBoard board, string symbol, int positionX, int positionY)
        {
            Draw(symbol, board.BoardPositionX + positionX, board.BoardPositionY + positionY);
        }

        public void DrawBoard(IBoard board)
        {
            try
            {
                for (int line = board.BoardPositionX; line <= board.BoardSizeX; line++)
                    for(int topBottomLine = board.BoardPositionY; topBottomLine<=board.BoardSizeY;topBottomLine+=board.BoardSizeY)
                        Draw(board.BoardLineMarkerX,line, topBottomLine);
                
                for (int line = board.BoardPositionY+1; line < board.BoardSizeY; line++)
                    for (int leftRightLine = board.BoardPositionX; leftRightLine <= board.BoardSizeX; leftRightLine += board.BoardSizeX)
                        Draw(board.BoardLineMarkerY, leftRightLine, line);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.Clear();
                Console.WriteLine(e.Message);
            }
        }

        public void DrawHorizontalLine(IBoard board, int positionY)
        {
            for (int line = board.BoardPositionX+1; line < board.BoardSizeX; line++)
                Draw(board.BoardLineMarkerX, line, positionY);
        }
        public void DrawVerticalLine(IBoard board, int positionX)
        {
            for (int line = board.BoardPositionY + 1; line < board.BoardSizeY; line++)
                Draw(board.BoardLineMarkerY, positionX, line);
        }

        //public void DrawingLines(IBoard board)
        //{
        //    for (int line = board.BoardPositionX + 1; line < board.BoardSizeX; line++)
        //        drawingDelegate(board.BoardLineMarkerX, line,board.BoardSizeY/2);
        //    for (int line = board.BoardPositionY + 1; line < board.BoardSizeY; line++)
        //        drawingDelegate(board.BoardLineMarkerY, board.BoardSizeX/2, line);
        //}
        public void DrawCentralHorizontalLine(IBoard board)
        {
            for (int line = board.BoardPositionX + 1; line < board.BoardSizeX; line++)
                Draw(board.BoardLineMarkerX, line, board.BoardSizeY / 2);
        }
        public void DrawCentralVerticalLine(IBoard board)
        {
            for (int line = board.BoardPositionY + 1; line < board.BoardSizeY; line++)
                Draw(board.BoardLineMarkerY, board.BoardSizeX / 2, line);
        }
    }
}
