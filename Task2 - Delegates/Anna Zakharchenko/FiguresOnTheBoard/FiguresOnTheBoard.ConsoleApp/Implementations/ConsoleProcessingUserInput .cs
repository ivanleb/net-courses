using System;
using FiguresOnTheBoard.Core.Abstractions;

namespace FiguresOnTheBoard.ConsoleApp.Implementations
{
    public class ConsoleProcessingUserInput : IProcessUserInput
    {
        public int GetChoice()
        {            
            string userChoice = Console.ReadLine();
            int action;
            try
            {
                action = int.Parse(userChoice);
            }
            catch
            {
                action = -1;
            }
            return action;
        }
    }
}
