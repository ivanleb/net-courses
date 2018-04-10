using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPAM_homework_lambdas
{
    class NumberGenerator
    {
        Random rand;

        Dictionary<Action<int>, IEnumerable<Func<int, bool>>> Subscribers;

        public NumberGenerator()
        {
            Subscribers = new Dictionary<Action<int>, IEnumerable<Func<int, bool>>>();
            rand = new Random();
        }

        public void Subscribe(Action<int> onNumberReceived, IEnumerable<Func<int, bool>> useFilter)
        {
            Subscribers.Add(onNumberReceived, useFilter);
        }

        public void GenerateNumbers(int Amount)
        {
            int num;

            for (int i = 0; i < Amount; i++)
            {
                num = rand.Next() % 1000;

                foreach (KeyValuePair<Action<int>, IEnumerable<Func<int, bool>>> pair in Subscribers)
                {
                    bool isFiltered = true;

                    foreach (Func<int, bool> filter in pair.Value)
                    {
                        if (!filter(num))
                        {
                            isFiltered = false;
                            break;
                        }
                    }

                    if (isFiltered)
                        pair.Key(num);
                }

            }
        }
    }
}
