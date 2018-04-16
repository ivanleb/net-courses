using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqCore
{
    public enum Side { unknown, right, left }
    public static class SideExtentions
    {
        public static Side CastToSide(this string s)
        {
            switch (s.ToLower())
            {
                case "left":
                    return Side.left;
                case "right":
                    return Side.right;
                default:
                    return Side.unknown;
            }
        }
        public static string ToString(this Side side)
        {
            switch (side)
            {
                case Side.left:
                    return "left";
                case Side.right:
                    return "right";
                default:
                    return "unknown";
            }
        }

    }
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime BirthdayDate { get; set; }
        public int Age { get { return DateTime.Now.Date.Year - this.BirthdayDate.Year - 1 + (DateTime.Now.DayOfYear >= this.BirthdayDate.DayOfYear ? 1 : 0); } }
        public Side StrongestHand { get; set; }
        public string Citizenship { get; set; }
        public decimal Salary { get; set; }
    }
    public class Team
    {
        public int Id { get; set; }
        public DateTime FoundationDate { get; set; }
        public string Name { get; set; }
        public string HeadCoach { get; set; }
        public string Country { get; set; }
    }
    public class Stadium
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }
        public string City { get; set; }
    }
    public interface IDataModel
    {
        IQueryable<Player> Players { get; }
        IQueryable<Team> Teams { get; }
        IQueryable<Stadium> Stadiums { get; }
    }
    public static class DataRetriever
    {
        private static void printPlayerInfo(IQueryable<Player> players)
        {
            foreach (var player in players)
            {
                Console.WriteLine($"{player.Id}: {player.Name} | {player.StrongestHand.ToString()} | {player.Citizenship} | {player.Salary} | {player.BirthdayDate.ToShortDateString()} | {player.Age}");
            }
        }

        private static void printTeamInfo(IQueryable<Team> teams)
        {
            foreach (var team in teams)
            {
                Console.WriteLine($"{team.Id}: {team.Name} | {team.HeadCoach} | {team.Country} | {team.FoundationDate.Year}");
            }
        }

        private static void printStadiumInfo(IQueryable<Stadium> stadiums)
        {
            foreach (var stadium in stadiums)
            {
                Console.WriteLine($"{stadium.Id}: {stadium.Name} | {stadium.City} | {stadium.Capacity}");
            }
        }

        public static void ShowOutput(this IDataModel dataModel)
        {
            var allPlayers = dataModel.Players;
            Console.WriteLine(string.Format("All registered players:"));
            printPlayerInfo(allPlayers);
            Console.WriteLine("------------------------------------");
            var avarageSalaries = dataModel.Players.GroupBy(p=>p.Citizenship).Select(g=>new { Salary = g.Average(p => p.Salary), Country = g.Key });
            foreach (var salary in avarageSalaries)
            {
                Console.WriteLine(string.Format("Avarage salary of the players from {0} is {1}", salary.Country, salary.Salary));
            }
            Console.WriteLine("------------------------------------");
            Console.WriteLine("All teams in the order of the foundation date:");
            var teamsOrderedByCountries = dataModel.Teams.OrderBy(t=>t.FoundationDate);
            printTeamInfo(teamsOrderedByCountries);
            Console.WriteLine("------------------------------------");
            Console.WriteLine("The bigest stadiums in the city:");
            var theBigestStadiumInTheCity = dataModel.Stadiums.GroupBy(s => s.City).Select(g => g.FirstOrDefault(s => s.Capacity == g.Max(st => st.Capacity)));//new { City = g.Key, MaxCapacity = g.Max(s => s.Capacity), Name = g.First(s => s.Capacity == g.Max(st => st.Capacity)).Name });
            printStadiumInfo(theBigestStadiumInTheCity);
            Console.WriteLine("------------------------------------");
            Console.ReadLine();

        }
    }
}