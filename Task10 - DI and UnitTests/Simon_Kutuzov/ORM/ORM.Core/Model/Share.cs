namespace ORM.Core.Model
{
    public class Share
    {
        public int Id { get; set; }
        public Listing Listing { get; set; }
        public Trader Owner { get; set; }
    }
}
