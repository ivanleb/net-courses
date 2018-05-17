using System;
using System.Collections.Generic;

namespace Lambda.Console.Properties
{
    public class Subscriber
    {
        public Action<int> OnNumberReceived { get; set; }
        public IEnumerable<Func<int, bool>> Filters { get; set; }
    }
}