using System;
using FiguresOnTheBoard.Core.Abstractions;

namespace FiguresOnTheBoard.ConsoleApp.Implementations
{
    public class ConsoleProcessingUserInput : IProcessUserInput
    {
        public int GetChoice()
        {
            //ShowInstructionForUser(board, "Enter the action to affect the board.");
            string userChoice = Console.ReadLine();
            int action;
            try
            {
                action = int.Parse(userChoice);//число но пока не известно каое
            }
            catch
            {
                action = -1;//не число
            }
            return action;
        }
    }
}
