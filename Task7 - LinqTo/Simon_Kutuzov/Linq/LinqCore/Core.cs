using System;
using System.Linq;

namespace LinqCore
{
    public class Guitar
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Name { get; set; }
        public string PickupConfig { get; set; }
        public string Color { get; set; }
        public int Frets { get; set; }
        public int Strings { get; set; }
        public int Price { get; set; }
        public string IncludedAccessories { get; set; }
    }

    public class Amplifier
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Name { get; set; }
        public string Nobs { get; set; }
        public string Effects { get; set; }
        public int MaxPower { get; set; }
        public int Price { get; set; }
    }

    public class Keyboard
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Name { get; set; }
        public int Keys { get; set; }
        public int Price { get; set; }
    }

    public interface IDataModel
    {
        IQueryable<Guitar> Guitars { get; }
        IQueryable<Amplifier> Amplifiers { get; }
        IQueryable<Keyboard> Keyboards { get; }
    }

    public static class DataRetriever
    {
        public static IQueryable<Guitar> GetGuitarsByBrand(this IQueryable<Guitar> guitars, string brand)
        {
            return guitars.Where(g => g.Brand == brand);
        }

        public static int GetMaxPrice(this IQueryable<Guitar> guitars)
        {
            return guitars.Min(g => g.Price);
        }

        public static double GetAvgPrice(this IQueryable<Guitar> guitars)
        {
            return guitars.Average(g => g.Price);
        }

        public static IQueryable<Guitar> GetGuitarsWithAccessories(this IQueryable<Guitar> guitars)
        {
            return guitars.Where(g => g.IncludedAccessories != null);
        }

        public static IQueryable<Tuple<string, int>> GetGuitarsGroupedByBrand(this IQueryable<Guitar> guitars)
        {
            return guitars.GroupBy(g => g.Brand)
                          .OrderBy(group => group.Key)
                          .Select(group => Tuple.Create(group.Key, group.Count()));
        }

        public static IQueryable<Amplifier> GetAllAmpsOrderedByPrice(this IQueryable<Amplifier> amplifiers)
        {
            return amplifiers.OrderBy(a => a.Price);
        }

        public static bool GetAmpsCheaperThen50(this IQueryable<Amplifier> amplifiers)
        {
            return amplifiers.Any(a => a.Price < 50);
        }

        public static bool GetFullSizeKeyboards(this IQueryable<Keyboard> keyboards)
        {
            return keyboards.Any(k => k.Keys == 88);
        }

        public static void GuitarsInfo(this IQueryable<Guitar> guitars)
        {
            Console.WriteLine("Specify a guitar brand:");
            Console.Write("> ");
            var brand = Console.ReadLine();

            Console.WriteLine($"Guitars by {brand}");
            foreach (var g in guitars.GetGuitarsByBrand(brand))
            {
                Console.WriteLine($"Model: {g.Name}");
                Console.WriteLine($"Price: {g.Price}");
                Console.Write('\n');
            }

            Console.WriteLine($"Minimum price for a guitar: {guitars.GetMaxPrice()}");
            Console.WriteLine($"Average price for a guitar: {guitars.GetAvgPrice()}");
            Console.Write('\n');

            Console.WriteLine("Guitars that come with accessories:");
            foreach (var g in guitars.GetGuitarsWithAccessories())
            {
                Console.WriteLine($"Brand: {g.Brand}");
                Console.WriteLine($"Model: {g.Name}");
                Console.WriteLine($"Accessories: {g.IncludedAccessories}");
                Console.Write('\n');
            }

            //Entity Framework has trouble working with tuples. Works with JSON provider just fine

            //Console.WriteLine("How many guitars of each brand do we have?");
            //foreach (var g in guitars.GetGuitarsGroupedByBrand())
            //{
            //    Console.WriteLine($"{g.Item1} {g.Item2}");
            //}

            //Console.Write('\n');
        }

        public static void AmpsInfo(this IQueryable<Amplifier> amps)
        {
            Console.WriteLine("All amps ordered by price:");
            foreach (var a in amps.GetAllAmpsOrderedByPrice())
            {
                Console.WriteLine($"Brand: {a.Brand}");
                Console.WriteLine($"Model: {a.Name}");
                Console.WriteLine($"Price: {a.Price}");
                Console.Write('\n');
            }

            Console.Write("Are there any amps cheaper then $50? ");
            Console.WriteLine(amps.GetAmpsCheaperThen50() ? "Yes" : "No");
        }

        public static void KeyboardsInfo(this IQueryable<Keyboard> keyboards)
        {
            Console.Write("Are there any full-size keyboards? ");
            Console.WriteLine(keyboards.GetFullSizeKeyboards() ? "Yes" : "No");
        }

        public static void ShowOutput(this IDataModel dataModel)
        {
            GuitarsInfo(dataModel.Guitars);
            AmpsInfo(dataModel.Amplifiers);
            KeyboardsInfo(dataModel.Keyboards);
        }
    }
}
