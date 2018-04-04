using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataFile
{
    public partial class Book
    {
        public Book()
        {
            this.Author = "Donald Ervin Knuth";
            this.Name = "The Art of Computer Programming";
            this.PagesAmount = "100500";
            this.OneProperty = "public domain";
            this.Format = "A4";
            this.Id = "02";
        }
        public String Format { get; set; }
        public String Author { get; set; }
        public String PagesAmount { get; set; }
    }
}
