namespace EventsGame.Core.Abstractions
{
    public interface IDrawing
    {
        void SetupDrawing(IBoard board, IHero hero);
        void DrawAll();
        void StartListenInput(IUserInteraction input);
    }
}
