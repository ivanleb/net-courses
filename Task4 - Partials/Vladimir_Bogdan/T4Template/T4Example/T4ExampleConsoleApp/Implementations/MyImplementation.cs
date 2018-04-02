using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataFile;
using T4ExampleCore.Abstractions;

namespace T4ExampleConsoleApp.Implementations
{
    class MyCatalogPrinter : ICatalogPrinter
    {
        public void PrintLiteratureContent(Catalog catalog)
        {
            if (catalog?.Books == null) return;
            if (catalog.Books.Count() == 0)
            {
                Console.WriteLine("There isn't even a book in the catalog yet.");
                return;
            }
            Console.WriteLine("There are the following books in the catalog:");
            foreach (var book in catalog.Books)
            {
                Console.WriteLine(string.Format("ID: {0}, Author: {1}, The {2} {3} part {4}. The volume is {5} pages.", book.Id, book.Author, book.Name, book.Format, book.OneProperty, book.PagesAmount));
            }
        }

        public void PrintMusicContent(Catalog catalog)
        {
            if (catalog?.Artists == null) return;
            if (catalog.Artists.Count() == 0)
            {
                Console.WriteLine("There is no artists in the catalog yet.");
                return;
            }
            foreach (var artist in catalog.Artists)
            {
                Console.WriteLine(string.Format("ID: {0}, {1} is a {2} years old {3}.", artist.Id, artist.Name, artist.Age, artist.Comments));
                Console.WriteLine("The artist's songs in the catalog: ");
                foreach (var song in artist.Songs)
                {
                    Console.WriteLine(string.Format("ID: {0}", song.Id));
                }
            }
        }
    }

    class MyCatalogManager : ICatalogManager
    {
        public Catalog CreateNewCatalog()
        {
            return new Catalog() { Artists = new List<Artist>(), Books = new List<Book>()};
        }

        public void FillMusicContent(Catalog catalog)
        {
            catalog.Artists = new List<Artist>(2) {
                new Artist()
                    {
                        Name = "Armin van Buuren",
                        Id = "0",
                        Comments = "Dutch DJ, record producer and remixer from South Holland",
                        BirthdayDate = new DateTime(year: 1976, month: 12, day: 25),
                        Songs = new List<Song>(2) { new Song() { Id = "AVB1" }, new Song() { Id = "AVB2" } }
                    },
                new Artist()
                    {
                        Name = "Petr Chaykovskiy",
                        Id = "1",
                        Comments = "Russian composer of romantic period",
                        BirthdayDate = new DateTime(year: 1840, month: 5, day: 7),
                        Songs = new List<Song>(2) { new Song() { Id = "Symph.1" }, new Song() { Id = "Op.1" } }
                }
            };
        }

        public void FillLiteratureContent(Catalog catalog)
        {
            catalog.Books = new List<Book>()
            {
                new Book() {Id = "1", Author="Lev Tolstoy", Format="Novel", Name="War and Peace", OneProperty="1", PagesAmount = 1000 },
                new Book() {Id = "2", Author="Lev Tolstoy", Format="Novel", Name="War and Peace", OneProperty="2", PagesAmount = 1000 },
                new Book() {Id = "3", Author="Lev Tolstoy", Format="Novel", Name="War and Peace", OneProperty="3", PagesAmount = 1000 },
                new Book() {Id = "4", Author="Lev Tolstoy", Format="Novel", Name="War and Peace", OneProperty="4", PagesAmount = 1000 },
                new Book() {Id = "5", Author="Lev Tolstoy", Format="Novel", Name="War and Peace", OneProperty="Epilogue", PagesAmount = 1000 },

            };
        }
    }

    class MyRegistry : Registry
    {
        public MyRegistry()
        {
            catalogManager = new MyCatalogManager();
            catalogPrinter = new MyCatalogPrinter();
        }
    }
}
