using System.Collections.Generic;

namespace ORM.Core.Model
{
    public enum Zone { Green, Orange, Black };

    public class Trader
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string PhoneNumber { get; set; }
        public decimal Balance { get; set; }
        public ICollection<Share> Portfolio { get; } = new List<Share>();
        public Zone Zone { get; set; }

        public override string ToString()
        {
            return $"{FirstName} {SecondName}";
        }

        public void AssignZone()
        {
            if (Balance > 0)
                Zone = Zone.Green;
            else if (Balance == 0)
                Zone = Zone.Orange;
            else if (Balance < 0)
                Zone = Zone.Black;
        }
    }
}
