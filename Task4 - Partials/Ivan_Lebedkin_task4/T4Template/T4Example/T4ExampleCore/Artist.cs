using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataFile
{
    public partial class Artist
    {
        public Artist()
        {
            this.Age = 53;
            this.BirthdayDate = new DateTime(1964, 07, 27);
            this.Id = "01";
            this.Name = "Jeffrey Richter";
            this.Comments = "microsoft";
            List<Song> defaultSong = new List<Song>();
            Song song = new Song();
            song.Id = "CLR via C#";
            defaultSong.Add(song);
            this.Songs = defaultSong;
        }
        public String Comments { get; set; }
        public Int32 Age { get; set; }
        public DateTime BirthdayDate { get; set; }
    }
}
