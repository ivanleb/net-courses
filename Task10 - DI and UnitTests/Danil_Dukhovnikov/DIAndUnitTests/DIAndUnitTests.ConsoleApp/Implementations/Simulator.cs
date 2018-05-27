using System;
using System.Linq;
using System.Threading;
using DIAndUnitTests.Core;
using DIAndUnitTests.Core.Abstractions;
using DIAndUnitTests.Core.Extentions;

namespace DIAndUnitTests.ConsoleApp.Implementations
{
    public interface ISimulator
    {
        bool KeepRunning { get; set; }
        void Run();
        void Stop();
    }

    public class Simulator : ISimulator
    {
        private readonly BusinessService _businessService;
        private readonly ILoggerService _loggerService;
        public bool KeepRunning { get; set; } = true;
        
        public Simulator(BusinessService businessService, ILoggerService loggerService)
        {
            _businessService = businessService;
            _loggerService = loggerService;
        }

        public void Run()
        {
            var traders = _businessService.GetAllTraders().ToList();

            while (KeepRunning)
            {
                var buyer = traders.GetRandomElement();
                var seller = traders.GetRandomElement();
                var share = seller.SharesCollection.GetRandomElement();

                if (share == null)
                {
                    _loggerService.Error(new NullReferenceException($"The seller is {seller} has no sellable stock"));
                }
                else
                {
                    _businessService.AddDeal(buyer, seller, share);
                }
                
                Thread.Sleep(10000);
            }
        }

        public void Stop() => KeepRunning = false;

    }
}