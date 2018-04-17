using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqExampleCore
{
    public class Book
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Genre { get; set; }

        public bool IsForSale { get; set; }

        public bool IsValuable { get; set; }

        public bool IsHaveDinosaurInto { get; set; }

        public int Id { get; set; }
    }

    public class Dinosaur
    {
        public string Name { get; set; }

        public decimal Weight { get; set; }

        public decimal High { get; set; }

        public bool IsDangerous { get; set; }

        public bool IsFlying { get; set; }

        public bool IsFloating { get; set; }

        public int Id { get; set; }
    }

    public class HistoricalFigure
    {
        public string Name { get; set; }

        public bool IsHaveBookAbout { get; set; }

        public string BookAbout { get; set; }

        public bool IsDangerous { get; set; }

        public int ArmCount { get; set; }

        public bool IsReptilian { get; set; }

        public int Id { get; set; }
    }

    public interface IDataModel
    {
        IQueryable<Book> Books { get; }
        IQueryable<Dinosaur> Dinosaurs { get; }
        IQueryable<HistoricalFigure> HistoricalFigures { get; }
    }

    public static class DataRetriever
    {
        public static IQueryable<Book> GetBooksForSale(this IQueryable<Book> books)
        {
            return books.Where(w => w.IsForSale);
        }

        public static IQueryable<Dinosaur> GetFatDinosaurs(this IQueryable<Dinosaur> dinosaurs)
        {
            return dinosaurs.Where(d => d.Weight > 100);
        }

        public static IQueryable<Dinosaur> GetTallDinosaurs(this IQueryable<Dinosaur> dinosaurs)
        {
            return dinosaurs.Where(d => d.High > 20);
        }

        public static IQueryable<HistoricalFigure> GetDangerousFiguresWithTwoArms(this IQueryable<HistoricalFigure> figures)
        {
            return figures.Where(f => f.ArmCount == 2 && f.IsDangerous);
        }

        public static IQueryable<HistoricalFigure> GetFiguresWithOneArm(this IQueryable<HistoricalFigure> figures)
        {
            return figures.Where(f => f.ArmCount == 1);
        }

        public static IQueryable<HistoricalFigure> GetHistoricalFiguresWithBooks(this IQueryable<HistoricalFigure> figures)
        {
            return from b in figures where b.IsHaveBookAbout select b;
        }

        

        public static Book GetByName(this IQueryable<Book> books, string bookName)
        {
            return books.FirstOrDefault(f => f.Name == bookName);
        }

        public static decimal GetAverageCost(this IQueryable<Book> books)
        {
            return books.Average(w => w.Price);
        }

        public static void ShowTheBiggestDinosaurs(this IDataModel dataModel)
        {
            var fatDinos = dataModel.Dinosaurs.GetFatDinosaurs();
            var tallDinos = dataModel.Dinosaurs.GetTallDinosaurs();

            foreach (var dino in fatDinos)
            {
                Console.WriteLine($"fatDinos: {dino.Name} | {dino.Weight} | {dino.High} ");
            }
            foreach (var dino in tallDinos)
            {
                Console.WriteLine($"tallDinos: {dino.Name} | {dino.Weight} | {dino.High} ");
            }
        }

        public static void ShowOneArmedHistoricalFigures(this IDataModel dataModel)
        {
            var  figures = dataModel.HistoricalFigures.GetFiguresWithOneArm();
            foreach (var figure in figures)
            {
                Console.WriteLine($"oneArmHistoricalFigures: {figure.Name} | Is dangerous - {figure.IsDangerous} | Reptilian - {figure.IsReptilian} ");
            }
        }

        public static void ShowOutput(this IDataModel dataModel)
        {
            var booksForSale = dataModel.Books.GetBooksForSale().ToArray();

            foreach (var book in booksForSale)
            {
                Console.WriteLine($"booksForSale: {book.Name} | {book.Price} | {book.Genre}");
            }

            var bookByName = dataModel.Books.GetByName("Two");

            Console.WriteLine($"booksForSale: {bookByName.Name} | {bookByName.Price} | {bookByName.Genre}");

            var averageCost = dataModel.Books.GetAverageCost();

            Console.WriteLine($"averageCost: {averageCost}");

            Console.ReadLine();
        }
         
    }
}
