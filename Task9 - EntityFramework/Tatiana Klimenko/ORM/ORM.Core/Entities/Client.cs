using System.Collections.Generic;

public enum Zone
{
    Green,
    Orange,
    Black
};

namespace ORM.Core.Entities
{
    public class Client
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string PhoneNumber { get; set; }

        public decimal Balance { get; set; }

        public ICollection<Stock> Stocks { get; set; }

        public Zone Zone
        {
            get
            {
                if (Balance == 0)
                    return Zone.Orange;
                else if (Balance < 0)
                    return Zone.Black;
                else
                    return Zone.Green;
            }
        }

        public override string ToString()
        {
            return string.Format($"{Id,3} {Name,10} {Surname,12} {PhoneNumber,16} {Balance,9} {Zone,6}");
        }
    }
}
