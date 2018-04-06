using Delegates.Core.Abstractions;

namespace Delegates.ConsoleApp.Implementations
{
    public class ConsoleAppRegistry : IRegistry
    {
        public IBoard Board { get; set; }
        public IShowMessageToUser ShowMessageToUser { get; set; }
        public IProcessUserChoice ProcessUserChoice { get; set; }

        public ConsoleAppRegistry()
        {
            Board = new ConsoleBoard(40, 15);
            ShowMessageToUser = new ConsoleShowMessageToUser();
            ProcessUserChoice = new ConsoleProcessUserChoice();
        }
    }
}