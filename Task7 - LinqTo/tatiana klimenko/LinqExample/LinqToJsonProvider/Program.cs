using LinqExampleCore;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqToJsonProvider
{
    class JsonLinqDataModel : IDataModel
    {
        private readonly JObject dataProvider;

        public JsonLinqDataModel(string pathToDataFile)
        {
            this.dataProvider = JObject.Parse(System.IO.File.ReadAllText(pathToDataFile));
        }

        public IQueryable<Institute> Institutes
        {
            get
            {
                return dataProvider["Institutes"].Select(s => new Institute()
                {
                    Id = s["Id"].Value<int>(),
                    Name = s["Name"].Value<string>()
                }).AsQueryable();
            }
        }

        public IQueryable<Department> Departments
        {
            get
            {
                return dataProvider["Departments"].Select(s => new Department()
                {
                    Id = s["Id"].Value<int>(),
                    Name = s["Name"].Value<string>(),
                    BachelorProgram = s["BachelorProgram"].Value<bool>(),
                    MasterProgram = s["MasterProgram"].Value<bool>(),
                    PhDProgram = s["PhDProgram"].Value<bool>(),
                    InstituteId = s["InstituteId"].Value<int>()
                }).AsQueryable();
            }
        }

        public IQueryable<Student> Students
        {
            get
            {
                return dataProvider["Students"].Select(s => new Student()
                {
                    Id = s["ID"].Value<int>(),
                    FirstName = s["FirstName"].Value<string>(),
                    LastName = s["LastName"].Value<string>(),
                    Age = s["Age"].Value<int>(),
                    Program = s["Program"].Value<string>(),
                    YearOfStudy = s["YearOfStudy"].Value<int>(),
                    DepartmentId = s["DepartmentId"].Value<int>(),
                }).AsQueryable();
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            IDataModel dataModel = new JsonLinqDataModel(".\\data.json");
            
            dataModel.ShowOutput();
        }
    }
}
