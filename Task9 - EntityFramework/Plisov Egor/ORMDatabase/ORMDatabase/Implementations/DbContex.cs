using System;
using System.Collections.Generic;
using System.Linq;
using ORMDatabaseCore;
using System.Data.Entity;


namespace ORMDatabase
{
    public class MyDbContex : DbContext, IDataContex
    {
        public MyDbContex(string connectionString) : base (connectionString)
        {
            Database.SetInitializer<MyDbContex>(new DropCreateDatabaseIfModelChanges<MyDbContex>());
        }

        public DbSet<Traider> Traiders { get; set; }
        public DbSet<TraiderBalance> TraiderBalances { get; set; }
        public DbSet<Deal> Deals { get; set; }

        IQueryable<Traider> IDataContex.Traiders => this.Traiders;
        IQueryable<TraiderBalance> IDataContex.TraiderBalances => this.TraiderBalances;
        IQueryable<Deal> IDataContex.Deals => this.Deals;


        public void Add(Traider entity) => this.Traiders.Add(entity);
        public void Add(TraiderBalance entity) => this.TraiderBalances.Add(entity);
        public void Add(Deal entity) => this.Deals.Add(entity);

        public void Remove(Traider entity) => this.Traiders.Remove(entity);
        public void Remove(TraiderBalance entity) => this.TraiderBalances.Remove(entity);
        public void Remove(Deal entity) => this.Deals.Remove(entity);

        public void Update(Traider entity)
        {
            Traider upd = this.Traiders.First(f => f.ID == entity.ID);
            upd.FirstName = entity.FirstName;
            upd.Surname = entity.Surname;
            upd.PhoneNum = entity.PhoneNum;
        }

        public void Update(TraiderBalance entity)
        {
            TraiderBalance upd = this.TraiderBalances.First(f => f.ID == entity.ID);
            upd.SimpleType = entity.SimpleType;
            upd.PreferenceShare = entity.PreferenceShare;
            upd.Balance = entity.Balance;
            upd.Zone = entity.Zone;
        }

        public void Update(Deal entity)
        {
            Deal upd = this.Deals.First(f => f.ID == entity.ID);
            upd.ID_seller = entity.ID_seller;
            upd.ID_buyer = entity.ID_buyer;
            upd.Price = entity.Price;
            upd.SharesType = entity.SharesType;
        }

        void IDataContex.SaveChanges() => this.SaveChanges();

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Traider>().ToTable("Traider");
            modelBuilder.Entity<TraiderBalance>().ToTable("TraiderBalance");
            modelBuilder.Entity<Deal>().ToTable("Deal");
        }
    }
}
