using log4net;
using ORMConsoleApp.Implementations;
using ORMCore;
using ORMCore.Abstractions;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORMConsoleApp
{
    public class DIContainer : Registry
    {
        public DIContainer()
        {
            For<ILog>().Use(LogManager.GetLogger("StockExchangeLogger"));
            For<ILoggerService>().Use<LoggerService>().Singleton();            
            For<IDataContext>().Use<StockExchangeDbContext>();
            For<BusinessService>().Use<BusinessService>();
            For<IDealProducer>().Use<DealProducer>();            
        }
    }
}
