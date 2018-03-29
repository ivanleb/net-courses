using DoorsAndLevels.Core.Abstractions;
using System;

namespace DoorsAndLevels.ConsoleApp.Implementations
{
    public class ConsoleProcessUserInput : IProcessUserInput
    {
        public int SelectDoorNumber()
        {
            var userChoice = Console.ReadLine();
            return int.Parse(userChoice);
        }
    }
}
