using Delegates.App.Implementations;
using Delegates.Core;

namespace Delegates.App
{
    internal class Program
    {
        private static void Main()
        {
            var registry = new Registry();
            AppLogic.Run(registry);
        }
    }
}