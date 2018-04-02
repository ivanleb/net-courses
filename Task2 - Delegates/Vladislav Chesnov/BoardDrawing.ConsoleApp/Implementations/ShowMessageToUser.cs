using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoardDrawing.Core.Abstractions;

namespace BoardDrawing.ConsoleApp.Implementations
{
    class ShowMessageToUser : IShowMessageToUser
    {
        public void ShowMenuItems(char[] menuItems)
        {
            Console.WriteLine("Press 1 to draw a point in the center of upper left corner" +
                "\npress 2 to draw a vertical line, that splits board in half" +
                "\npress 3 to draw a horizontal line that splits board in half" +
                "\npress 4 to clear the board");
            Console.WriteLine(string.Join(", ", menuItems));
        }

        public void ShowMessage(string message)
        {
            Console.WriteLine(message);
        }
    }
}
