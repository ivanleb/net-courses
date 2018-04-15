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

        public int Id { get; set; }
    }

    public interface IDataModel
    {
        IQueryable<Book> Books { get; }
    }

    public static class DataRetriever
    {
        public static IQueryable<Book> GetBooksForSale(this IQueryable<Book> books)
        {
            return books.Where(w => w.IsForSale);
        }

        public static Book GetByName(this IQueryable<Book> books, string bookName)
        {
            return books.FirstOrDefault(f => f.Name == bookName);
        }

        public static decimal GetAverageCost(this IQueryable<Book> books)
        {
            return books.Average(w => w.Price);
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
