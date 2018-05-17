namespace EventsGame.Core.Abstractions
{
    public interface IHero
    {
        int PosX { get; set; }
        int PosY { get; set; }

        void SetupHeroInitialPosition(int x, int y, IBoard board);
        void StartListenInput(IUserInteraction input);
    }
}
