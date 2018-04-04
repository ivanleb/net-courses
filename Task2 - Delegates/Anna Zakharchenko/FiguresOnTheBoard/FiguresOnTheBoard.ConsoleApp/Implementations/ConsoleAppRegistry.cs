using System;
using FiguresOnTheBoard.Core.Abstractions;

namespace FiguresOnTheBoard.ConsoleApp.Implementations
{
    public class ConsoleAppRegistry : IRegistry
    {
        public IBoard Board { get; set; }
        public IProcessUserInput ProcessUserInput { get; set; }
        public IShowMessageToUser ShowMessageToUser { get; set; }
        public IDrawing Drawing { get; set; }
        public IExecuteUserChoice ExecuteUserChoice { get; set; }


        public ConsoleAppRegistry()
        {
            this.Board = new Board();
            this.ProcessUserInput = new ConsoleProcessingUserInput();
            this.ShowMessageToUser = new ConsoleShowMessageToUser();
            this.Drawing = new ConsoleDrawing();
            this.ExecuteUserChoice = new NumericExecuteUserChoice();
            this.ProcessUserInput = new ConsoleProcessingUserInput();
        }
    }
}
