using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lambda
{
    class Generator
    {
        Random random;

        Dictionary <Action<int>, IEnumerable <Func<int, bool>>> subscr;

        public Generator()
        {
            subscr = new Dictionary<Action<int>, IEnumerable<Func<int, bool>>>();
            random = new Random();
        }

        public void NumGenerate(int count)
        {
            int num;

            for (int i = 0; i < count; i++)
            {
                num = random.Next() % 100;

                foreach (KeyValuePair<Action<int>, IEnumerable<Func<int, bool>>> part in subscr)
                {
                    bool isFiltered = true;

                    foreach (Func<int, bool> filter in part.Value)
                    {
                        if (!filter(num))
                        {
                            isFiltered = false;
                            break;
                        }
                    }

                    if (isFiltered)
                        part.Key(num);
                }

            }
        }

        public void Subscribe(Action<int> onReceived, IEnumerable<Func<int, bool>> useFilter)
        {
            subscr.Add(onReceived, useFilter);
        }
    }
}
