using Board;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPAM_homework_delegates
{
    class ProgramLogic
    {
        delegate void Draw(IBoard board);

        public void DrawVerticalLine(IBoard board)
        {
            board.DrawVerticalLine(board.BoardSizeX / 2, board.BoardSizeY);
            Console.SetCursorPosition(0, board.BoardSizeY + 1);
        }
        public void DrawHorizontalLine(IBoard board)
        {
            board.DrawHorizontalLine(board.BoardSizeY / 2, board.BoardSizeX);
            Console.SetCursorPosition(0, board.BoardSizeY + 1);
        }

        public void DrawPoint(IBoard board)
        {
            board.DrawPoint(board.BoardSizeX / 4, board.BoardSizeY / 4);
            Console.SetCursorPosition(0, board.BoardSizeY + 1);
        }

        public void DisplayMenu(IUserInterface UserInterface)
        {
            UserInterface.ShowMessage("");
            UserInterface.ShowMessage("Select what you want to draw or to delete:");
            UserInterface.ShowMessage("1)Vertical line");
            UserInterface.ShowMessage("2)Horizontal line");
            UserInterface.ShowMessage("3)Point");
        }

        public void Run(IRegistry registry)
        {
            var UserInterface = registry.UserInterface;
            var Board = registry.Board;

            Draw drawObjects = null;

            string objectToDraw;

            Board.DrawBoard();
            DisplayMenu(UserInterface);

            while ((objectToDraw = UserInterface.ProcessUserInput()) != "`")
            {
                Console.Clear();

                switch (objectToDraw)
                {
                    case "1":
                        if (drawObjects == null || !drawObjects.GetInvocationList().Contains((Draw)DrawVerticalLine))
                            drawObjects += DrawVerticalLine;
                        else
                            drawObjects -= DrawVerticalLine;

                        break;
                    case "2":
                        if (drawObjects == null || !drawObjects.GetInvocationList().Contains((Draw)DrawHorizontalLine))
                            drawObjects += DrawHorizontalLine;
                        else
                            drawObjects -= DrawHorizontalLine;
                        break;
                    case "3":
                        if (drawObjects == null || !drawObjects.GetInvocationList().Contains((Draw)DrawPoint))
                            drawObjects += DrawPoint;
                        else
                            drawObjects -= DrawPoint;
                        break;
                }

                drawObjects?.Invoke(Board);

                Board.DrawBoard();
                DisplayMenu(UserInterface);
            }
        }
    }
}
