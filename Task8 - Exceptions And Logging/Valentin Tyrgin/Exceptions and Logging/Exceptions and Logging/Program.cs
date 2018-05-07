using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exceptions_and_Logging.Core;
using Exceptions_and_Logging.Implementation;

namespace Exceptions_and_Logging
{
    class Program
    {
        static void Main(string[] args)
        {
            Logger.InitLogger();
            var pointProducers = new IPointProducer[]
                {new Produce2XY(), new ProduceNotSimpleCoords(), new ProduceXYYYLessThan500()};

            var client = new Client {Name = "Red"};
            pointProducers[1].OnPointProduced += client.OnPointReceived;
            foreach (var producer in pointProducers)
            {
                Task.Run(() => producer.Run(x => Logger.Log.Info($"{producer.GetType().Name} produced a point {x}")));
            }
            while (true)
            {
                if (Console.ReadKey().Key != ConsoleKey.Escape) continue;
                foreach (var producer in pointProducers)
                {
                    producer.IsContinue = false;
                }
                break;
            }
        }
    }
}
