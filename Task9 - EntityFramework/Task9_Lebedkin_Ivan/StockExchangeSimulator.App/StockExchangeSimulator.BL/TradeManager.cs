using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using StockExchangeSimulator.BL.Contracts;
using StockExchangeSimulator.BL.Domain;
using StockExchangeSimulator.Data;
using StockExchangeSimulator.Data.Models;
using StockExchangeSimulator.Data.Repositories;

namespace StockExchangeSimulator.BL
{
    public class TradeManager
    {
        public bool IsContinue { get; set; }
        private IRepositoryClient _repositoryClient;
        private IRepositoryStock _repositoryStock;
        private IRepositoryTransaction _repositoryTransaction;
        private IClientService _clientService;
        private IStockService _stockService;
        private readonly ITransactionService _transactionService;
        private readonly ILoggerService _loggerService;
        private readonly ITransactionGenerator _transactionGenerator;
        private readonly IEnumerable<ITransactionValidator> _transactionValidators;

        public TradeManager(
            IRepositoryClient repositoryClient,
            IRepositoryStock repositoryStock,
            IRepositoryTransaction repositoryTransaction,
            IClientService clientService,
            IStockService stockService,
            ITransactionService transactionService,
            ILoggerService loggerService,
            ITransactionGenerator transactionGenerator,
            IEnumerable<ITransactionValidator> transactionValidators)
        {
            _repositoryClient = repositoryClient;
            _repositoryStock = repositoryStock;
            _repositoryTransaction = repositoryTransaction;
            _clientService = clientService;
            _stockService = stockService;
            _transactionService = transactionService;
            _loggerService = loggerService;
            _transactionGenerator = transactionGenerator;
            _transactionValidators = transactionValidators;
            IsContinue = true;
        }

        public void Run()
        {
            while (IsContinue)
            {
                var transaction =
                    _loggerService.RunWithExceptionLogging(
                        () => _transactionGenerator.GenerateTransaction(_transactionValidators),
                        isSilent: true
                    );

                if (transaction == null)
                    return;

                _transactionService.Add(transaction);
                _loggerService.Info(
                    $"\n{transaction.Seller.FirstName} {transaction.Seller.SurName} -> {transaction.Buyer.FirstName} {transaction.Buyer.SurName},\nstock number = {transaction.StocksQuantity}, stock price = {transaction.Stock.Price}\n");
                Thread.Sleep(2000);
            }

            _repositoryClient.SaveChanges();
        }

        public void ShowResults(Action<string> presenter)
        {
            //all
            presenter.Invoke("All clients:");
            var clients = _clientService.GetAllClients();

            string allClientsString = "";

            foreach (var client in clients)
            {
                allClientsString += $"{client.FirstName} {client.SurName}: balance - {client.Balance}\n";
            }
            presenter.Invoke(allClientsString);

            //black
            presenter.Invoke("Black zone clients:");
            var blackClients = _clientService.GetBlackClients();

            string allBlackClientsString = "";

            foreach (var client in blackClients)
            {
                allBlackClientsString += $"{client.FirstName} {client.SurName}: balance - {client.Balance}\n";
            }
            presenter.Invoke(allBlackClientsString);

            //orange
            presenter.Invoke("Orange zone clients:");
            var orangeClients = _clientService.GetOrangeClients();

            string allOrangeClientsString = "";

            foreach (var client in orangeClients)
            {
                allOrangeClientsString += $"{client.FirstName} {client.SurName}: balance - {client.Balance}\n";
            }
            presenter.Invoke(allOrangeClientsString);
        }
    }
}