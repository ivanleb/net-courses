using System;
using System.Collections.Generic;

namespace T4.App
{
    internal class Program
    {
        private static readonly Random random = new Random();

        private static void Main()
        {
            var artists = new List<Artist>();
            var books = new List<Book>();
            var count = random.Next(2, 6);
            for (var i = 1; i <= count; i++)
            {
                var artist = new Artist
                {
                    Id = "ArtistID_" + i,
                    Age = i + 20,
                    BirthdayDate = DateTime.Now.Date,
                    Comments = "ArtistID_" + i + "_комментарий",
                    Name = "Artist_" + (char) i
                };
                var songsCount = random.Next(1, 5);
                var songs = new List<Song>();
                for (var j = 1; j < songsCount; j++)
                {
                    var song = new Song
                    {
                        Id = "SongId_" + i + j
                    };
                    songs.Add(song);
                }
                artist.Songs = songs;
                artists.Add(artist);
                var book = new Book
                {
                    Id = "BookID_" + i,
                    Name = "Book_" + (char) i,
                    Author = "AuthorOfBook_" + i,
                    Format = "KindOfFormat_#" + i,
                    PagesAmount = i * random.Next(100, 300)
                };
                books.Add(book);
            }
            var catalog = new Catalog
            {
                Artists = artists,
                Books = books
            };
            foreach (var artist in catalog.Artists)
            {
                artist.Show();
            }
            foreach (var book in catalog.Books)
            {
                book.Show();
            }
        }
    }
}