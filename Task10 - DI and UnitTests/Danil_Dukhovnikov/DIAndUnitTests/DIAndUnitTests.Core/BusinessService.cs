using System;
using System.Collections.Generic;
using System.Linq;
using DIAndUnitTests.Core.Abstractions;
using DIAndUnitTests.Core.Models;

namespace DIAndUnitTests.Core
{
    public class BusinessService : IBusinessService
    {
        private readonly IDataContext _dataContext;
        private readonly ILoggerService _loggerService;

        public BusinessService(IDataContext dataContext, ILoggerService loggerService)
        {
            _loggerService = loggerService;
            _dataContext = dataContext;
        }

        public void AddTrader(string name, string surname, decimal balane,  string phoneNumber)
        {
            var trader = new Trader()
            {
                Balance = balane,
                Name = name,
                PhoneNumber = phoneNumber,
                Surname = surname
            };

            _dataContext.Add(trader);
            _dataContext.SaveChanges();
            
            _loggerService.Info($"New trader: {name} {surname} with {balane} in account was added");
        }

        public void AddShare(Trader owner, string name, decimal price)
        {
            var share = new Share()
            {
                Owner = owner,
                Name = name,
                Price = price
            };

            _dataContext.Add(share);
            _dataContext.SaveChanges();
            
            _loggerService.Info($"New share is {name} owned by {owner} was added");
        }

        public void RegisterDeal(Trader byer, Trader seller, Share share)
        {
            var deal = new Deal()
            {
                Buyer = byer,
                Seller = seller,
                Share = share,
                Amount = share.Price
            };

            _dataContext.Add(deal);
            _dataContext.SaveChanges();

            _loggerService.Info($"{byer} bought {share.Price} {share.Name} from {seller}");
        }

        public void AddDeal(Trader buyer, Trader seller, Share share)
        {

            {
                if (buyer.Balance < 0)
                {
                    _loggerService.Error(
                        new ArgumentException($"An attempt {buyer} to buy a share with a negative balance"));
                    return;
                }

                if (!seller.SharesCollection.Contains(share))
                {
                    _loggerService.Error(
                        new ArgumentException($"An attempt {seller} to sell a share not belonging to the seller"));
                    return;
                }

                seller.SharesCollection.Remove(share);
                seller.Balance += share.Price;

                buyer.SharesCollection.Add(share);
                buyer.Balance -= share.Price;

                share.Owner = buyer;

                RegisterDeal(buyer, seller, share);
            }
        }

        public IQueryable<Trader> GetOrangeTraders() =>
            _dataContext.Traders.Where(trader => TraderService.GetZone(trader) == Zone.Orange);
        
        public IEnumerable<Trader> GetAllTraders() => _dataContext.Traders;
    }
}