namespace EventsGame.Core.Abstractions
{
    public interface IRegistry
    {
        IBoard Board { get; set; }
        IHero Hero { get; set; }
        IDrawing Drawing { get; set; }
        IAnimation Animation { get; set; }
        IUserInteraction UserInteraction { get; set; }
        IGameEventArgs GetGameEventArgs { get; set; }
    }
}
