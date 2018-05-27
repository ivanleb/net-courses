using System;
using DIAndUnitTests.ConsoleApp.Implementations;

namespace DIAndUnitTests.ConsoleApp.Refactoring.Implementation
{
    internal class AlphaConsoleRepository : IAlphaConsoleRepository
    {
        public void Waiting(ISimulator simulator)
        {
            while (true)
            {
                if (Console.ReadKey().Key.Equals(ConsoleKey.Q))
                {
                    simulator.Stop();
                    break;
                }
            }
        }
    }
}