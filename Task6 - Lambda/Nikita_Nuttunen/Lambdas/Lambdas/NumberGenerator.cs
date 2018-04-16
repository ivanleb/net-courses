using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lambdas
{
    class NumberGenerator
    {
        bool UseFilter(int number, IEnumerable<Func<int, bool>> useFilter)
        {
            foreach (var filter in useFilter)
            {
                if (!filter(number)) return false;
            }
            return true;
        }

        public void Subscribe(Action<int> onNumberReceived, IEnumerable<Func<int, bool>> useFilter)
        {
            for (int number = 1; number <= 50; number++)
            {
                if (UseFilter(number, useFilter))
                {
                    onNumberReceived(number);
                }
            }
        }

        public void ShowNumber(int number)
        {
            Console.Write(" " + number);
        }
    }
}
