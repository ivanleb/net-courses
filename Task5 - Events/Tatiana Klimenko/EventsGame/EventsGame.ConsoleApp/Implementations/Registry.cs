using EventsGame.Core.Abstractions;

namespace EventsGame.ConsoleApp.Implementations
{
    class Registry : IRegistry
    {
        public IBoard Board { get; set; }
        public IHero Hero { get; set; }
        public IDrawing Drawing { get; set; }
        public IAnimation Animation { get; set; }
        public IUserInteraction UserInteraction { get; set; }
        public IGameEventArgs GetGameEventArgs { get; set; }

        public Registry()
        {
            this.Board = new Board();
            this.Hero = new Hero();
            this.Drawing = new Drawing();
            this.Animation = new Animation();
            this.UserInteraction = new UserInteraction();
            this.GetGameEventArgs = new GameEventArgs();
        }
    }
}
