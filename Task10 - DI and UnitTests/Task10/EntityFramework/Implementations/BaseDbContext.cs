using System;
using System.Collections.Generic;
using System.Linq;
using ORMCore;
using System.Data.Entity;

namespace EntityFramework.Implementations
{
    public abstract class BaseDbContext: DbContext, IDataContext
    {
        
        public DbSet<Trade> Trades { get; set; }

        public DbSet<Shareholder> Shareholders { get; set; }

        public DbSet<Balance> Balances { get; set; }
        

        IQueryable<Trade> IDataContext.Trades => this.Trades;

        IQueryable<Shareholder> IDataContext.Shareholders => this.Shareholders;

        IQueryable<Balance> IDataContext.Balances => this.Balances;
        


        #region Shareholders
        public void Add(Shareholder entity) => this.Shareholders.Add(entity);

        public void Remove(Shareholder entity) => this.Shareholders.Remove(entity);

        public void Update(Shareholder entity)
        {
            var modified = this.Shareholders.First(f => f.Id == entity.Id);
            modified.FirstName = entity.FirstName;
            modified.LastName = entity.LastName;
            modified.PhoneNumber = entity.PhoneNumber;
        }
        #endregion

        #region Balances

        public void Add(Balance entity) => this.Balances.Add(entity);

        public void Remove(Balance entity) => this.Balances.Remove(entity);

        public void Update(Balance entity)
        {
            var modified = this.Balances.First(f => f.Id == entity.Id);
            modified.FirstType = entity.FirstType;
            modified.SecondType = entity.SecondType;
            modified.ThirdType = entity.ThirdType;
            modified.BalanceValue = entity.BalanceValue;
            modified.BalanceZone = entity.BalanceZone;
        }

        #endregion

        #region Trades

        public void Add(Trade entity) => this.Trades.Add(entity);

        public void Remove(Trade entity) => this.Trades.Remove(entity);

        public void Update(Trade entity)
        {
            var modified = this.Trades.First(f => f.Id == entity.Id);
            modified.ShareholderId = entity.ShareholderId;
            modified.BuyerId = entity.BuyerId;
            modified.Value = entity.Value;
            modified.ValueType = entity.ValueType;
        }

        #endregion



        void IDataContext.SaveChanges() => this.SaveChanges();

        protected BaseDbContext(string connectionString) : base(connectionString)//:base()//
        {

        }

    }
}
