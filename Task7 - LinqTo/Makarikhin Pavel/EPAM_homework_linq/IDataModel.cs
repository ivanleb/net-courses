using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPAM_homework_linq
{
    public class Song
    {
        public string Name;
        public string Author;
        public string Album;

        public int ChartPosition;

        public double Duration;
    }

    public class Picture
    {
        public string Name;
        public string Author;
        public string Style;

        public int Age;

        public double Cost;
    }

    public class Movie
    {
        public string Name;
        public string Genre;

        public double Rating;
        public double Duration;
    }

    public interface IDataModel
    {
        IQueryable<Song> Songs { get; }
        IQueryable<Picture> Pictures { get; }
        IQueryable<Movie> Movies { get; }
    }
}
