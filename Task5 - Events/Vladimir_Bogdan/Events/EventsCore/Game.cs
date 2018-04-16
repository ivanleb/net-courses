using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventsCore.Abstractions;

namespace EventsCore
{
    public class GameEventArgs : EventArgs { }
    public delegate void GameEventHandler(object sender, GameEventArgs args);

    public class Game
    {
        public void Start(IModel model, IBoard board, IUserInput input)
        {
            board.Initialize(model);
            board.Draw(model);
            foreach (var hero in model.Heroes)
            {
                hero.ListenToTheInput(input);
                hero.ListenToTheOtherHeroes(model.Heroes);
            }
            foreach (var mine in model.Mines)
            {
                mine.ListenToTheHeroes(model.Heroes);
            }
            board.ListenToTheInput(input);
            input.ListenToUser();
        }
    }
}