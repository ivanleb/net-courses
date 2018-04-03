using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Delegates.Core.Abstractions;

namespace Delegates.Implementations
{
    public class ConsoleProcessUserInput : IProcessUserInput
    {
        public IDrawingObject InputObject()
        {
            Console.WriteLine("\nSelect what you want draw: 0 - point, 1 - horizontal line, 2 - vertical line\nother - delete object\n-1 - Exit from app");
            var userChoice = Console.ReadLine();
            if (int.Parse(userChoice) == -1) Environment.Exit(0);
            else if (int.Parse(userChoice) == 0) return new Point();
            else if (int.Parse(userChoice) == 1) return new HorizontalLine();
            else if (int.Parse(userChoice) == 2) return new VerticalLine();
            return null;
        }
        public void DeleteObjectFromBoard(IBoard board)
        {
            IDrawingObject obj = new DrawingObject();
            Console.WriteLine("Select what you want delete: 0 - point, 1 - horizontal line, 2 - vertical line\n3 - Back to drawing\n-1 - Exit from app");
            var userChoice = Console.ReadLine();
            while (int.Parse(userChoice) < -1 || int.Parse(userChoice) > 3)
            {
                Console.WriteLine("Inavlid symbol. Try again!\nSelect what you want delete: 0 - point, 1 - horizontal line, 2 - vertical line\n3 - Back to drawing\n-1 - Exit from app");
                userChoice = Console.ReadLine();
            }
            if (int.Parse(userChoice) == -1) Environment.Exit(0);
            else if (int.Parse(userChoice) == 0) obj = new Point();
            else if (int.Parse(userChoice) == 1) obj = new HorizontalLine();
            else if (int.Parse(userChoice) == 2) obj = new VerticalLine();
            else return;
            board.DeleteObject(obj);            
        }
    }
}
