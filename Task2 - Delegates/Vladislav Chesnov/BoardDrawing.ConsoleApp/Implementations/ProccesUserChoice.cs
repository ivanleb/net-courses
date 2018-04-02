using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoardDrawing.Core.Abstractions;

namespace BoardDrawing.ConsoleApp.Implementations
{
    class ProccesUserChoice : IProccesUserChoice
    {
        public char[] GetMenuItems()
        {
            char[] menuItems = { '1', '2', '3', '4' };
            return menuItems;
        }

        public string SelectFromMenu()
        {
            return Console.ReadLine();
        }

        public string GetInfoForUser()
        {
            return "\nEach number represents a single operation.\nInput a numbers together without any symbols between (1 for operation 1, 123 for operations 1 then 2 then 3) and press enter to start drawing. \nTo exit type 'exit'";
        }
    }
}
