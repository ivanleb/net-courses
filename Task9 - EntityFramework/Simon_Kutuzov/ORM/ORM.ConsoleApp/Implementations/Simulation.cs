using System.Linq;
using ORM.Core;
using ORM.Core.Abstractions;

namespace ORM.ConsoleApp.Implementations
{
    public class Simulation : ISimulation
    {
        private readonly BusinessService businessService;
        private readonly ILoggerService loggerService;
        public bool KeepRunning { get; set; }

        public Simulation(BusinessService businessService, ILoggerService loggerService)
        {
            this.KeepRunning = true;
            this.loggerService = loggerService;
            this.businessService = businessService;
        }

        public void Run()
        {
            var traiders = businessService.Traiders.ToList();

            while (KeepRunning)
            {
                var buyer = traiders.RandomElement();
                var seller = traiders.RandomElement();
                var share = seller.Portfolio.RandomElement();

                if (share == null)
                    loggerService.Error(new System.NullReferenceException("Seller had nothing to sell"));
                else
                    businessService.MakeDeal(buyer, seller, share);

                System.Threading.Thread.Sleep(1000);
            }
        }
    }
}
