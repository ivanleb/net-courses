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
        static void ShowCatalogToConsole(Catalog catalog)
        {
            Console.WriteLine("catalog:");
            foreach(var artist in catalog.Artists)
            {
                Console.WriteLine("\tid: "+artist.Id);
                Console.WriteLine("\tName: "+artist.Name);
                Console.WriteLine("\tage: :"+artist.age);
                Console.WriteLine("\tbirth: "+artist.birthDate);
                Console.WriteLine("\tcomments: "+artist.comments);
                Console.WriteLine("\tSongs: ");
                foreach(var song in artist.Songs)
                    Console.WriteLine("\t\t"+song.Id);
                Console.WriteLine();
            }
            foreach(var book in catalog.Books)
            {
                Console.WriteLine("\tid: "+book.Id);
                Console.WriteLine("\tname "+book.Name);
                Console.WriteLine("\tone property: "+book.OneProperty);
                Console.WriteLine("\tauthor "+book.author);
                Console.WriteLine("\tformat "+book.format);
                Console.WriteLine("\tpages: "+book.pagesAmount);
                Console.WriteLine();
            }
        }

        static void Main(string[] args)
        {
            //Artist, Book, Songs и собрать их в Catalog
            List<Song> songs = new List<Song>();
            for (char song = 'a'; song < 'k'; song++) 
            {
                songs.Add(new Song() { Id = song.ToString() });
            }
            List<Artist> artists = new List<Artist>();
            for (char artist = 'a'; artist < 'z'; artist++)
            {
                artists.Add(new Artist()
                {
                    age = (int)artist,
                    birthDate = DateTime.Now.Date,
                    comments = "Firtst" + (int)artist,
                    Id = ((int)artist).ToString(),
                    Name = "Name"+artist.ToString().ToUpper(),
                    Songs = songs
                });
            }
            
            List<Book> books = new List<Book>();
            for(int i = 0; i < 10; i++)
            {
                books.Add(new Book
                {
                    Id = i.ToString(),
                    Name = "Name" + ((char)i).ToString().ToUpper(),
                    OneProperty = "property=" + i.ToString(),
                    author = artists[i].Name,
                    format = "format type " + i.ToString(),
                    pagesAmount = i
                });
            }
            Catalog catalog = new Catalog() { Artists = artists, Books = books };

            ShowCatalogToConsole(catalog);
            Console.WriteLine("Enter any key to exit...");
            Console.Read();
        }
    }
}
