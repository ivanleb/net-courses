using Delegates.Core.Abstractions;

namespace Delegates.Core
{
    public class GameLogic
    {
        public void Run(IRegistry registry)
        {
            var board = registry.Board;
            var drawing = registry.Drawing;
            var processUserInput = registry.ProcessUserInput;
            var showMessageToUser = registry.ShowMessageToUser;
            Draw draw;

            board.SetBoardSize(21,13);
            drawing.DrawBoard(board);
            showMessageToUser.ShowMenu();

            while (true)
            {
                draw = null;

                switch (processUserInput.SelectMenuItem())
                {
                    case 0:
                        processUserInput.QuitGame();
                        break;
                    case 1:
                        draw = drawing.DrawDot;
                        break;
                    case 2:
                        draw = drawing.DrawVerticalLine;
                        break;
                    case 3:
                        draw = drawing.DrawHorizontalLine;
                        break;
                    default:
                        showMessageToUser.ShowMessage("Invalid input.");
                        break;

                }
                draw?.Invoke(board);
            }
        }
    }
}
