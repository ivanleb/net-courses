using System;
using Delegates.Core.Abstractions;

namespace Delegates.ConsoleApp.Implementations
{
    public class ConsoleProcessUserChoice : IProcessUserChoice
    {
        public DrawType SelectedDrawingType()
        {
            var userChoice = Console.ReadLine();

            switch (userChoice)
            {
                case "1":
                    return DrawType.Point;
                case "2":
                    return DrawType.HorizontalLine;
                case "3":
                    return DrawType.VerticalLine;
                case "4":
                    return DrawType.Clear;
                case "5":
                    return DrawType.Stop;
                default:
                    return DrawType.Error;
            }
        }
    }
}