using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoggingAndExceptions.Abstracrions;

namespace LoggingAndExceptions.Implementation
{
    class Client : IClient
    {
        public void GetGoodPoint(object sender, IPoint point)
        {
            Console.WriteLine($"Client received the point from BadProducer: {point}");
        }
    }
}
