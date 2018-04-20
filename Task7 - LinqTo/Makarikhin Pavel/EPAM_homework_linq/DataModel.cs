using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPAM_homework_linq
{
    static class DataModel
    {
        public static void ShowOutput(this IDataModel dataModel)
        {
            Console.WriteLine("====== SONGS ======");
            foreach (IEnumerable<Song> songs in dataModel.Songs.GroupByAuthor())
                if (songs.AsQueryable().IsByOneAuthor())
                {
                    Console.WriteLine(songs.First().Author);

                    foreach (Song song in songs)
                        Console.WriteLine($"{song.Name} | {song.Album} | {song.ChartPosition} | {song.Duration}");
                }

            Console.WriteLine("====== PICTURES ======");

            foreach (IEnumerable<Picture> pictures in dataModel.Pictures.GroupByAge())
            {
                Console.WriteLine($"AGE: {pictures.First().Age}");

                foreach (IEnumerable<Picture> picturesByStyle in pictures.AsQueryable().GroupByStyle())
                {
                    Console.WriteLine($"STYLE: {picturesByStyle.First().Style}");

                    foreach (Picture picture in picturesByStyle)
                        Console.WriteLine($"{picture.Name} | {picture.Author} | {picture.Style} | {picture.Age} | {picture.Cost}");
                }
            }

            Console.WriteLine("====== MOVIES ======");

            foreach (Movie movie in dataModel.Movies.SortMoviesByRating(true))
                Console.WriteLine($"{movie.Name} | {movie.Genre} | {movie.Rating} | {movie.Duration}");

            Console.WriteLine($"Average duration {dataModel.Movies.GetAverageDuration()}");
        }

        #region Movie extension methods
        public static IQueryable<Movie> GetMoviesByGenre(this IQueryable<Movie> movies, string genre)
        {
            return (from Movie movie in movies where movie.Genre == genre select movie).AsQueryable();
        }

        public static double GetAverageDuration(this IQueryable<Movie> movies)
        {
            return movies.Average((movie) => movie.Duration);
        }

        public static IQueryable<Movie> SortMoviesByRating(this IQueryable<Movie> movies, bool sortByIncreasing)
        {
            return sortByIncreasing? movies.OrderBy((movie) => movie.Rating) : movies.OrderByDescending((movie)=>movie.Rating);
        }

        public static IQueryable<Movie> SkipFirstNMovies(this IQueryable<Movie> movies, int N)
        {
            return movies.Skip(N);
        }

        public static IQueryable<IEnumerable<Movie>> GroupByGenre(this IQueryable<Movie> movies)
        {
            return movies.GroupBy((movie) => movie.Genre);
        }

        #endregion

        #region Picture extension methods
        public static double GetTheMostExpensive(this IQueryable<Picture> pictures)
        {
            return pictures.Max((picture) => picture.Cost);
        }

        public static double GetCheapestPrice(this IQueryable<Picture> pictures)
        {
            return pictures.Min((picture) => picture.Cost);
        }

        public static IQueryable<IEnumerable<Picture>> GroupByStyle(this IQueryable<Picture> pictures)
        {
            return from Picture picture in pictures group picture by picture.Style into sortedPictures select sortedPictures; 
        }

        public static IQueryable<IEnumerable<Picture>> GroupByAge(this IQueryable<Picture> pictures)
        {
            return pictures.GroupBy((picture) => picture.Age);
        }
        #endregion

        #region Song extension methods
        public static IQueryable<Song> GetFirstNSongInChart(this IQueryable<Song> songs, int N)
        {
            return (from Song song in songs orderby song.ChartPosition select song).Take(N);
        }

        public static IQueryable<IEnumerable<Song>> GroupByAuthor(this IQueryable<Song> songs)
        {
            return songs.GroupBy((song) => song.Author);
        }

        public static bool IsAlbum(this IQueryable<Song> songs)
        {
            return songs.All(((song) => song.Album == songs.First().Album));
        }

        public static bool IsByOneAuthor(this IQueryable<Song> songs)
        {
            return !songs.Any((song)=> song.Author != songs.First().Author);
        }

        #endregion
    }
}
