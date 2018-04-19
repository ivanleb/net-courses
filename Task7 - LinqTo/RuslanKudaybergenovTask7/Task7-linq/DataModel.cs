using System;
using System.Linq;

namespace Task7_linq
{
    public class User
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Type { get; set; }
        public int IDLocation { get; set; }
    }

    public class Location
    {
        public int ID { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
    }

    public interface IDataModel
    {
        IQueryable<User> Users { get; }
        IQueryable<Location> Locations { get; }
    }

    public static class DataModel
    {
        public static IQueryable<User> GetUsersByType(this IQueryable<User> users, string userType)
        {
            return users.Where(w => w.Type==userType);
        }

        public static IQueryable<User> GetBannedUsers(this IQueryable<User> users)
        {
            return users.Where(w => w.Type == "Banned");
        }
        public static IQueryable<User> GetAdmins(this IQueryable<User> users)
        {
            return users.Where(w => w.Type == "Admin");
        }
        public static IQueryable<User> GetGoldUsers(this IQueryable<User> users)
        {
            return users.Where(w => w.Type == "Gold");
        }

        public static void ShowOutput(this IDataModel dataModel)
        {
            var users = dataModel.Users.ToArray();
            var locations = dataModel.Locations.ToArray();

            //simple example
            foreach (var user in users)
            {
                Console.WriteLine($"ID:{user.ID}\n\tName: {user.Name} \n\tPassword :{user.Password} \n\tType: {user.Type}");
            }

            //group
            var groupedUsersByLocationID = users.GroupBy(u => u.IDLocation, u => u.Name);
            foreach (var group in groupedUsersByLocationID)
            {
                Console.ForegroundColor++;
                Console.WriteLine($"{locations.Where(l => l.ID == group.Key).ToArray()[0].City}");
                foreach (var username in group)
                    Console.WriteLine($"\t\t\t{username}");
            }

            //join
            var joinedUsersAndLocations = locations.Join(users,
                city => city.ID,
                user => user.IDLocation,
                (city, user) => new { City = city.City, User = user.Name });
            foreach(var obj in joinedUsersAndLocations)
            {
                Console.ForegroundColor++;
                Console.WriteLine($"{obj.City} has {obj.User}");
            }

            //select+order/desc
            var orderedUsers = users.Select(u => new { ID = u.ID, Name = u.Name }).OrderBy(u=>u.ID);
            Console.ForegroundColor = ConsoleColor.Black;
            Console.ForegroundColor++;
            foreach (var user in orderedUsers)
            {
                Console.WriteLine($"ID: {user.ID} : Name : {user.Name}");
            }
            var orderedUsersDesc = orderedUsers.OrderByDescending(u => u.ID);
            Console.ForegroundColor++;
            foreach (var user in orderedUsersDesc)
            {
                Console.WriteLine($"ID: {user.ID} : Name : {user.Name}");
            }
            //count
            Console.WriteLine($"Last added user: {users.Last().Name}");

            Console.ReadLine();
        }

    }
}
