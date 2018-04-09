using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoardDrawing.Core.Abstractions;

namespace BoardDrawing.ConsoleApp.Implementations
{
    class ConsoleAppRegistry:IRegistry
    {
        public IBoard Board { get; set; }
        public IUserInteraction UserInteraction { get; set; }
        public IShowMessageToUser ShowMessageToUser { get; set; }
        public IModel Model { get; set; }

        public ConsoleAppRegistry()
        {
            Model = new ConsoleAppModel(new IHero[]
            {
                new ConsoleAppHero(10,10,'+')
            });
            Board = new BoardWithListOfPoints(Model);
            UserInteraction = new ConsoleAppUserInteraction();
            ShowMessageToUser = new ConsoleAppShowMessageToUser();
        }
    }
}
