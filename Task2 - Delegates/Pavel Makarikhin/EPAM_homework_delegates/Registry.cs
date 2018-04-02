using Board;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPAM_homework_delegates
{
    class Registry : IRegistry
    {
        public IUserInterface UserInterface { get; set; }
        public IBoard Board { get; set; }

        public Registry()
        {
            this.UserInterface = new ConsoleInterface();
            this.Board = new ConsoleBoard(20, 20);
        }
    }
}
