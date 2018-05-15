using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ORM;
using ORMCore.Model;

namespace ORM.Implementations
{
    class TPTUserDbContext : BaseDbContext
    {
        public TPTUserDbContext(string connectionString): base(connectionString)
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
