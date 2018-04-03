using System;

namespace T4
{
    partial class Artist
    {
        public string Comments { get; set; }
        public int Age { get; set; }
        public DateTime BirthdayDate { get; set; }

        public void Show()
        {
            Console.WriteLine($"Id: {Id}\tName: {Name}\tAge: {Age}\tBirthDay: {BirthdayDate.ToShortDateString()}\tComments: {Comments}");
            foreach (var song in Songs)
            {
                Console.WriteLine($"SongId: {song.Id}");
            }
        }
    }
}
