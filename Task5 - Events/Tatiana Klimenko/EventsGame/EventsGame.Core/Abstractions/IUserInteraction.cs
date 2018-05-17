namespace EventsGame.Core.Abstractions
{
    public delegate void GameEventHandler(object sender, IGameEventArgs eventArgs);

    public interface IUserInteraction
    {
        void StartListening();
        event GameEventHandler InputReceived;
    }
}
