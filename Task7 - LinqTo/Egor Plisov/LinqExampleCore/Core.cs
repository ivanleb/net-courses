using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LinqExampleCore
{
    public class Guest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public List<int> Visits { get; set; }
    }

    public class Visit
    {
        public int Id { get; set; }
        public int GuestId { get; set; }
        public int DaysNumber { get; set; }
        public DateTime VisitDate { get; set; }
        public decimal Total { get; set; }
    }

    public class Room
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public decimal PricePerDay { get; set; }

    }

    public interface IDataModel
    {
        IQueryable<Room> Rooms { get; }
        IQueryable<Visit> Visits { get;}
        IQueryable<Guest> Guests { get; }
    }

    public static class DataRetriever
    {
        public static IQueryable<Room> GetRoomPriceDayLow2000(this IQueryable<Room> rooms)
        {
            return rooms.Where(p => p.PricePerDay <= 2000);
        }

        public static IQueryable<string> GetRoomCategory(this IQueryable<Room> rooms)
        {
            return rooms.Select(x => x.Category);
        }

        public static decimal GetAveragePrice(this IQueryable<Room> rooms)
        {
            return rooms.Average(w => w.PricePerDay);
        }

        public static IQueryable<Visit> GetLastFiveVisits(this IQueryable<Visit> visits)
        {
            return visits.OrderByDescending(x => x.VisitDate).Take(5);
        }

        public static IQueryable<int> GetOrdersFromCity(this IQueryable<Guest> guests, string city)
        {
            return guests.Where(c => c.City == city).SelectMany(x => x.Visits);
        }

        public static IQueryable<Room> GetAllButMostExpensive1(this IQueryable<Room> rooms)
        {
            return rooms.OrderByDescending(p => p.PricePerDay).Skip(1);
        }
       
        public static IQueryable<string> GetAdresses(this IQueryable<Guest> guests)
        {
            return guests.Select(c => c.City).
                    Concat(guests.Select(c => c.Country)).
                    Distinct();
        }

        public static IQueryable<IGrouping<string, Room>> GetGroupedRooms(this IQueryable<Room> rooms)
        {
            return rooms.GroupBy(p => p.Category);
        }

        public static decimal GetMinCost(this IQueryable<Room> rooms)
        {
            return rooms.Min(x => x.PricePerDay);
        }

        public static decimal GetMaxCost(this IQueryable<Room> rooms)
        {
            return rooms.Max(x => x.PricePerDay);
        }

        public static decimal GetCostOfAllCustomerVisits(this IQueryable<Visit> visits, int questId)
        {
            return visits.Where(x => x.GuestId == questId).Select(x => x.Total).Sum();
        }

        public static bool IsQuestsPlacedVisits(this IQueryable<Visit> visit, int questId)
        {
            return visit.Any(x => x.GuestId == questId);
        }



        public static void ShowData(this IDataModel dataModel)
        {
            Console.WriteLine("Rooms:");
            dataModel.Rooms.ShowRooms();
            Console.WriteLine("Visits:");
            dataModel.Visits.ShowVisits();
            Console.WriteLine("Quests:");
            dataModel.Guests.ShowGuests();
            
        }

        public static void ShowGuests(this IEnumerable<Guest> guests)
        {
            foreach (var guest in guests)
            {
                Console.Write("{0, -5}|", guest.Id);
                Console.Write("{0, -15}|", guest.Name);
                Console.Write("{0, -30}|", guest.Address);
                Console.Write("{0, -15}|", guest.Country);
                Console.WriteLine();
            }
        }

        public static void ShowVisits(this IEnumerable<Visit> visits)
        {
            foreach (var visit in visits)
            {
                Console.Write("{0, -5}|", visit.Id);
                Console.Write("{0, -5}|", visit.GuestId);
                Console.Write("{0, -15}|", visit.VisitDate);
                Console.Write("{0, -15}|", visit.DaysNumber);
                Console.Write("{0, -10}|", visit.Total);
                Console.WriteLine();
            }
        }

        public static void ShowRooms(this IEnumerable<Room> rooms)
        {
            foreach (var room in rooms)
            {
                Console.Write("{0, -20}|", room.Id);
                Console.Write("{0, -20}|", room.Category);
                Console.Write("{0, -20}|", room.PricePerDay);
                Console.WriteLine();
            }
        }


        public static void ShowOperations(this IDataModel dataModel)
        {
            var adresses = dataModel.Guests.GetAdresses().ToList();
            ShowTitle("Adresses:");
            foreach (var adress in adresses)
            {
                Console.WriteLine(adress);
            }
            var id = 1;
            var cost = dataModel.Visits.GetCostOfAllCustomerVisits(id);
            ShowTitle($"Total cost of guest {id} visits is {cost}");
            var latestFiveOrders = dataModel.Visits.GetLastFiveVisits().ToList();
            ShowTitle("Latest five orders:");
            latestFiveOrders?.ShowVisits();
            var isCustomerPlacedOrders = dataModel.Visits.IsQuestsPlacedVisits(id);
            ShowTitle($"Guest {id} plased orders is {isCustomerPlacedOrders}");
            var allButMostExpensive1 = dataModel.Rooms.GetAllButMostExpensive1().ToList();
            allButMostExpensive1?.ShowRooms();
            var averageCost = dataModel.Rooms.GetAveragePrice();
            ShowTitle($"Average rooms cost is ${averageCost}");
            var minCost = dataModel.Rooms.GetMinCost();
            ShowTitle($"Min rooms cost is ${minCost}");
            var maxCost = dataModel.Rooms.GetMaxCost();
            ShowTitle($"Max rooms cost is ${maxCost}");
            var roomsCategoryes = dataModel.Rooms.GetRoomCategory().ToList();
            ShowTitle("Rooms categoryes:");
            foreach (var roomCategory in roomsCategoryes)
            {
                Console.WriteLine(roomCategory);
            }
            var productsWithLowPrice = dataModel.Rooms.GetRoomPriceDayLow2000().ToList();
            ShowTitle("Rooms with price low 2000:");
            productsWithLowPrice.ShowRooms();
        }

        public static void ShowTitle(string title)
        {
            Console.WriteLine(title);
        }
    }
}
