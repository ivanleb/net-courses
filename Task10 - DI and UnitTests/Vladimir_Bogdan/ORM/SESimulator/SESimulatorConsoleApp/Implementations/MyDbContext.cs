using System.Data.Entity;

namespace SESimulatorConsoleApp.Implementations
{
    public class MyDbContext : BaseDbContext
    {
        public MyDbContext(string connectionString) : base(connectionString) { }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
