using System;
using EF.Core.Abstractions;
using EF.Core.Services;
using EF.Implementations;
using log4net;
using StructureMap;

namespace EF.Core.Implementations
{
    public class RegisterByContainer
    {
        public IContainer Container { get; set; }

        public RegisterByContainer()
        {
            var logger = LogManager.GetLogger("SampleTextLogger");
            Container = new Container(x =>
            {
                x.For<ILogService>().Use<Logger>().Ctor<ILog>().Is(logger);
                x.For<IDataContext>().ContainerScoped().Use<StockExchangeDataContext>().Ctor<string>().Is("DBConnection");
                x.For<ITransactionGenerator>().ContainerScoped().Use<RandomTransactionGenerator>();
                x.For<IPrinter>().ContainerScoped().Use<Printer>();
                x.For<IBusiness>().ContainerScoped().Use<BusinessService>();
                x.For<IUserInput>().ContainerScoped().Use<UserInput>();
                x.For<IUserInputHandler>().ContainerScoped().Use<UserInputHandler>().SetProperty(y =>
                {
                    y.Start = ConsoleKey.Enter;
                    y.Pause = ConsoleKey.P;
                    y.Stop = ConsoleKey.Escape;
                });
            });
        }
    }
}