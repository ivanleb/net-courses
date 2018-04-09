using System;
using System.Collections.Generic;

namespace Lambdas
{
    class NumberGenerator
    {
        private List<Subscriber> subs;

        private class Subscriber
        {
            public Action<int> OnNumberReceived { get; set; }
            public IEnumerable<Func<int, bool>> Filters { get; set; }
        }

        private void Notify(int n)
        {
            foreach (var sub in subs)
            {
                if ((sub.OnNumberReceived != null) && CheckFilters(n, sub.Filters))
                {
                    sub.OnNumberReceived(n);
                }
            }
        }

        private bool CheckFilters(int n, IEnumerable<Func<int, bool>> filters)
        {
            if (filters == null)
                return true;

            foreach (var filter in filters)
            {
                if (!filter(n))
                    return false;
            }

            return true;
        }

        public void Subscribe(Action<int> onNumberReceived, IEnumerable<Func<int, bool>> filters)
        {
            subs.Add(new Subscriber { OnNumberReceived = onNumberReceived, Filters = filters});
        }

        public void Generate(int limit)
        {
            for (int i = 0; i <= limit; i++)
            {
                Notify(i);
            }
        }

        public NumberGenerator()
        {
            subs = new List<Subscriber>();
        }
    }
}
