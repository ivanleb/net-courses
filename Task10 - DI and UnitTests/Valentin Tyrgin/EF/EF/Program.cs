using System;
using EF.Core.Abstractions;
using EF.Core.Implementations;
using EF.Core.Services;
using EF.Implementations.Entities;
using log4net;
using log4net.Config;
using log4net.Core;

namespace EF
{
    internal class Program
    {
        private static void Main()
        {
            XmlConfigurator.Configure();

            var container = new RegisterByContainer();

            using (var db = container.Container.GetInstance<IDataContext>())
            {
                var logger = container.Container.GetInstance<ILogService>();
                var businessService = container.Container.GetInstance<IBusiness>();
                var printer = container.Container.GetInstance<IPrinter>();
                var userInputHandler = container.Container.GetInstance<IUserInputHandler>();
                var userInput = container.Container.GetInstance<IUserInput>();

                printer.ShowMessage(userInputHandler.ToString());
                logger.Info(TradeOperation.Header);

                userInput.KeyPressed += userInputHandler.ProcessUserCommand;
                userInput.ListenToUser();

                printer.ShowAll(businessService.GetAllTraiders(), Trader.Header);
                printer.ShowAll(businessService.GetBlackZoneTraiders(), Trader.Header);
                printer.ShowAll(businessService.GetOrangeZoneTraiders(), Trader.Header);
                printer.ShowAll(businessService.GetAllOperations(), TradeOperation.Header);
            }
            Console.ReadLine();
        }
    }
}