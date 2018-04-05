using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataFile;

namespace T4ExampleConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Song song1 = new Song();
            Song song2 = new Song();
            Song song3 = new Song();
            Song song4 = new Song();
            song1.Id = "1";
            song2.Id = "2";
            song3.Id = "3";
            song4.Id = "4";

            Artist artist1 = new Artist();
            artist1.Id = "1";
            artist1.Name = "Amy Winehouse";
            artist1.Songs = new List<Song> { song1, song2};
            artist1.age = 27;
            artist1.birthdayDate = new DateTime(1983, 9, 14);
            artist1.comments = "Very beautiful";

            Artist artist2 = new Artist();
            artist2.Id = "2";
            artist2.Name = "Alla Pugachva";
            artist2.Songs = new List<Song> { song3, song4};
            artist2.age = 30;
            artist2.birthdayDate = new DateTime(1945,12, 2);
            artist2.comments = "I like old songs";

            Book book1 = new Book();
            book1.Id = "1";
            book1.author = "Pushkin";
            book1.Name = "Sanya";
            book1.format = "paper";
            book1.OneProperty = "1";
            book1.pagesAmount = 12900;

            Book book2 = new Book();
            book2.Id = "2";
            book2.author = "Dostoevsky";
            book2.Name = "Fedor";
            book2.format = "ebook";
            book2.OneProperty = "2";
            book2.pagesAmount = 23891;

            Catalog catalog = new Catalog();
            catalog.Artists = new List<Artist> { artist1, artist2 };
            catalog.Books = new List<Book> { book1, book2 };


            Console.Write("\n\nArtist:");
            foreach (var artist in catalog.Artists)
            {
                Console.Write($"\nID: {artist.Id}\tName: {artist.Name}\tAge: {artist.age}\t" +
                                $" Birth Date: {artist.birthdayDate.ToShortDateString()}\nComments: {artist.comments}\tSongs:\t");
                foreach (var song in artist.Songs)
                {
                    Console.Write("\t" + song.Id);
                }
                Console.WriteLine();
            }

            Console.Write("\n\nBooks:");
            foreach (var book in catalog.Books)
            {
                Console.Write($"\nID: {book.Id}\tAuthor: {book.author}\tName: {book.Name}\n" +
                             $"Format: {book.format}\tOneProperty: {book.OneProperty}\tPagesAmount: {book.pagesAmount}\n");
            }

            Console.ReadKey();
        }
    }
}
