using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataFile
{
    public partial class Artist
    {
        public string Comments { get; set; }
        public int Age
        {
            get
            {
                var today = DateTime.Now.Date;
                return today.Year - this.BirthdayDate.Year - 1 + (today.DayOfYear >= this.BirthdayDate.DayOfYear ? 1 : 0);
            }
        }
        public DateTime BirthdayDate { get; set; }
    }
}
