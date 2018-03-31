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
    }
}
