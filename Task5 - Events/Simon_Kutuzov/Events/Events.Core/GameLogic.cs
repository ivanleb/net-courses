using Events.Core.Abstractions;

namespace Events.Core
{
    public static class GameLogic
    {
        public static void Run()
        {
            var board = StaticRegistry.board;
            var model = StaticRegistry.model;
            var input = StaticRegistry.input;

            board.Draw();
            model.Hero.StartListeningInput(input);
            board.StartListeningInput(input);
            foreach (var mine in model.Mines)
            {
                mine.WatchHeroMove(model.Hero);
            }
            input.StartListening();
        }
    }
}
