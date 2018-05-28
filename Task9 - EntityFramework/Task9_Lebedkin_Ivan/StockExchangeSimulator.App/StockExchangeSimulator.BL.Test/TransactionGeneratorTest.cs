using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using StockExchangeSimulator.BL;
using StockExchangeSimulator.BL.Contracts;
using StockExchangeSimulator.BL.Domain;
using StockExchangeSimulator.Data;

namespace StockExchangeSimulator.BL.Test
{
    [TestFixture]
    public class TransactionGeneratorTest
    {
        private ITransactionGenerator _generator;
        private IEnumerable<ITransactionValidator> _transactionValidators;

        [SetUp]
        public void Init()
        {
            _generator = new TransactionGenerator(new RepositoryMock());
            _transactionValidators = new List<ITransactionValidator>(){new TransactionValidatorMock()};
        }

        [Test]
        public void GenerateTransaction_IdRequired()
        {
            //arrange section
            //ITransactionGenerator generator = null;
            //IEnumerable<ITransactionValidator> transactionValidators = null;
            //act section
            var transaction = _generator.GenerateTransaction(_transactionValidators);
            var id = transaction.Id;
            //assert section
            Assert.That(id, Is.Zero);
        }

        [Test]
        public void GenerateTransaction_BuyerRequired()
        {
            //arrange section
            //ITransactionGenerator generator = null;
            //IEnumerable<ITransactionValidator> transactionValidators = null;
            //act section
            var transaction = _generator.GenerateTransaction(_transactionValidators);
            var buyer = transaction.Buyer;
            //assert section
            Assert.That(buyer, Is.Not.Null);
        }

        [Test]
        public void GenerateTransaction_SellerRequired()
        {
            //arrange section
            //ITransactionGenerator generator = null;
            //IEnumerable<ITransactionValidator> transactionValidators = null;
            //act section
            var transaction = _generator.GenerateTransaction(_transactionValidators);
            var seller = transaction.Seller;
            //assert section
            Assert.That(seller, Is.Not.Null);
        }

        [Test]
        public void GenerateTransaction_StockRequired()
        {
            //arrange section
            //ITransactionGenerator generator = null;
            //IEnumerable<ITransactionValidator> transactionValidators = null;
            //act section
            var transaction = _generator.GenerateTransaction(_transactionValidators);
            var stock = transaction.Stock;
            //assert section
            Assert.That(stock, Is.Not.Null);
        }

        [Test]
        [Repeat(10000)]
        public void GenerateTransaction_StocksQuantityRequired()
        {
            //arrange section
            //ITransactionGenerator generator = null;
            //IEnumerable<ITransactionValidator> transactionValidators = null;
            //act section
            var transaction = _generator.GenerateTransaction(_transactionValidators);
            int stocksQuantity = transaction.StocksQuantity;
            //assert section
            Assert.That(stocksQuantity, Is.InRange(0, transaction.Seller.ClientStocksQuantity));
        }

    }


}
