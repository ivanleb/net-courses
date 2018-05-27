using StructureMap;
using System.Threading.Tasks;
using DIAndUnitTests.ConsoleApp.Implementations;
using DIAndUnitTests.Core;
using DIAndUnitTests.Core.Abstractions;
using log4net;
using log4net.Config;

namespace DIAndUnitTests.ConsoleApp.Refactoring.Implementation
{
    internal class AlphaDataContext : IAlphaDataContext
    {
        public IDataContext DataContext { get; set; }
        public IBusinessService BusinessService { get; set; }
        public ISimulator Simulator { get; set; }

        public Task Run()
        {
            XmlConfigurator.Configure();
            
            //ILoggerService loggerService = new LoggerService(LogManager.GetLogger("mylog"));
            
            var container = new Container(new AlphaContainer());
            Simulator = container.GetInstance<ISimulator>();
            return new Task(() => { Simulator.Run(); });
            
/*            using (DataContext = new DataContext())
            {
                BusinessService = new BusinessService(DataContext, loggerService);
                Simulator = new Simulator(BusinessService, loggerService);

                return new Task(() => { Simulator.Run(); });
            }*/
        }
    }
}