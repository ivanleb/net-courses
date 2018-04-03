using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyNameSpace;

namespace T4ExampleConsoleApp
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            #region Creating

            var fisrt = new Song {Id = "21"};
            var second = new Song {Id = "22"};
            var third = new Song {Id = "23"};
            var fourth = new Song {Id = "24"};
            var fifth = new Song {Id = "25"};
            var sixth = new Song {Id = "26"};

            var rhcp = new Artist
            {
                Age = 55,
                BirthdayDate = new DateTime(1952, 11, 1),
                Comments = "Lead singer of Red Hot Chilly Peppers band",
                Id = "1",
                Name = "Anthony Kiedis",
                Songs = new List<Song> {fisrt, second, third}
            };

            var ratm = new Artist
            {
                Age = 48,
                BirthdayDate = new DateTime(1970, 1, 12),
                Comments = "Lead singer of Rage Against the Machine band",
                Id = "2",
                Name = "Zak de la Rocha",
                Songs = new List<Song> {fourth, fifth}
            };

            var splean = new Artist
            {
                Age = 48,
                BirthdayDate = new DateTime(1969, 07, 15),
                Comments = "Lead singer and leader of Russian band Splean",
                Id = "3",
                Name = "Vasilyev Alexander Georgievich",
                Songs = new List<Song> {sixth}
            };

            var warAndPeace = new Book
            {
                Author = "Leo Tolstoy",
                Format = "Paper",
                Id = "4",
                Name = "War and Peace",
                OneProperty = "8 out of 10",
                PagesAmount = 1225
            };

            var deadSouls = new Book
            {
                Author = "Nikolai Gogol",
                Format = "Paper",
                Id = "5",
                Name = "Dead Souls",
                OneProperty = "9 out of 10",
                PagesAmount = 410
            };

            #endregion

            var catalog = new Catalog
            {
                Artists = new List<Artist> {rhcp, ratm, splean},
                Books = new List<Book> {warAndPeace, deadSouls}
            };

            foreach(var a in catalog.Artists)
            {
                Console.WriteLine($"{a.Name} -  {a.Comments}, date of birth - {a.BirthdayDate.ToShortDateString()} " +
                                  $"now {a.Age} years old, number of songs in database {a.Songs.Count()}, id in database - {a.Id}" +
                                  $"\nId of songs:");
                foreach(Song s in a.Songs)
                {
                    Console.Write($"|{s.Id}| ");
                }
                Console.WriteLine();
                
            }

            foreach(var b in catalog.Books)
            {
                Console.WriteLine($"\nBook name - {b.Name}, author - {b.Author}, format - {b.Format}, " +
                                  $"number of pages = {b.PagesAmount}, rating in database - {b.OneProperty}, id in database - {b.Id}");
            }
            Console.ReadLine();

        }
    }
}