using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T4Example
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Creating
            Song smokeOnTheWater  = new Song();
            smokeOnTheWater.Id = "1";
            Song one = new Song();
            one.Id = "2";
            Song livingOnAPrayer = new Song();
            livingOnAPrayer.Id = "3";
            Song mamaSaid = new Song();
            mamaSaid.Id = "4";
            Song runAway = new Song();
            runAway.Id = "5";
            Song childInTime = new Song();
            childInTime.Id = "6";

            Artist deepPurple = new Artist();
            deepPurple.Age = 50;
            deepPurple.BirthdayDate = new DateTime(1958, 1, 1);
            deepPurple.Comments = "Classical hard-rock group";
            deepPurple.Id = "11";
            deepPurple.Name = "Deep Purple";
            deepPurple.Songs = new List<Song> { smokeOnTheWater, childInTime };

            Artist metallica = new Artist();
            metallica.Age = 37;
            metallica.BirthdayDate = new DateTime(1981, 1, 1);
            metallica.Comments = "One of the big four thrash";
            metallica.Id = "21";
            metallica.Name = "Metallica";
            metallica.Songs = new List<Song> { one, mamaSaid };

            Artist bonJovi = new Artist();
            bonJovi.Age = 35;
            bonJovi.BirthdayDate = new DateTime(1983, 1, 1);
            bonJovi.Comments = "The glam-rock group";
            bonJovi.Id = "31";
            bonJovi.Name = "Bon Jovi";
            bonJovi.Songs = new List<Song> { runAway, livingOnAPrayer };

            Book lordOfTheRings = new Book();
            lordOfTheRings.Author = "J. R. R. Tolkien";
            lordOfTheRings.Format = "Paper";
            lordOfTheRings.Id = "41";
            lordOfTheRings.Name = "Lord Of Rings";
            lordOfTheRings.OneProperty = "10/10";
            lordOfTheRings.PagesAmount = 2000;

            #endregion

            Catalog catalog = new Catalog();
            catalog.Artists = new List<Artist> { bonJovi, metallica, deepPurple };
            catalog.Books = new List<Book> { lordOfTheRings };

            foreach (Artist art in catalog.Artists)
            {
                Console.WriteLine($"Group {art.Name} - {art.Comments}\r\ndate of birth - {art.BirthdayDate.ToShortDateString()} " +
                    $"\r\n{art.Age} years old \r\nid in database - {art.Id}" +
                    $"\r\nId of songs:");
                foreach (Song son in art.Songs)
                {
                    Console.Write($" {son.Id} ");
                }
                Console.WriteLine();

            }

            foreach (Book book in catalog.Books)
            {
                Console.WriteLine($"\r\nBook name - {book.Name} \r\nAuthor - {book.Author}\r\nFormat - {book.Format} " +
                    $"\r\nNumber of pages = {book.PagesAmount} \r\nId in database - {book.Id}");
            }
            Console.ReadLine();
        }
    }
}
