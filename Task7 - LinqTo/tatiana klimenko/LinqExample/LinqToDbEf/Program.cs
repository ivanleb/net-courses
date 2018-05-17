using LinqExampleCore;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Data.SQLite;
using System.Text;
using System.Threading.Tasks;

namespace LinqToDbEf
{
    public class DbEfDataContext : DbContext, IDataModel
    {
        public DbSet<Institute> Institutes { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Student> Students { get; set; }

        IQueryable<Institute> IDataModel.Institutes => Institutes;
        IQueryable<Department> IDataModel.Departments => Departments;
        IQueryable<Student> IDataModel.Students => Students;

        public DbEfDataContext(string connectionString) : base(connectionString)
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Institute>().ToTable("Institutes");
            modelBuilder.Entity<Institute>().HasKey(w => w.Id);
            modelBuilder.Entity<Institute>().Property(w => w.Name).HasColumnName("Name");

            modelBuilder.Entity<Department>().ToTable("Departments");
            modelBuilder.Entity<Department>().HasKey(w => w.Id);
            modelBuilder.Entity<Department>().Property(w => w.Name).HasColumnName("Name");
            modelBuilder.Entity<Department>().Property(w => w.BachelorProgram).HasColumnName("BachelorProgram");
            modelBuilder.Entity<Department>().Property(w => w.MasterProgram).HasColumnName("MasterProgram");
            modelBuilder.Entity<Department>().Property(w => w.PhDProgram).HasColumnName("PhDProgram");
            modelBuilder.Entity<Department>().Property(w => w.InstituteId).HasColumnName("InstituteId");

            modelBuilder.Entity<Student>().ToTable("Students");
            modelBuilder.Entity<Student>().HasKey(w => w.Id);
            modelBuilder.Entity<Student>().Property(w => w.FirstName).HasColumnName("FirstName");
            modelBuilder.Entity<Student>().Property(w => w.LastName).HasColumnName("LastName");
            modelBuilder.Entity<Student>().Property(w => w.Age).HasColumnName("Age");
            modelBuilder.Entity<Student>().Property(w => w.Program).HasColumnName("Program");
            modelBuilder.Entity<Student>().Property(w => w.YearOfStudy).HasColumnName("YearOfStudy");
            modelBuilder.Entity<Student>().Property(w => w.DepartmentId).HasColumnName("DepartmentId");
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            using (var dbContext = new DbEfDataContext("connectionString"))
            {
                dbContext.ShowOutput();
            }
        }
    }
}
