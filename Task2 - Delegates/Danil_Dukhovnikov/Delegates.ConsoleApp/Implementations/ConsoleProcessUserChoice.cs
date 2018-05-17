using System;
using Delegates.Core.Abstractions;

namespace Delegates.ConsoleApp.Implementations
{
    public class ConsoleProcessUserChoice : IProcessUserChoice
    {
        public MenuOptions SelectedDrawingType()
        {
            var userChoice = Console.ReadLine();

            switch (userChoice)
            {
                case "1":
                    return MenuOptions.Point;
                case "2":
                    return MenuOptions.HorizontalLine;
                case "3":
                    return MenuOptions.VerticalLine;
                case "4":
                    return MenuOptions.Clear;
                case "5":
                    return MenuOptions.Exit;
                default:
                    return MenuOptions.Error;
            }
        }
    }
}