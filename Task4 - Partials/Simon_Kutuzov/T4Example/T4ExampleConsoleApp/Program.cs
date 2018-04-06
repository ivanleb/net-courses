using System;
using System.Collections.Generic;
using DataFile;

namespace T4ExampleConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var SummerBreak = new Song { Id = "1" };
            var Cassini = new Song { Id = "2" };
            var DoubleHelix = new Song { Id = "3" };
            var WhoBitTheMoon = new Song { Id = "4" };
            var LivingRoom = new Song { Id = "5" };
            var SomeoneElseHat = new Song { Id = "6" };
            var SoDidWe = new Song { Id = "7" };
            var InFiction = new Song { Id = "8" };
            var TheBeginningAndTheEnd = new Song { Id = "9" };

            var ModernC = new Book
            {
                Id = "1",
                Author = "Jens Gustedt",
                Name = "Modern C",
                Format = "PDF",
                PagesAmount = 320,
                OneProperty = "A lot happend since ANSI C",
            };
            var IntroductionToAlgorithms = new Book
            {
                Id = "2",
                Author = "Thomas H. Cormen",
                Name = "Intoduction to Algorithms",
                Format = "Hardcover",
                PagesAmount = 1313,
                OneProperty = "Must read for everyone",
            };

            var SithuAye = new Artist
            {
                Id = "1",
                Name = "Sithu Aye",
                BirthdayDate = new DateTime(1990, 5, 26),
                Age = 27,
                Songs = new List<Song> { SummerBreak, Cassini, DoubleHelix },
                Comments = "Weeb trash",
            };
            var DavidMaximMicic = new Artist
            {
                Id = "2",
                Name = "David Maxim Micic",
                BirthdayDate = new DateTime(1990, 5, 5),
                Age = 27,
                Songs = new List<Song> { WhoBitTheMoon, LivingRoom, SomeoneElseHat },
                Comments = "A guitar/keyboard player, composer and producer",
            };
            var Isis = new Artist
            {
                Id = "3",
                Name = "ISIS",
                BirthdayDate = new DateTime(1997, 11, 1),
                Age = 20,
                Songs = new List<Song> { SoDidWe, InFiction, TheBeginningAndTheEnd },
                Comments = "An American post-metal band",
            };

            var catalog = new Catalog
            {
                Artists = new List<Artist> { SithuAye, DavidMaximMicic, Isis },
                Books = new List<Book> { ModernC, IntroductionToAlgorithms },
            };

            foreach (var artist in catalog.Artists)
            {
                Console.WriteLine($"Band: {artist.Name} -- {artist.Comments}");
                Console.WriteLine($"Founded: {artist.BirthdayDate.ToShortDateString()}");
                Console.WriteLine($"{artist.Age} years in music");
                Console.WriteLine($"Database id: {artist.Id}");
                Console.Write("Song ids in database: ");
                foreach (var song in artist.Songs)
                {
                    Console.Write($"{song.Id} ");
                }
                Console.Write("\n\n");
            }

            foreach (var book in catalog.Books)
            {
                Console.WriteLine($"Author: {book.Author}");
                Console.WriteLine($"Title: {book.Name}");
                Console.WriteLine($"Pages count: {book.PagesAmount}");
                Console.WriteLine($"Format: {book.Format}");
                Console.WriteLine($"Description: {book.OneProperty}");
                Console.WriteLine($"Database id: {book.Id}");
                Console.Write('\n');
            }
        }
    }
}
