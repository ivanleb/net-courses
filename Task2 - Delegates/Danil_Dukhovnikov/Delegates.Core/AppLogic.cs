using Delegates.Core.Abstractions;

namespace Delegates.Core
{   
    public class AppLogic
    {
        public void Run(IRegistry registry)
        {
            var board = registry.Board;
            var showMessageToUser = registry.ShowMessageToUser;
            var processUserChoice = registry.ProcessUserChoice;

            while (true)
            {
                showMessageToUser.ShowMenu();
                var userChoice = processUserChoice.SelectedDrawingType();

                if (userChoice.Equals(DrawType.Stop))
                {
                    showMessageToUser.ShowMessage("Stopped.");
                    break;
                }

                if (userChoice.Equals(DrawType.Error))
                {
                    showMessageToUser.ShowMessage("Incorrect input!\n");
                    continue;
                }
                
                board.AddOnBoard(userChoice);
                board.Draw(board);
            }            
        }
    }
}