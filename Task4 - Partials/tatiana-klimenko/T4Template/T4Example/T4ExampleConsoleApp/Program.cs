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
            song1.Id = "1";
            Song song2 = new Song();
            song2.Id = "2";
            Song song3 = new Song();
            song3.Id = "3";
            Song song4 = new Song();
            song4.Id = "4";

            Artist artist1 = new Artist();
            artist1.Id = "1";
            artist1.Name = "name1";
            artist1.Songs = new List<Song> { song1, song2 };
            artist1.age = 20;
            artist1.birthdayDate = new DateTime(1998, 2, 2);
            artist1.comments = "comment1";

            Artist artist2 = new Artist();
            artist2.Id = "2";
            artist2.Name = "name2";
            artist2.Songs = new List<Song> { song3, song4 };
            artist2.age = 30;
            artist2.birthdayDate = new DateTime(1988, 3, 3);
            artist2.comments = "comment2";

            Book book1 = new Book();
            book1.Id = "1";
            book1.author = "author1";
            book1.Name = "name1";
            book1.format = "epub";
            book1.OneProperty = "1";
            book1.pagesAmount = 600;

            Book book2 = new Book();
            book2.Id = "2";
            book2.author = "author2";
            book2.Name = "name2";
            book2.format = "fb2";
            book2.OneProperty = "2";
            book2.pagesAmount = 348;

            Catalog catalog = new Catalog();
            catalog.Artists = new List<Artist> { artist1, artist2 };
            catalog.Books = new List<Book> { book1, book2 };

            Console.WriteLine("ARTISTS");
            Console.WriteLine("{0,-5} {1,-10} {2,-5} {3,-15} {4,-15} {5,-15}", "Id", "Name", "Age", "Birthday date", "Comments", "Songs");
            foreach (Artist a in catalog.Artists)
            {
                string songs = "";
                foreach (Song s in a.Songs)
                {
                    songs += s.Id + ", ";
                }
                Console.WriteLine("{0,-5} {1,-10} {2,-5} {3,-15} {4,-15} {5,-15}", a.Id , a.Name, a.age, a.birthdayDate.ToShortDateString(), a.comments, songs);             

            }

            Console.WriteLine("\nBOOKS");
            Console.WriteLine("{0,-5} {1,-10} {2,-10} {3,-10} {4,-17} {5,-10}", "Id", "Name", "Author", "Format", "Number of pages", "Rating");
            foreach (Book b in catalog.Books)
            {
                Console.WriteLine("{0,-5} {1,-10} {2,-10} {3,-10} {4,-17} {5,-10}", b.Id, b.Name, b.author, b.format, b.pagesAmount, b.OneProperty);
            }
        }
    }
}
