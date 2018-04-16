using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberGeneratorLambda
{
    class NumberGenerator
    {
        public int Numbers { get; set; }
        public NumberGenerator(int numbersCount)
        {
            Numbers = numbersCount;
        }

        public void SubscribeWithOneFilter(Action<int> onNumberRecieved, Func<int, bool> useFilter)
        {
            for(int i = 1; i < Numbers; i++)
            {
                if (useFilter(i))
                {
                    onNumberRecieved(i);
                }
            }
        }

        public void SubscribeWithSeveralFilters(Action<int> onNumberRecieved, IEnumerable<Func<int, bool>> filters)
        {
            for (int i = 1; i < Numbers; i++)
            {
                if(filters.All(usefilter=>usefilter(i)))
                {
                    onNumberRecieved(i);
                }
            }
        }

    }
}
