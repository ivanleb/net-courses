namespace EventsGame.Core.Abstractions
{
    public interface IBoard
    {
        int SizeX { get; set; }
        int SizeY { get; set; }

        void SetupBoardSize(int x, int y);
    }
}
