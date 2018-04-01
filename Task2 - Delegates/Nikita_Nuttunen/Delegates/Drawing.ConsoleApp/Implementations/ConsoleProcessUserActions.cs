using Drawing.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Drawing.ConsoleApp.Implementations
{
    public class ConsoleProcessUserActions : IProcessUserActions
    {
        public int GetChosenAction()
        {
            int actionNumber;
            try
            {
                actionNumber = int.Parse(Console.ReadLine());
            }
            catch
            {
                return -1;
            }
            return actionNumber;
        }

        public void QuitGame()
        {
            Environment.Exit(0);
        }

        public void SetBoardSize(IBoard board)
        {
            int width;
            int height;
            
            while (true)
            {
                Console.WriteLine("Enter board width:");
                try
                {
                    width = int.Parse(Console.ReadLine());
                    if (width > 2 && width < Console.WindowWidth)
                    {
                        break;
                    }
                    Console.WriteLine("Width must be 2 - " + (Console.WindowWidth - 1));
                }
                catch
                {
                    Console.WriteLine("Wrong input! Try again");
                }
            }

            
            while (true)
            {
                Console.WriteLine("Enter board height:");
                try
                {
                    height = int.Parse(Console.ReadLine());
                    if (height > 2 && height <= 100)
                    {
                        break;
                    }
                    Console.WriteLine("Height must be 2 - 100");
                }
                catch
                {
                    Console.WriteLine("Wrong input! Try again");
                }
            }
            board.BoardSizeX = width;
            board.BoardSizeY = height;
        }
    }
}
