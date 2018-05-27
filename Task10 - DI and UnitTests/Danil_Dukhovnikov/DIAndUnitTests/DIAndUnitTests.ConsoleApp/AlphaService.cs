using DIAndUnitTests.ConsoleApp.Refactoring;

namespace DIAndUnitTests.ConsoleApp
{
    public class AlphaService
    {
        private readonly IAlphaDataContext _alphaDataContext;
        private readonly IAlphaConsoleRepository _alphaConsoleRepository;

        public AlphaService(IAlphaDataContext alphaDataContext, IAlphaConsoleRepository alphaConsoleRepository)
        {
            _alphaDataContext = alphaDataContext;
            _alphaConsoleRepository = alphaConsoleRepository;
        }

        public void Run()
        {
            _alphaDataContext.Run().RunSynchronously();
            _alphaConsoleRepository.Waiting(_alphaDataContext.Simulator);
            
            
/*          XmlConfigurator.Configure();
            var loggerService = new LoggerService(LogManager.GetLogger("mylog"));*/

/*            using (var dataContext = new DataContext())
            {
                var businessService = new BusinessService(dataContext, loggerService);
                var simulator = new Simulator(businessService, loggerService);

                Task.Run(() => { simulator.Run(); });

                /*while (true)
                {
                    if (Console.ReadKey().Key.Equals(ConsoleKey.Q))
                    {
                        simulator.Stop();
                        break;
                    }
                }#1#
            }*/
        }
    }
}