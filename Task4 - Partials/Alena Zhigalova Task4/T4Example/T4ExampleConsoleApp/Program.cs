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
            Song happyNewYear  = new Song();
            happyNewYear.Id = "1";
            Song helloWorld = new Song();
            helloWorld.Id = "2";
            Song firstDay = new Song();
            firstDay.Id = "3";


            Artist abba = new Artist();
            abba.Age = 46;
            abba.BirthdayDate = new DateTime(1972, 1, 1);
            abba.Comments = "pop-rock";
            abba.Id = "1";
            abba.Name = "Abba";
            abba.Songs = new List<Song> {happyNewYear};

            Artist timoMaas = new Artist();
            timoMaas.Age = 35;
            timoMaas.BirthdayDate = new DateTime(1988, 1, 1);
            timoMaas.Comments = "dj";
            timoMaas.Id = "2";
            timoMaas.Name = "Timo Mass";
            timoMaas.Songs = new List<Song> {firstDay, helloWorld};


            Book harryPotter= new Book();
            harryPotter.Author = "Mama Ro";
            harryPotter.Format = "Paper";
            harryPotter.Id = "1";
            harryPotter.Name = "Harry Potter";
            harryPotter.OneProperty = "Nice";
            harryPotter.PagesAmount = 777;

            #endregion

            Catalog catalog = new Catalog();
            catalog.Artists = new List<Artist> {timoMaas, abba};
            catalog.Books = new List<Book> {harryPotter};

            foreach (Artist art in catalog.Artists)
            {
                Console.WriteLine("Id:" + art.Id);
                Console.WriteLine(" Artist:" + art.Name + " " + art.Comments);
                Console.WriteLine(" Date of birth:" + art.BirthdayDate);
                Console.WriteLine(" Age:" + art.Age);
                Console.Write(" Id songs:");
                foreach (Song son in art.Songs)
                {
                    Console.Write(son.Id);
                }
                Console.WriteLine();

            }

            foreach (Book book in catalog.Books)
            {
                Console.WriteLine("Id: " + book.Id);
                Console.WriteLine("Name: " + book.Name);
                Console.WriteLine("Format: " + book.Format);
                Console.WriteLine("Pages: " + book.PagesAmount);
            }
            Console.ReadLine();
        }
    }
}
