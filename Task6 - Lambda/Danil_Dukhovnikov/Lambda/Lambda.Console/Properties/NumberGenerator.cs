using System;
using System.Collections.Generic;
using System.Linq;

namespace Lambda.Console.Properties
{
    public class NumberGenerator
    {
        private readonly IList<Subscriber> _subscribers;

        public NumberGenerator()
        {
            _subscribers = new List<Subscriber>();
        }
        
        private void Notify(int n)
        {
            foreach (var sub in _subscribers)
            {
                if ((sub.OnNumberReceived != null) && CheckFilters(n, sub.Filters))
                {
                    sub.OnNumberReceived(n);
                }
            }
        }
        
        private static bool CheckFilters(int x,IEnumerable<Func<int, bool>> filters)
        {
            return filters == null || filters.All(filter => filter(x));
        }        
        
        public void Subscribe(Action<int> onNumberReceived, IEnumerable<Func<int, bool>> filters)
        {
            _subscribers.Add(new Subscriber
                {
                    OnNumberReceived = onNumberReceived, 
                    Filters = filters
                }
            );
        }
        
        public void Generate(int max)
        {
            for (var i = 0; i <= max; i++)
            {
                Notify(i);
            }
        }
    }
}