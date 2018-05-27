using StructureMap;
using log4net;
using ORM.Core;
using ORM.Core.Abstractions;
using ORM.Implementations;

namespace ORM
{
    public class DIContainer : Registry
    {
        public DIContainer() 
        {
            For<ILog>().Use(LogManager.GetLogger("StockExchangeLogger"));
            For<ILoggerService>().Use<LoggerService>().Singleton();
            For<IDataContext>().Use<StockExchangeDBContext>();
            For<BusinessLogic>().Use<BusinessLogic>();
            For<ISimulation>().Use<Simulation>();
        }
    }
}
