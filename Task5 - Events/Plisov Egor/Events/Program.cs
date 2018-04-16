using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events
{
    class Program
    {
        static void Main(string[] args)
        {
            StaticRegistry.board = new Board(25, 25);
            StaticRegistry.model = new Model();
            StaticRegistry.input = new UserInput();
            GameLogic.Run();
        }
    }
}
