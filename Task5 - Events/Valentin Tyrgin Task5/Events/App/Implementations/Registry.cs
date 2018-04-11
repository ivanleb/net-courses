using Events.Core;

namespace Events.App.Implementations
{
    internal class Registry : IRegistry
    {
        public Registry()
        {
            board = new Board(20, 12);
            hero = new Hero(new Point(1, 1));
            mines = new Mines(6, board);
            user = new User();
        }

        public IPoint point { get; set; }
        public IBoard board { get; set; }

        public IHero hero { get; set; }

        public IMine mine { get; set; }

        public IMines mines { get; set; }

        public IUserAction user { get; set; }
    }
}