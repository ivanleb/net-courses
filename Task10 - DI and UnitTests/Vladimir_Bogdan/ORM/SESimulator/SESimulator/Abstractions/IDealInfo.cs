namespace SESimulator.Abstractions
{
    public interface IDealInfo
    {
        string Seller { get; set; }
        string Buyer { get; set; }
        decimal Amount { get; set; }
        string Stock { get; set; }
    }
}
