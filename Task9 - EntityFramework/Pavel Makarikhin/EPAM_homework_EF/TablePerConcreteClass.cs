using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using EPAM_homework_EF_Core;
using System.ComponentModel.DataAnnotations.Schema;

namespace EPAM_homework_EF
{
    public class TablePerConcreteClass : DbContext, IDataContext
    {
        public DbSet<WhiteZoneClient> WhiteZoneClients { get; set; }
        public DbSet<OrangeZoneClient> OrangeZoneClients { get; set; }
        public DbSet<BlackZoneClient> BlackZoneClients { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Share> Shares { get; set; }
        public DbSet<ClientShare> ClientsShares { get; set; }
        public DbSet<DealHistory> History { get; set; }

        IQueryable<WhiteZoneClient> IDataContext.WhiteZoneClients => WhiteZoneClients;
        IQueryable<OrangeZoneClient> IDataContext.OrangeZoneClients => OrangeZoneClients;
        IQueryable<BlackZoneClient> IDataContext.BlackZoneClients => BlackZoneClients;
        IQueryable<Client> IDataContext.Clients => Clients;
        IQueryable<Share> IDataContext.Shares => Shares;
        IQueryable<ClientShare> IDataContext.ClientsShares => ClientsShares;
        IQueryable<DealHistory> IDataContext.History => History;

        public int Add(WhiteZoneClient entity) => WhiteZoneClients.Add(entity).ClientId;
        public int Add(OrangeZoneClient entity) => OrangeZoneClients.Add(entity).ClientId;
        public int Add(BlackZoneClient entity) => BlackZoneClients.Add(entity).ClientId;
        public int Add(Client entity) => Clients.Add(entity).Id;
        public int Add(Share entity) => Shares.Add(entity).ShareId;
        public int Add(DealHistory entity) => History.Add(entity).DealId;
        public int Add(ClientShare entity) => ClientsShares.Add(entity).ClientId;

        public void Remove(WhiteZoneClient entity) => WhiteZoneClients.Remove(entity);
        public void Remove(OrangeZoneClient entity) => OrangeZoneClients.Remove(entity);
        public void Remove(BlackZoneClient entity) => BlackZoneClients.Remove(entity);
        public void Remove(Client entity) => Clients.Remove(entity);
        public void Remove(Share entity) => Shares.Remove(entity);
        public void Remove(DealHistory entity) => History.Remove(entity);
        public void Remove(ClientShare entity) => ClientsShares.Remove(entity);

        public void Update(WhiteZoneClient entity)
        {
        }
        public void Update(OrangeZoneClient entity)
        {
            OrangeZoneClients.First(m => m.ClientId == entity.ClientId).Timeout = entity.Timeout;
        }
        public void Update(BlackZoneClient entity)
        {
            BlackZoneClients.First(m => m.ClientId == entity.ClientId).Penalty = entity.Penalty;
        }
        public void Update(Client entity)
        {
            var modified = Clients.First(m => m.Id == entity.Id);

            modified.FirstName = entity.FirstName;
            modified.LastName = entity.LastName;
            modified.Number = entity.Number;
            modified.Balance = entity.Balance;
        }
        public void Update(Share entity)
        {
            var modified = Shares.First(m => m.ShareId == entity.ShareId);

            modified.ShareName = entity.ShareName;
            modified.ShareCost = entity.ShareCost;
        }
        public void Update(DealHistory entity)
        {
            var modified = History.First(m => m.DealId == entity.DealId);

            modified.BuyerId = entity.BuyerId;
            modified.SellerId = entity.SellerId;
            modified.ShareId = entity.ShareId;
            modified.Amount = entity.Amount;
            modified.Total = entity.Total;
        }
        public void Update(ClientShare entity)
        {
            var modified = ClientsShares.First(m => m.ClientId == entity.ClientId && m.ShareId == entity.ShareId).Amount = entity.Amount;
        }
         
        void IDataContext.SaveChanges() => SaveChanges();

        public TablePerConcreteClass(string connectionString) : base(connectionString)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<WhiteZoneClient>().HasKey(m => m.ClientId);

            modelBuilder.Entity<OrangeZoneClient>().Map(m =>
            {
                m.MapInheritedProperties();
                m.ToTable("OrangeZoneClients");
            }).HasKey(m => m.ClientId);

            modelBuilder.Entity<BlackZoneClient>().Map(m =>
            {
                m.MapInheritedProperties();
                m.ToTable("BlackZoneClients");
            }).HasKey(m => m.ClientId);


            modelBuilder.Entity<Client>().HasKey(m => m.Id).Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<Share>().HasKey(m => m.ShareId).Property(m => m.ShareId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<ClientShare>().HasKey(p => new { p.ClientId, p.ShareId });

            modelBuilder.Entity<DealHistory>().HasKey(m => m.DealId).Property(m => m.DealId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

        }
    }
}
