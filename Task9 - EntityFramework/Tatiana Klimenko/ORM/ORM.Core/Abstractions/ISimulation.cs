namespace ORM.Core.Abstractions
{
    public interface ISimulation
    {
        bool IsContinue { get; set; }

        void Run();
    }
}
