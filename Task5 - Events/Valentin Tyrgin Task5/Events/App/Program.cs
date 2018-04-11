using Events.App.Implementations;
using Events.Core;

namespace Events.App
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            GameLogic.Run(new Registry());
        }
    }
}