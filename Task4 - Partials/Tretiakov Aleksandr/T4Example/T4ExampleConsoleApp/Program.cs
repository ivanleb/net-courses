using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataFile;
using T4ExampleConsoleApp.Partials;

namespace T4ExampleConsoleApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            var catalog = new Catalog
            {
                Artists = InitArtists(),
                Books = InitBooks()
            };

            Console.WriteLine($"Catalog: {ReflectionUtil.GetObjectStringRepresentation(catalog)}");

            Console.WriteLine($"=========================================");

            foreach (var artist in catalog.Artists)
            {
                Console.WriteLine($"Artist: {ReflectionUtil.GetObjectStringRepresentation(artist)}");
            }

            Console.WriteLine($"=========================================");

            foreach (var book in catalog.Books)
            {
                Console.WriteLine($"Book: {ReflectionUtil.GetObjectStringRepresentation(book)}");
            }

            Console.ReadKey();
        }

        private static Book[] InitBooks()
        {
            return new Book[]
            {
                new Book {Id="12", Name="Winnie-the-Pooh", Author="A. A. Milne", Format="eBook", PagesAmount = 50},
                new Book {Id="44", Name="How to Stop Worrying and Start Living", Author="Dale Carnegie", Format="pdf", PagesAmount = 200},
            };
        }

        private static Artist[] InitArtists()
        {
            return new Artist[]
                        {
                new Artist {
                    Age = 15,
                    BirthdayDate = DateTime.Now.AddYears(-15).AddMonths(-4),
                    Comments ="No comments",
                    Id = "14",
                    Name = "Bartlay",
                    Songs = new Song[] {
                        new Song { Id = "99"},
                        new Song { Id = "70"},
                        new Song { Id = "80"},
                        new Song { Id = "57"},
                    }
                },
                new Artist {
                    Age = 46,
                    BirthdayDate = DateTime.Now.AddYears(-46).AddMonths(-1),
                    Comments ="2 comments",
                    Id = "25",
                    Name = "Tatam",
                    Songs = new Song[] {
                        new Song { Id = "89"},
                        new Song { Id = "56"},
                        new Song { Id = "52"},
                        new Song { Id = "17"},
                    }
                },
                new Artist {
                    Age = 33,
                    BirthdayDate = DateTime.Now.AddYears(-33).AddMonths(-7),
                    Comments ="12 comments",
                    Id = "66",
                    Name = "Wolfram",
                    Songs = new Song[] {
                        new Song { Id = "43"},
                        new Song { Id = "44"},
                        new Song { Id = "1"},
                        new Song { Id = "9"},
                    }
                },
                new Artist {
                    Age = 24,
                    BirthdayDate = DateTime.Now.AddYears(-24).AddMonths(-5),
                    Comments ="5 comments",
                    Id = "18",
                    Name = "Benedict",
                }
            };
        }
    }
}
