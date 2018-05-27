using DIAndUnitTests.ConsoleApp.Implementations;
using DIAndUnitTests.Core;
using DIAndUnitTests.Core.Abstractions;
using log4net;
using StructureMap;

namespace DIAndUnitTests.ConsoleApp
{
    public class AlphaContainer : Registry
    {
        public AlphaContainer()
        {
            For<ILog>().Use(LogManager.GetLogger("mylog"));
            For<ILoggerService>().Singleton().Use<LoggerService>();
            For<IBusinessService>().Use<BusinessService>();
            For<ISimulator>().Use<Simulator>();
            For<IDataContext>().Use<DataContext>();
        }
    }
}