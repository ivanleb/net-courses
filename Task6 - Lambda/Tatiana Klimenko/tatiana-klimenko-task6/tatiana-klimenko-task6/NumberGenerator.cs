using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tatiana_klimenko_task6
{
    class NumberGenerator
    {
        public int Numbers { get; set; }

        public NumberGenerator(int numbers)
        {
            this.Numbers = numbers;
        }

        //subscribe with one filter
        public void Subscribe(Action<int> onNumberReceived, Func<int, bool> filter)
        {
            for (int i = 0; i < Numbers; i++)
            {
                if (filter(i))
                {
                    onNumberReceived(i);
                }
            }
        }

        //subscribe with several filters
        public void Subscribe(Action<int> onNumberReceived, IEnumerable<Func<int, bool>> filters)
        {
            for (int i = 0; i < Numbers; i++)
            {
                if (filters.All(filter => filter(i)))
                {
                    onNumberReceived(i);
                }
            }
        }
    }
}
