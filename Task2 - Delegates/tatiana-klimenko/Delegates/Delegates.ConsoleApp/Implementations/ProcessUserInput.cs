using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Delegates.Core.Abstractions;

namespace Delegates.ConsoleApp.Implementations
{
    public class ProcessUserInput : IProcessUserInput
    {
        public int SelectMenuItem()
        {
            int selectedItem;
            try
            {
                selectedItem = int.Parse(Console.ReadLine());
            }
            catch
            {
                selectedItem = -1;
            }
            return selectedItem;
        }

        public void QuitGame()
        {
            Environment.Exit(0);
        }
    }
}
