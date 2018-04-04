using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board
{
    public interface IBoard
    {
        int BoardSizeX { get; set; }
        int BoardSizeY { get; set; }

        void DrawVerticalLine(int x, int lenght);
        void DrawHorizontalLine(int y, int lenght);
        void DrawPoint(int x, int y);

        void DrawBoard();
    }

    public interface IUserInterface
    {
        void ShowMessage(string message);

        string ProcessUserInput();
    }

    public interface IRegistry
    {
        IUserInterface UserInterface { get; set; }
        IBoard Board { get; set; }
    }

    public class Class1
    {

    }
}
