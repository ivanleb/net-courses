using Events.Core.Abstractions;

namespace Task5_Events
{
    public class AppLogic
    {
        public static void Run()
        {
            var board = StaticRegistry.Board;
            var model = StaticRegistry.Model;
            var input = StaticRegistry.UserInteraction;
            
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