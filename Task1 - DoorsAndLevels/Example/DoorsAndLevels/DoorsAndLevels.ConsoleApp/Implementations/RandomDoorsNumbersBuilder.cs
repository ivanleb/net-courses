using DoorsAndLevels.Core.Abstractions;
using System;

namespace DoorsAndLevels.ConsoleApp.Implementations
{
    public class RandomDoorsNumbersBuilder : IDoorsNumbersBuilder
    {
        private Random rnd = new Random();

        private const int DoorsAmount = 4;

        public int[] GetDoorsNumbersOnStart()
        {
            return new int[]
            {
                this.rnd.Next(1, 10),
                this.rnd.Next(1, 10),
                this.rnd.Next(1, 10),
                0
            };
        }
    }
}
