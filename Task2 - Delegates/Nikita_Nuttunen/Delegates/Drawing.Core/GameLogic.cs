using Drawing.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Drawing.Core
{
    public class GameLogic
    {
        enum Actions { QuitGame = 0, DrawPoint = 1, DrawVerticalLine = 2, DrawHorizontalLine = 3, ClearBoard = 4, DrawNewBoard = 5 };
        public void Run(IRegistry registry)
        {            
            var board = registry.Board;
            var itemsBuilder = registry.ItemsBuilder;
            var processUserActions = registry.ProcessUserActions;
            var showMessageToUser = registry.ShowMessageToUser;
            Draw draw;

            showMessageToUser.ShowMessage("Hello!");
            processUserActions.SetBoardSize(board);
            showMessageToUser.DrawBoard(board);

            string message = "Choose action:\n" +
                        "0 - quit the game\n" +
                        "1 - draw a point\n" +
                        "2 - draw a vertical line\n" +
                        "3 - draw a horizontal line\n" +
                        "4 - clear the board\n" +
                        "5 - draw a new board\n";
            showMessageToUser.ShowMessage(message);

            while (true)
            {               
                draw = null;
                int actionNumber = processUserActions.GetChosenAction();
                switch (actionNumber)
                {
                    case (int)Actions.QuitGame:
                        processUserActions.QuitGame();
                        break;
                    case (int)Actions.DrawPoint:
                        draw = itemsBuilder.DrawPoint;
                        break;
                    case (int)Actions.DrawVerticalLine:
                        draw = itemsBuilder.DrawVerticalLine;
                        break;
                    case (int)Actions.DrawHorizontalLine:
                        draw = itemsBuilder.DrawHorizontalLine;
                        break;
                    case (int)Actions.ClearBoard:
                        showMessageToUser.ClearBoard();
                        break;
                    case (int)Actions.DrawNewBoard:
                        showMessageToUser.ClearScreen();
                        processUserActions.SetBoardSize(board);
                        showMessageToUser.DrawBoard(board);
                        showMessageToUser.ShowMessage(message);
                        break;
                    default:
                        showMessageToUser.ShowMessage("Wrong Input! Please try again");
                        break;
                }
                showMessageToUser.ClearInput();
                draw?.Invoke(board);
            }
        }
    }
}
