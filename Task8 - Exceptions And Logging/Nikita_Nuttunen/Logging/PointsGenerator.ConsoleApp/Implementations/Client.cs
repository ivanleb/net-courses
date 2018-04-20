using PointsGenerator.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PointsGenerator.ConsoleApp.Implementations
{
    public class Client
    {
        public void StartListeningToProducer(BadProducer producer)
        {
            producer.OnPointProduced += OnPointWritten;
        }

        private void OnPointWritten(object sender, IPoint point)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Client: {point}");
        }
    }
}
