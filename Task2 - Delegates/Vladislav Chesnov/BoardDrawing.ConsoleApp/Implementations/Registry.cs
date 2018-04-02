using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoardDrawing.Core.Abstractions;

namespace BoardDrawing.ConsoleApp.Implementations
{
    class ConsoleAppRegistry : IRegistry
    {
        public IBoard Board { get; set; }
        public IProccesUserChoice ProccesUserChoice { get; set; }
        public IShowMessageToUser ShowMessageToUser { get; set; }

        public ConsoleAppRegistry()
        {
            this.Board = new BoardWithListOfPoints(40, 12);
            this.ProccesUserChoice = new ProccesUserChoice();
            this.ShowMessageToUser = new ShowMessageToUser();
        }
    }
}
