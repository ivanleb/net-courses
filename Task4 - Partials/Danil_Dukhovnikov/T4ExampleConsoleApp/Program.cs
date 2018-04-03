using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyNameSpace;

namespace T4ExampleConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
#region Creating
            Song fisrt = new Song();
            fisrt.Id = "21";
            Song second = new Song();
            second.Id = "22";
            Song third = new Song();
            third.Id = "23";
            Song fourth = new Song();
            fourth.Id = "24";
            Song fifth = new Song();
            fifth.Id = "25";
            Song sixth = new Song();
            sixth.Id = "26";

            Artist rhcp = new Artist();
            rhcp.Age = 55;
            rhcp.BirthdayDate = new DateTime(1952, 11, 1);
            rhcp.Comments = "Lead singer of Red Hot Chilly Peppers band";
            rhcp.Id = "1";
            rhcp.Name = "Anthony Kiedis";
            rhcp.Songs = new List<Song> { fisrt, second, third };

            Artist ratm = new Artist();
            ratm.Age = 48;
            ratm.BirthdayDate = new DateTime(1970, 1, 12);
            ratm.Comments = "Lead singer of Rage Against the Machine band";
            ratm.Id = "2";
            ratm.Name = "Zak de la Rocha";
            ratm.Songs = new List<Song> { fourth, fifth };

            Artist splean  = new Artist();
            splean.Age = 48;
            splean.BirthdayDate = new DateTime(1969, 07, 15);
            splean.Comments = "Lead singer and leader of Russian band Splean";
            splean.Id = "3";
            splean.Name = "Vasilyev Alexander Georgievich";
            splean.Songs = new List<Song> { sixth };

            Book warAndPeace = new Book();
            warAndPeace.Author = "Leo Tolstoy";
            warAndPeace.Format = "Paper";
            warAndPeace.Id = "4";
            warAndPeace.Name = "War and Peace";
            warAndPeace.OneProperty = "8 out of 10";
            warAndPeace.PagesAmount = 1225;

            Book deadSouls = new Book();
            deadSouls.Author = "Nikolai Gogol";
            deadSouls.Format = "Paper";
            deadSouls.Id = "5";
            deadSouls.Name = "Dead Souls";
            deadSouls.OneProperty = "9 out of 10";
            deadSouls.PagesAmount = 410;
#endregion

            Catalog catalog = new Catalog();
            catalog.Artists = new List<Artist> { rhcp, ratm, splean };
            catalog.Books = new List<Book> { warAndPeace, deadSouls };

            foreach(Artist a in catalog.Artists)
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

            foreach(Book b in catalog.Books)
            {
                Console.WriteLine($"\nBook name - {b.Name}, author - {b.Author}, format - {b.Format}, " +
                    $"number of pages = {b.PagesAmount}, rating in database - {b.OneProperty}, id in database - {b.Id}");
            }
            Console.ReadLine();

        }
    }
}
