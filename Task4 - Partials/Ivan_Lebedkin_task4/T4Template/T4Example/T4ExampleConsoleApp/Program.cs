using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataFile;
//using T4ExampleCore;

namespace T4ExampleConsoleApp
{
    
    class Program
    {
        static void Main(string[] args)
        {
            Artist artist = new Artist();           
            Book book = new Book();
            Song sng = new Song();

            Catalog catalog = new Catalog();
            
            List<Artist> artst = new List<Artist>();
            artst.Add(artist);
            List<Book> bks = new List<Book>();
            bks.Add(book);

            catalog.Artists = artst;
            catalog.Books = bks;

            catalog.Artists.All(x => { Console.WriteLine("{0}\n{1}\n{2}\n{3}\n{4}", x.Name, x.BirthdayDate, x.Age, x.Id, x.Comments);
                x.Songs.All(y => { Console.WriteLine(y.Id); return true; });
                return true; });
            Console.WriteLine();
            catalog.Books.All(x => 
            {
                Console.WriteLine("{0}\n{1}\n{2}\n{3}\n{4}\n{5}", x.Name, x.Author, x.PagesAmount, x.Id, x.OneProperty, x.Format);
                return true;
            });
        }
    }
}
