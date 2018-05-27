namespace DIAndUnitTests.Core.Models
{
    public class Share
    {
        public int Id { get; set; }
        public Trader Owner { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}