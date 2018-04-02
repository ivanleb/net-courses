using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoardDrawing.Core.Abstractions;

namespace BoardDrawing.Core
{
    public class ProgramLogic
    {
        public void Run(IRegistry registry)
        {
            var board = registry.Board;
            var showMessageToUser = registry.ShowMessageToUser;
            var proccesUserChoice = registry.ProccesUserChoice;
            char[] menuItems = proccesUserChoice.GetMenuItems();
            string infoMessage = showMessageToUser.GetInfoForUser();

            while (true)
            {
                board.DrawBoard();
                showMessageToUser.ShowMessage(infoMessage);
                showMessageToUser.ShowMenuItems(menuItems);
                string userChoice = proccesUserChoice.SelectFromMenu();
                if (userChoice == "exit")
                {
                    showMessageToUser.ShowMessage("\nFinishing");
                    break;
                }
                if (!board.Draw(userChoice, menuItems))
                {
                    showMessageToUser.ShowMessage("\nInvalid input, press enter to continue");
                    showMessageToUser.Pause();
                    board.ClearScreen();
                    continue;
                }
                board.ClearScreen();             
            }
        }
    }
}
