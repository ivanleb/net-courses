namespace EF.Core.Repositories
{
    public interface ITransactionGeneratorRepository
    {
        bool Active { get; set; }
    }
}