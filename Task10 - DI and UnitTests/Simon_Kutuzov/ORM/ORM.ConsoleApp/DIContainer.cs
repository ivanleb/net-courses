using StructureMap;
using log4net;
using ORM.Core;
using ORM.Core.Abstractions;
using ORM.ConsoleApp.Implementations;

namespace ORM.ConsoleApp
{
    public class DIContainer : Registry
    {
        public DIContainer()
        {
            For<ILog>().Use(LogManager.GetLogger("SampleLogger"));
            For<ILoggerService>().Singleton().Use<LoggerService>();
            For<IBusinessService>().Use<BusinessService>();
            For<IRepository>().Use<Repository>();
            For<ISimulation>().Use<Simulation>();
        }
    }
}
