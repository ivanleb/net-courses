using System.Collections.Generic;

namespace DIAndUnitTests.Core.Models
{
    public enum Zone
    {
        Green,
        Orange,
        Black
    };
    
    public class Trader
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public decimal Balance { get; set; }
        public ICollection<Share> SharesCollection { get; } = new List<Share>();
        
        #region Ovveride methods

        public override string ToString()
        {
            return $"{Name} {Surname}";
        }

        #endregion
    }
}