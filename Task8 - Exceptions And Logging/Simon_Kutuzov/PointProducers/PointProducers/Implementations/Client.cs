using PointProducers.Abstractions;

namespace PointProducers.Implementations
{
    public class Client : IClient
    {
        private readonly ILoggerService loggerService;
        public string Name { get; set; }

        public Client (string name, ILoggerService loggerService)
        {
            this.Name = name;
            this.loggerService = loggerService;
        }

        public void OnPointReceived(object sender, IPoint point)
        {
            loggerService.Info($"Client {Name} receved a point {point}");
        }
    }
}
