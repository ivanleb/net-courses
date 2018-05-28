using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using StockExchangeSimulator.BL.Contracts;
using StockExchangeSimulator.Data.Models;

namespace StockExchangeSimulator.BL.Test
{
    [TestFixture]
    public class TransactionValidatorTest
    {
        private ITransactionValidator validator;
        private Transaction transaction;

        [SetUp]
        public void Init()
        {
            validator = null;
            transaction = null;
        }

        [Test]
        public void IsTransactionValid_CheckTransaction()
        {
            var isValid = validator.IsTransactionValid(transaction);
        }
    }
}
