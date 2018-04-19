using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqExampleCore
{
    public class Institute
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return $"| {Id,2} | {Name,50} |";
        }
    }

    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool BachelorProgram { get; set; }
        public bool MasterProgram { get; set; }
        public bool PhDProgram { get; set; }
        public int InstituteId { get; set; }

        public override string ToString()
        {
            return $"| {Id,2} | {Name,50} | {BachelorProgram,15} | {MasterProgram,13} | {PhDProgram,10} | {InstituteId,11} |";
        }
    }

    public class Student
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Program { get; set; }
        public int YearOfStudy { get; set; }
        public int DepartmentId { get; set; }

        public override string ToString()
        {
            return $"| {Id,2} | {FirstName,10} | {LastName,15} | {Age,3} | {Program,10} | {YearOfStudy,11} | {DepartmentId,12} |";
        }
    }

    public interface IDataModel
    {
        IQueryable<Institute> Institutes { get; }
        IQueryable<Department> Departments { get; }
        IQueryable<Student> Students { get; }
    }

    public static class DataRetriever
    {
        // Where
        public static IQueryable<Department> GetDepartmentsWithBachelorProgram(this IQueryable<Department> departments)
        {
            return departments.Where(d => d.BachelorProgram);
        }

        public static int GetAmountOfStudentsInDepartment(this IQueryable<Student> students, int departmentId)
        {
            return students.Where(s => s.DepartmentId == departmentId).Count();
        }

        // Take
        public static IQueryable<Student> GetYoungestStudents(this IQueryable<Student> students)
        {
            return students.OrderBy(s => s.Age).Take(5);
        }
        
        // Skip while
        public static IQueryable<Student> GetAdultStudents(this IQueryable<Student> students)
        {
            return students.OrderBy(s => s.Age).SkipWhile(s => s.Age < 21);
        }
    
        // Single
        public static string GetDepartmentNameById(this IQueryable<Department> departments, int id)
        {
            return departments.Single(d => d.Id == id).Name;
        }

        public static string GetInstituteNameById(this IQueryable<Institute> institutes, int id)
        {
            return institutes.Single(d => d.Id == id).Name;
        }

        // Min
        public static int GetMinStudentAge(this IQueryable<Student> students)
        {
            return students.Min(s => s.Age);
        }

        // Max
        public static int GetMaxStudentAge(this IQueryable<Student> students)
        {
            return students.Max(s => s.Age);
        }

        // Average
        public static double GetAverageStudentAge(this IQueryable<Student> students)
        {
            return students.Average(s => s.Age);
        }
        
        // Distinct, Select
        public static IQueryable<string> GetPrograms(this IQueryable<Student> students)
        {
            return students.Select(s => s.Program).Distinct();
        }

        // GroupBy
        public static IQueryable<IGrouping<string, Student>> GroupStudentsByProgram(this IQueryable<Student> students)
        {
            return students.GroupBy(s=>s.Program);
        }

        // Any
        public static bool IsAnyDepartmentsInInstituteHasPhDProgram(this IQueryable<Department> departments, int instituteId)
        {
            return departments.Any(d => d.InstituteId == instituteId && d.PhDProgram == true);
        }

        // All
        public static bool IsAllDepartmentsInInstituteHasMasterProgram(this IQueryable<Department> departments, int instituteId)
        {
            return departments.All(d => d.InstituteId == instituteId && d.MasterProgram == true);
        }


        public static void ShowStudents(IQueryable<Student> students)
        {
            Console.WriteLine("| {0,2} | {1,10} | {2,15} | {3,3} | {4,10} | {5,11} | {6,12} |", "Id", "FirstName", "LastName", "Age", "Program", "YearOfStudy", "DepartmentId");
            foreach (var s in students)
                Console.WriteLine(s.ToString());
        }

        public static void ShowOutput(this IDataModel dataModel)
        {
            IQueryable<Institute> institutes = dataModel.Institutes;
            IQueryable<Department> departments = dataModel.Departments;
            IQueryable<Student> students = dataModel.Students;
            
            // Where
            IQueryable<Department> departmentWithBachelorProgram = departments.GetDepartmentsWithBachelorProgram();
            Console.WriteLine("Departments with bachelor program:");
            foreach (var d in departmentWithBachelorProgram)
                Console.WriteLine("- " + d.Name);

            // Take
            IQueryable<Student> youngestStudents = students.GetYoungestStudents();
            Console.WriteLine("\nYoungest students:");
            ShowStudents(youngestStudents);

            // SkipWhile
            IQueryable<Student> adultStudents = students.GetAdultStudents();
            Console.WriteLine("\nAdult students:");
            ShowStudents(adultStudents);

            // Union
            Console.WriteLine("\nAll students:");
            IQueryable<Student> union = youngestStudents.Union(adultStudents);
            ShowStudents(union);

            // Single, Where
            int amount = students.GetAmountOfStudentsInDepartment(1);
            string depName = departments.GetDepartmentNameById(1);
            Console.WriteLine("\nAmount of students in department {0}: {1}", depName, amount);

            // Min
            Console.WriteLine("\nMinimum student age: " + students.GetMinStudentAge());

            // Max
            Console.WriteLine("\nMaximum student age: " + students.GetMaxStudentAge());

            // Average
            Console.WriteLine("\nAverage student age: " + Math.Round(students.GetAverageStudentAge(), 1));

            // Distinct, Select
            Console.WriteLine("\nAvailable programs: ");
            IQueryable<string> programs = students.GetPrograms();
            foreach (string s in programs)
                Console.WriteLine("- " + s);

            // GroupBy
            Console.WriteLine("\nGroup by: ");
            var groupedStudents = dataModel.Students.GroupStudentsByProgram().ToList();
            foreach (var x in groupedStudents)
            {
                Console.WriteLine(x.Key);
                ShowStudents(x.AsQueryable());
            }

            // Any, All
            string instName = institutes.GetInstituteNameById(1);
            bool any = departments.IsAnyDepartmentsInInstituteHasPhDProgram(1);
            bool all = departments.IsAllDepartmentsInInstituteHasMasterProgram(1);

            Console.WriteLine("\nIs any of deparments in institute {0} has PhD program: {1}", instName, any);
            Console.WriteLine("\nIs all of deparments in institute {0} has Master program: {1}", instName, all);

            Console.ReadLine();
        }
         
    }
}
