using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneToManyApp
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int CurrentGradeId { get; set; }
        public Grade CurrentGrade { get; set; }
    }

    public class Grade
    {
        public int GradeId { get; set; }
        public string GradeName { get; set; }
        public string Section { get; set; }

        public virtual ICollection<Student> Students { get; set; }
    }

    public class OneToManyDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Grade> Grades { get; set; }

        public OneToManyDbContext() 
            : base("Data Source=.;Initial Catalog=OneToMany2;Integrated Security=True")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // configures one-to-many relationship
            modelBuilder.Entity<Student>()
                .HasRequired<Grade>(s => s.CurrentGrade)
                .WithMany(g => g.Students)
                .HasForeignKey<int>(s => s.CurrentGradeId);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //using (var db = new OneToManyDbContext())
            //{
            //    var student = new Student()
            //    {
            //        Name = "Student A"
            //    };

            //    student.CurrentGrade = new Grade()
            //    {
            //        Section = "Grade A"
            //    };

            //    db.Students.Add(student);

            //    db.SaveChanges();
            //}

            using (var db = new OneToManyDbContext())
            {
                {
                    //db.Configuration.LazyLoadingEnabled = true;

                    //db.Configuration.LazyLoadingEnabled = false;

                    var firstGrade = db.Grades.First();

                    //db.Entry(firstGrade).Collection(p => p.Students).Load();

                    var amount = firstGrade.Students.Count;
                }

            }
        }
    }
}
