using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockExchangeSimulator.Data.Models;

namespace StockExchangeSimulator.Data.Repositories
{
    public abstract class RepositoryClient : DbContext, IRepositoryClient
    {
        protected RepositoryClient(string connectionString) : base(connectionString)
        {
        }

        public DbSet<Client> Clients { get; set; }

        IQueryable<Client> IRepositoryClient.Clients => this.Clients;

        int IRepository<Client>.Add(Client entity) => this.Clients.Add(entity).Id;
        void IRepository<Client>.Remove(Client entity) => this.Clients.Remove(entity);
        void IRepository<Client>.Update(Client entity)
        {
            var modified = this.Clients.First(f => f.Id == entity.Id);
            modified.FirstName = entity.FirstName;
            modified.SurName = entity.SurName;
            modified.TelephonNumber = entity.TelephonNumber;
            modified.Balance = entity.Balance;
            //modified.Zone = modified.Zone;
            modified.Stock = modified.Stock;
            modified.ClientStocksQuantity = modified.ClientStocksQuantity;
        }

        void IRepositoryClient.SaveChanges() => this.SaveChanges();
    }
}
