using DataFile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace T4ExampleConsoleApp
{
    class Program
    {
        static void DisplaySong(Song song)
        {
            WriteLine(song.Id);
        }

        static void DisplayArtist(Artist artist)
        {
            WriteLine("ID: " + artist.Id);
            WriteLine("Name: " + artist.Name);
            WriteLine("Age: " + artist.age);
            WriteLine("Birthday: " + artist.birthDate);
            WriteLine("Comment: " + artist.comments);

            WriteLine("SONGS: ");

            foreach (Song song in artist.Songs)
                DisplaySong(song);
        }

        static void DisplayBook(Book book)
        {
            WriteLine("ID: " + book.Id);
            WriteLine("Name: " + book.Name);
            WriteLine("Author: " + book.author);
            WriteLine("Format: " + book.format);
            WriteLine("Page amount: " + book.pagesAmount);
            WriteLine("Property: " + book.OneProperty);
        }

        static void DisplayCatalog(Catalog catalog)
        {
            foreach (Artist artist in catalog.Artists)
            {
                WriteLine();
                DisplayArtist(artist);
            }

            foreach (Book book in catalog.Books)
            {
                WriteLine();
                DisplayBook(book);
            }
        }

        static void Main(string[] args)
        {
            Random rand = new Random();
            List<Artist> artists = new List<Artist>();

            List<Song> songs = new List<Song>();

            songs.Add(new Song() { Id = "Last time" });
            songs.Add(new Song() { Id = "Summer time" });

            List<Song> songs1 = new List<Song>();

            songs1.Add(new Song() { Id = "Four seasons" });
            songs1.Add(new Song() { Id = "Mistake" });

            List<Song> songs2 = new List<Song>();

            songs2.Add(new Song() { Id = "Unknown" });
            songs2.Add(new Song() { Id = "Top top" });
            songs2.Add(new Song() { Id = "QWERTY" });

            artists.Add(new Artist() { age = 20, birthDate = new DateTime(1890, 7, 25), comments = "Bestseller", Id = rand.Next().ToString(), Name = "Alex K", Songs = songs });
            artists.Add(new Artist() { age = 17, birthDate = new DateTime(1678, 2, 1), comments = "Sale 50%", Id = rand.Next().ToString(), Name = "Nikolay G", Songs = songs1 });
            artists.Add(new Artist() { age = 18, birthDate = new DateTime(1759, 4, 18), comments = "Rare", Id = rand.Next().ToString(), Name = "Boris F", Songs = songs2 });

            List<Book> books = new List<Book>();

            books.Add(new Book()
            {
                author = "Nikolay G",
                format = "paper",
                Id = rand.Next().ToString(),
                Name = "Philosophy of C# developer",
                OneProperty = "Doesn't exist",
                pagesAmount = 0
            });

            books.Add(new Book()
            {
                author = "Pavel M",
                format = "paper",
                Id = rand.Next().ToString(),
                Name = "How to be creative while you're doing your homework",
                OneProperty = "Science",
                pagesAmount = 12000
            });

            books.Add(new Book()
            {
                author = "Incognito",
                format = "electronic",
                Id = rand.Next().ToString(),
                Name = "How to spam 200 letters on Git Hub",
                OneProperty = "True life",
                pagesAmount = 1
            });

            Catalog catalog = new Catalog() { Artists = artists, Books = books };

            DisplayCatalog(catalog);

            ReadKey();
        }
    }
}
