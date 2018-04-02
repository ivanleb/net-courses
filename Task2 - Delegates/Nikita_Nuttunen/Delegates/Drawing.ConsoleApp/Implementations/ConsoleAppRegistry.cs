using Drawing.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Drawing.ConsoleApp.Implementations
{
    public class ConsoleAppRegistry : IRegistry
    {
        public IBoard Board { get; set; }
        public IItemsBuilder ItemsBuilder { get; set; }
        public IProcessUserActions ProcessUserActions { get; set; }
        public IShowMessageToUser ShowMessageToUser { get; set; }

        public ConsoleAppRegistry()
        {
            this.Board = new Board();
            this.ItemsBuilder = new ItemsBuilder();
            this.ProcessUserActions = new ConsoleProcessUserActions();
            this.ShowMessageToUser = new ConsoleOutput();
        }
    }
}
