using DoorsAndLevels.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoorsAndLevels.Core
{

    public class GameLogic
    {
        
        public void Run(IRegistry registry)
        {
            var showMessageToUser = registry.ShowMessageToUser;
            var doorsNumbersBuilder = registry.DoorsNumbersBuilder;
            var processUserInput = registry.ProcessUserInput;
            var levelWatcher = registry.LevelWatcher;

            showMessageToUser.ShowMessage("Greetings!");
            var startDoorsNumbers = doorsNumbersBuilder.GetDoorsNumbersOnStart();
            levelWatcher.SetDoorsOnStart(startDoorsNumbers);

            while (true)
            {
                if (levelWatcher.GetCurrentLevel() == -1)
                {
                    showMessageToUser.ShowMessage("We are leaving this building!");
                    break;
                }

                var currentDoors = levelWatcher.GetDoorsOnCurrentLevel();

                showMessageToUser.ShowMessage("Please choose the door:");
                showMessageToUser.ShowDoorNumbers(currentDoors);

                var selectedDoor = processUserInput.SelectDoorNumber();

                if (!currentDoors.Contains(selectedDoor))
                {
                    showMessageToUser.ShowMessage("Incorrect door number!");
                    continue;
                }

                if (selectedDoor == 0)
                {
                    levelWatcher.GoPreviousLevel();
                }
                else
                {
                    levelWatcher.GoNextLevel(selectedDoor);
                }

            }
        }
    }
}
