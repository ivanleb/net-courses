using System;
using System.Linq;

namespace LinqTo.Core
{
    public interface IDataModel
    {
        IQueryable<Project> Projects { get; }
    }
        
    public static class DataRetriever
    {
        public static Project GetProjectByName(this IQueryable<Project> projects, string projectName)
        {
            return projects.FirstOrDefault(p => p.ProjectName.Equals(projectName));
        }

        public static decimal GetAverageCostToProjectBalance(this IQueryable<Project> projects)
        {
            return projects.Average(p => p.ProjectBalance);
        }


        public static IQueryable<Transaction> GetAllTransacionsOrderedByAmount(
            this IQueryable<Transaction> transactions)
        {
            return transactions.OrderBy(t => t.Amount);
        }

        public static decimal GetMaxCategoryCost(this IQueryable<Project> projects)
        {
            return projects.SelectMany(category => category.Categories)
                .Max(t => t.Transactions)
                .Sum(w => w.Amount);
        }

        public static IQueryable<Project> GetProjectsById(this IQueryable<Project> projects, string projectId)
        {
            return projects.Where(project => project.ProjectId.Equals(projectId));
        }

        public static IQueryable<Tuple<string, int>> GetPrpojectsGroupedByName(
            this IQueryable<Project> projects)
        {
            return projects.GroupBy(p => p.ProjectName)
                .Select(g => Tuple.Create(g.Key, g.Count()));
        }

        public static bool IsAnyTransactionsCheaper1000(this IQueryable<Project> projects)
        {
            return projects.SelectMany(project => project.Categories)
                .SelectMany(category => category.Transactions)
                .Any(transaction => transaction.Amount > 1000);                      
        }

        public static void ProjectsInfo(this IQueryable<Project> projects)
        {
            foreach (var project in projects)
            {
                Console.WriteLine($"Project: {project.ProjectId} | {project.ProjectName} |  {project.ProjectBalance}");

                foreach (var category in project.Categories)
                {
                    Console.WriteLine($"\tCategory: {category.CategoryId} | {category.CategoryName}");
                    
                    foreach (var transaction in category.Transactions)
                    {
                        Console.WriteLine(
                            $"\t\tCategory: {transaction.TransactionId} | {transaction.Amount} | {transaction.Comment}");
                    }
                }
            }

            var projectByName = projects.GetProjectByName(projects.First().ProjectId);

            Console.WriteLine(
                $"projectByName: {projectByName.ProjectId} | {projectByName.ProjectName} | {projectByName.ProjectBalance}");

            var maxCategoryCost = projects.GetMaxCategoryCost();
            
            Console.WriteLine($"maxCategoryCost = {maxCategoryCost}");
        }
        
        public static void ShowOutput(this IDataModel dataModel)
        {
            ProjectsInfo(dataModel.Projects);
        }
    }

}