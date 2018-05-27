using System;
using System.Threading.Tasks;
using StructureMap;
using ORM.Core.Abstractions;

namespace ORM.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            log4net.Config.XmlConfigurator.Configure();
            var container = new Container(new DIContainer());
            var simulation = container.GetInstance<ISimulation>();

            Task.Run(() =>
            {
                simulation.Run();
            });

            while (true)
            {
                if (Console.ReadKey().Key == ConsoleKey.Escape)
                {
                    simulation.KeepRunning = false;
                    break;
                }
            }
        }
    }
}
