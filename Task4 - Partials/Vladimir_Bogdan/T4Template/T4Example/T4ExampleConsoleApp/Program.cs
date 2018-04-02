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
            Catalog myCatalog = new Catalog();
            List<Artist> Artists = new List<Artist>(2);
            Artists[0] = new Artist()
            {
                Name = "Armin van Buuren",
                Id = "0",
                Comments = "",
                BirthdayDate = new DateTime(year: 1976, month: 12, day: 25),
                Songs = new List<Song>(2) { new Song() { }, new Song() { } }
            };
        }
    }
}
