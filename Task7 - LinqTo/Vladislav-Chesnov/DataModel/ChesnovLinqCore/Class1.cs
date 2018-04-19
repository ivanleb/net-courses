using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChesnovLinqCore
{
    public class Game
    {
        public string Name { get; set; }
        public string Genre { get; set; }
        public string Platform { get; set; }
        public bool CollectorsEdition { get; set; }
        public decimal Price { get; set; }
        public decimal Rating { get; set; }
        public int Quantity { get; set; }
        public int MinimumAge { get; set; }
        public int DevelopingCompanyId { get; set; }
        public int NumberOfTimesBuyed { get; set; }
        public int Id { get; set; }
    }

    public class Player
    {
        public string RealName { get; set; }
        public string Nickname { get; set; }
        public string Country { get; set; }
        public DateTime DateOfBirth { get; set; }
        public decimal Balance { get; set; }
        public bool IsActive { get; set; }
        public int Id { get; set; }
    }

    public class GameCompany
    {
        public string Name { get; set; }
        public int YearOfEstablishment { get; set; }
        public int Id { get; set; }
    }


    public interface IDataModel
    {
        IQueryable<Game> Games { get; }
        IQueryable<Player> Players { get; }
        IQueryable<GameCompany> GameCompanies { get; }
    }   

    public static class DataRetriever
    {
        #region GameMeth
        public static IQueryable<Game> GetAvaliableGames(this IQueryable<Game> games)
        {
            return games.Where(g => g.Quantity > 0);
        }

        public static IQueryable<Game> GetGameByName(this IQueryable<Game> games, string gameName)
        {
            return games.Where(g => g.Name == gameName);
        }

        public static IQueryable<Game> GetAllGamesByGenre(this IQueryable<Game> games, string genre)
        {
            return games.Where(g => g.Genre == genre);
        }

        public static IQueryable<Game> GetAllGamesForAge(this IQueryable<Game> games, int age)
        {
            return games.Where(g => g.MinimumAge <= age);
        }

        public static IQueryable<Game> GetAllGamesForPlatform(this IQueryable<Game> games, string platform)
        {
            return games.Where(g => g.Platform == platform);
        }

        public static IQueryable<Game> GetAllGamesFilteredByCost(this IQueryable<Game> games)
        {
            return games.OrderByDescending(g => g.Price);
        }

        public static IQueryable<Game> GetAllGamesFilteredByAgeRating(this IQueryable<Game> games)
        {
            return games.OrderBy(g => g.MinimumAge);
        }

        public static IQueryable<Game> GetTop3Games(this IQueryable<Game> games)
        {
            return games.OrderByDescending(g => g.Rating).Take(3);
        }

        public static bool IsThereAnyGamesForPlatform(this IQueryable<Game> games, string platform)
        {
            return games.Any(g => g.Platform == platform);
        }

        public static bool IsThereAreGamesCheaperThanX(this IQueryable<Game> games, decimal price)
        {
            return games.Any(g => g.Price <= price);
        }

        public static decimal GetMaxCost(this IQueryable<Game> games)
        {
            return games.Max(g => g.Price);
        }

        public static decimal GetMinCost(this IQueryable<Game> games)
        {
            return games.Min(g => g.Price);
        }

        public static decimal GetAverageCost(this IQueryable<Game> games)
        {
            return games.Average(g => g.Price);
        }

        public static IQueryable<IGrouping<string,Game>> GetGamesGropedByGenre(this IQueryable<Game> games)
        {
            return games.GroupBy(g => g.Genre);
        }


        #endregion

        #region PlayersMeth
        public static IQueryable<Player> GetActiveUsers(this IQueryable<Player> players)
        {
            return players.Where(p => p.IsActive);
        }

        public static IQueryable<Player> GetUsersOlderThanX(this IQueryable<Player> players, int x)
        {
            return players.Where(p => x < DateTime.Now.Year - p.DateOfBirth.Year);
        }

        public static IQueryable<Player> GetUsersFilteredByMoney(this IQueryable<Player> players)
        {
            return players.OrderByDescending(p => p.Balance);
        }

        public static IQueryable<IGrouping<string,Player>> GetPlayersGroupedByCountry(this IQueryable<Player> players)
        {
            return players.GroupBy(p => p.Country);
        }
        #endregion

        #region GameCompanyMeth

        public static IQueryable<GameCompany> GetFilteredCompaniesByYear(this IQueryable<GameCompany> gameCompanies)
        {
            return gameCompanies.OrderBy(c => c.YearOfEstablishment);
        }

        public static IQueryable<Game> GetGamesDevelopedBy(this IQueryable<Game> games, int companyId)
        {
            return games.Where(g => g.DevelopingCompanyId == companyId);
        }

        #endregion

        public static void PrintGamesInfo(this IQueryable<Game> games)
        {
            foreach(var game in games)
            {
                Console.WriteLine($"Name:{game.Name} | price: {game.Price} | " +
                $"genre: {game.Genre} | avaliable on: {game.Platform} | {game.MinimumAge}+ | " +
                $"{game.Rating} out of 10 | {game.Quantity} copies in stock | have collectors edition : {game.CollectorsEdition} | Copies sold: {game.NumberOfTimesBuyed} | game id: {game.Id}");
            }
        }

        public static void PrintPlayersInfo(this IQueryable<Player> players)
        {
            foreach(var player in players)
            {
                Console.WriteLine($"Nickname: {player.Nickname} | real name: {player.RealName} | ID: {player.Id} | from: {player.Country} | DOB: {player.DateOfBirth.Date.ToString("d")} " +
                $"({(DateTime.Now.Date - player.DateOfBirth.Date).Days / 365} years) | balance {player.Balance} | Active: {player.IsActive} |");
            }
        }

        public static void PrintCompaniesInfo(this IQueryable<GameCompany> gameCompanies)
        {
            foreach(var gameCompany in gameCompanies)
            {
                Console.WriteLine($"Name of company: {gameCompany.Name} | ID: {gameCompany.Id} | year of establishment: {gameCompany.YearOfEstablishment} |");
            }            
        }

        public static void ShowAllGames(this IDataModel dataModel)
        {            
            Console.WriteLine("Below is the list of games we can offer");
            dataModel.Games.PrintGamesInfo();
            Console.ReadLine();
        }

        public static void ShowOutput(this IDataModel dataModel)
        {
            string userInput;
            do
            {
                Console.WriteLine("Input a game name, to exit input 0");
                userInput = Console.ReadLine();
                dataModel.Games.GetGameByName(userInput).PrintGamesInfo();
            } while (userInput != "0");

            Console.WriteLine("\nShowing all RTS");
            var rts = dataModel.Games.GetAllGamesByGenre("RTS");
            rts.PrintGamesInfo();

            Console.WriteLine("\nShowing only games for 14 years or younger");
            var gamesFor14yo = dataModel.Games.GetAllGamesForAge(14);
            gamesFor14yo.PrintGamesInfo();

            Console.WriteLine("\nShowing only games for Microwave");
            var gamesForPC = dataModel.Games.GetAllGamesForPlatform("Microwave");
            gamesForPC.PrintGamesInfo();

            Console.WriteLine("\nFiltered by cost");
            var filteredByCost = dataModel.Games.GetAllGamesFilteredByCost();
            filteredByCost.PrintGamesInfo();

            Console.WriteLine("\nFiltered by age");
            var filteredByAge = dataModel.Games.GetAllGamesFilteredByAgeRating();
            filteredByCost.PrintGamesInfo();

            Console.WriteLine("\n Top3");
            var top3 = dataModel.Games.GetTop3Games();
            top3.PrintGamesInfo();

            Console.WriteLine("\nAny games on ps3?");
            Console.WriteLine(dataModel.Games.IsThereAnyGamesForPlatform("PS3"));

            Console.WriteLine("\nAny games cheaper than 1?");
            Console.WriteLine(dataModel.Games.IsThereAreGamesCheaperThanX(1));

            Console.WriteLine($"\nMax cost: {dataModel.Games.GetMaxCost()}");
            Console.WriteLine($"\nMin cost: {dataModel.Games.GetMinCost()}");
            Console.WriteLine($"\nAverage cost: {dataModel.Games.GetAverageCost()}");

            Console.WriteLine("\nGrouped by genre:");
            var grouper = dataModel.Games.GetGamesGropedByGenre().SelectMany(group=>group);
            PrintGamesInfo(grouper);

            var activeUsers = dataModel.Players.GetActiveUsers();
            Console.WriteLine("\nActive players: ");
            activeUsers.PrintPlayersInfo();

            var usersOlderThan = dataModel.Players.GetUsersOlderThanX(17);
            Console.WriteLine("\nPlayers older 17 years: ");
            usersOlderThan.PrintPlayersInfo();

            var richiestUsers = dataModel.Players.GetUsersFilteredByMoney();
            Console.WriteLine("\nPlayers filtered by balance: ");
            richiestUsers.PrintPlayersInfo();

            var playersGroupedByCountry = dataModel.Players.GetPlayersGroupedByCountry().SelectMany(group => group);
            Console.WriteLine("\nPlayers grouped by country: ");
            playersGroupedByCountry.PrintPlayersInfo();

            var companiesFilteredByYear = dataModel.GameCompanies.GetFilteredCompaniesByYear();
            Console.WriteLine("\nGame companies:");
            companiesFilteredByYear.PrintCompaniesInfo();

            var gamesDevelopedByFirst = dataModel.Games.GetGamesDevelopedBy(1);
            decimal income = 0;
            foreach(var game in gamesDevelopedByFirst)
            {
                income += game.Price * game.NumberOfTimesBuyed;
            }
            Console.WriteLine($"Income of first company = {income}");

            Console.ReadLine();
        }

        public static void ShowOutput1(this IDataModel dataModel)
        {

        }
    }
}
