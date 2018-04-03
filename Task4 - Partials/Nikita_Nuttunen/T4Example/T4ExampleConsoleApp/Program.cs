using DataFile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace T4ExampleConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var song1 = new Song();
            var song2 = new Song();
            var song3 = new Song();
            var song4 = new Song();
            var song5 = new Song();
            song1.Id = "1";
            song2.Id = "2";
            song3.Id = "3";
            song4.Id = "4";
            song5.Id = "5";

            var artistSmith = new Artist();
            artistSmith.Id = "1";
            artistSmith.Name = "John Smith";
            artistSmith.Songs = new List<Song> { song1, song3 };
            artistSmith.Age = 25;
            artistSmith.BirthDate = new DateTime(1993, 1, 26);
            artistSmith.Comments = "Professional artist";

            var artistGreen = new Artist();
            artistGreen.Id = "2";
            artistGreen.Name = "Peter Green";
            artistGreen.Songs = new List<Song> { song2, song4, song5 };
            artistGreen.Age = 30;
            artistGreen.BirthDate = new DateTime(1987, 7, 7);
            artistGreen.Comments = "Not the best artist";

            var book1984 = new Book();
            book1984.Id = "1";
            book1984.Author = "George Orwell";
            book1984.Name = "1984";
            book1984.Format = "electronic";
            book1984.OneProperty = "small";
            book1984.PagesAmount = 320;

            var bookCatcher = new Book();
            bookCatcher.Id = "2";
            bookCatcher.Author = "J. D. Salinger";
            bookCatcher.Name = "The Catcher in the rye";
            bookCatcher.Format = "paper";
            bookCatcher.OneProperty = "small";
            bookCatcher.PagesAmount = 224;

            var catalog = new Catalog();
            catalog.Songs = new List<Song> { song1, song2, song3, song4, song5 };
            catalog.Artists = new List<Artist> { artistSmith, artistGreen };
            catalog.Books = new List<Book> { book1984, bookCatcher };

            ShowCatalog(catalog);
            Console.ReadKey();
        }

        private static void ShowCatalog(Catalog catalog)
        {
            Console.Write("Songs in catalog:");
            foreach (var song in catalog.Songs)
            {
                Console.Write(" " + song.Id);
            }

            Console.Write("\n\nArtists in catalog:");
            foreach (var artist in catalog.Artists)
            {
                Console.Write($"\nID: {artist.Id}, Name: {artist.Name}, Age: {artist.Age}, Birth Date: {artist.BirthDate.ToShortDateString()}\n" +
                    $"Comments: {artist.Comments}, Songs:");
                foreach (var song in artist.Songs)
                {
                    Console.Write(" " + song.Id);
                }
                Console.WriteLine();
            }

            Console.Write("\n\nBooks in catalog:");
            foreach (var book in catalog.Books)
            {
                Console.Write($"\nID: {book.Id}, Author: {book.Author}, Name: {book.Name}\n" +
                    $"Format: {book.Format}, OneProperty: {book.OneProperty}, PagesAmount: {book.PagesAmount}\n");
            }
        }
    }
}
