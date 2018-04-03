using Delegates.Core.Abstractions;
using Task2_Delegates_ConsoleApplication.Implementations;

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