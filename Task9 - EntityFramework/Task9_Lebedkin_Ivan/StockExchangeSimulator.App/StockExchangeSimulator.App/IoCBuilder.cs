using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using StockExchangeSimulator.App.Implementations;
using StockExchangeSimulator.BL;
using StockExchangeSimulator.BL.Contracts;
using StockExchangeSimulator.BL.Domain;
using StockExchangeSimulator.Data.Repositories;

namespace StockExchangeSimulator.App
{
    public class IoCBuilder
    {
        public static IContainer Build(
            IEnumerable<ITransactionValidator> transactionValidators, 
            ILoggerService loggerService,
            StockExchangeDataContext dataContext
            )
        { 
            //var clients = new ClientService();
            var builder = new ContainerBuilder();

            builder.RegisterInstance(loggerService).As<ILoggerService>();
            builder.RegisterInstance(dataContext).As<IRepositoryClient>();
            builder.RegisterInstance(dataContext).As<IRepositoryStock>();
            builder.RegisterInstance(dataContext).As<IRepositoryTransaction>();
            builder.RegisterInstance(transactionValidators).As<IEnumerable<ITransactionValidator>>();

            builder.RegisterType<ClientService>().As<IClientService>().SingleInstance();
            builder.RegisterType<StockService>().As<IStockService>();
            builder.RegisterType<TransactionService>().As<ITransactionService>();
            builder.RegisterType<TransactionGenerator>().As<ITransactionGenerator>().SingleInstance();
            builder.RegisterType<TradeManager>();
            return builder.Build();
        }
    }
}

