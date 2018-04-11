using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPAM_homework_events
{
    class Registry : IRegistry
    {
        public IUserInterface UserInterface { get; set; }
        public IBoard Board { get; set; }
        public IController Controller { get; set; }

        public Registry()
        {
            this.UserInterface = new ConsoleInterface();

            this.Controller = new ConsoleController();

            this.Board = new ConsoleBoard(new ConsoleGameModel(new Point(40, 20)));
        }

    }
}
