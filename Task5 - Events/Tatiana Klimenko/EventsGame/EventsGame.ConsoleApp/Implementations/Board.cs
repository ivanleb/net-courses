using EventsGame.Core.Abstractions;

namespace EventsGame.ConsoleApp.Implementations
{
    class Board : IBoard
    {
        public int SizeX { get; set; }
        public int SizeY { get; set; }

        public void SetupBoardSize(int x, int y)
        {
            this.SizeX = x;
            this.SizeY = y;
        }
    }
}
