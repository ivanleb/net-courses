using Delegates.ConsoleApp.Implementations;
using Delegates.Core;

namespace Delegates.ConsoleApp
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            AppLogic.Run(new ConsoleAppRegistry());
        }
    }
}