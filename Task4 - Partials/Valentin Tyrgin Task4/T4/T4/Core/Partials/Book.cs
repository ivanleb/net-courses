using System;

namespace T4
{
    partial class Book
    {
        public string Format { get; set; }
        public string Author { get; set; }
        public int PagesAmount { get; set; }
        public void Show()
        {
            Console.WriteLine($"Id:{Id}\tName: {Name}\tAuthor: {Author}\tPages amount: {PagesAmount}\tFormat: {Format}");
        }
    }
}