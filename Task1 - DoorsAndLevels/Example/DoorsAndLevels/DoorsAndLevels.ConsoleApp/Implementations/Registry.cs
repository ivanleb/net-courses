using DoorsAndLevels.Core.Abstractions;
using System;

namespace DoorsAndLevels.ConsoleApp.Implementations
{
    public class ConsoleAppRegistry : IRegistry
    {
        public IDoorsNumbersBuilder DoorsNumbersBuilder { get; set; }
        public IShowMessageToUser ShowMessageToUser { get; set; }
        public IProcessUserInput ProcessUserInput { get; set; }
        public ILevelWatcher LevelWatcher { get; set; }

        public ConsoleAppRegistry()
        {
            // put initialization here;
            this.DoorsNumbersBuilder = new RandomDoorsNumbersBuilder();
            this.ShowMessageToUser = new ConsoleOutputWithDateAndTime();
            this.LevelWatcher = new SimpleDictionaryLevelWatcher();
            this.ProcessUserInput = new ConsoleProcessUserInput();
        }
    }
}
