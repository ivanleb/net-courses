namespace ORM.Core.Abstractions
{
    public interface ISimulation
    {
        bool KeepRunning { get; set; }
        void Run();
    }
}
