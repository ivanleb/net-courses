using System;
using Delegates.Core.Abstractions;

namespace Delegates.Core
{   
    public static class AppLogic
    {
        public static void Run(IRegistry registry)
        {
            Draw draw = null;
            
            var board = registry.Board;
            var drawing = registry.Drawing;
            var showMessageToUser = registry.ShowMessageToUser;
            var processUserChoice = registry.ProcessUserChoice;
            
            drawing.DrawBoard(board);

            while (true)
            {
                showMessageToUser.ShowMenu();
                var userChoice = processUserChoice.SelectedDrawingType();

                switch (userChoice)
                {
                    case MenuOptions.Point:
                        draw = drawing.DrawPoint;
                        break;
                    case MenuOptions.VerticalLine:
                        draw = drawing.DrawVerticalLine;
                        break;
                    case MenuOptions.HorizontalLine:
                        draw = drawing.DrawHorizontalLine;
                        break;
                    case MenuOptions.Clear:
                        drawing.DrawBoard(board);
                        draw = null;
                        break;
                    case MenuOptions.Error:
                        showMessageToUser.ShowMessage("Incorrect input!\n");
                        break;
                    case MenuOptions.Exit:
                        showMessageToUser.ShowMessage("Stopped.");
                        return;
                    default:
                        draw = null;
                        break;
                }  
                
                draw?.Invoke(board);
            }            
        }
    }
}