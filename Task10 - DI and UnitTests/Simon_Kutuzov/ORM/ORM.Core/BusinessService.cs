using System;
using System.Linq;
using ORM.Core.Abstractions;
using ORM.Core.Model;

namespace ORM.Core
{
    public class BusinessService : IBusinessService
    {
        private readonly IRepository dataContext;
        private readonly ILoggerService loggerService;

        public IQueryable<Trader> Traders { get => dataContext.Traders; }

        public BusinessService(IRepository dataContext, ILoggerService loggerService)
        {
            this.dataContext = dataContext;
            this.loggerService = loggerService;
        }

        public void AddTraider(string firstName, string secondName,
                               string phoneNumber, decimal balance)
        {
            var newTraider = new Trader
            {
                FirstName = firstName,
                SecondName = secondName,
                PhoneNumber = phoneNumber,
                Balance = balance,
            };

            loggerService.Info($"Adding new traider {firstName} {secondName} with {balance} in account");
            loggerService.RunWithExceptionLogging(() =>
            {
                dataContext.Add(newTraider);
                dataContext.SaveChanges();
            });
        }

        public void AddListing(string name, decimal price)
        {
            var newListing = new Listing
            {
                Name = name,
                Price = price,
            };

            loggerService.Info($"Adding new listing {name} priced at {price}");
            loggerService.RunWithExceptionLogging(() =>
            {
                dataContext.Add(newListing);
                dataContext.SaveChanges();
            });
        }

        public void AddShare(Listing listing, Trader owner)
        {
            var newShare = new Share
            {
                Listing = listing,
                Owner = owner,
            };

            loggerService.Info($"Adding new share for {listing.Name} owned by {owner}");
            loggerService.RunWithExceptionLogging(() =>
            {
                dataContext.Add(newShare);
                dataContext.SaveChanges();
            });
        }

        private void AddDeal(Trader buyer, Trader seller, Share share)
        {
            var newDeal = new Deal
            {
                Buyer = buyer,
                Seller = seller,
                Share = share,
                Amount = share.Listing.Price,
            };

            loggerService.Info($"{seller} sold {buyer} {share.Listing.Name} at {share.Listing.Price}");
            loggerService.RunWithExceptionLogging(() =>
            {
                dataContext.Add(newDeal);
                dataContext.SaveChanges();
            });
        }

        public void MakeDeal(Trader buyer, Trader seller, Share share)
        {
            if (buyer == seller)
            {
                loggerService.Error(new ArgumentException("Attempted to sell to self"));
                return;
            }
            if (buyer.Balance < 0)
            {
                loggerService.Error(new ArgumentException("Attempted to purchase a share with negative balance"));
                return;
            }
            if (!seller.Portfolio.Contains(share))
            {
                loggerService.Error(new ArgumentException("Attempted to sell a share not owned by seller"));
                return;
            }

            seller.Portfolio.Remove(share);
            seller.Balance += share.Listing.Price;
            buyer.Portfolio.Add(share);
            buyer.Balance -= share.Listing.Price;
            share.Owner = buyer;

            if (buyer.Balance == 0)
                buyer.Zone = Zone.Orange;
            if (buyer.Balance < 0)
                buyer.Zone = Zone.Black;

            AddDeal(buyer, seller, share);
        }

        public IQueryable<Trader> GetTraidersInOrangeZone()
        {
            return dataContext.Traders.Where(t => t.Zone == Zone.Orange);
        }
    }
}
