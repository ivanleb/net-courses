using Game.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Core
{
    public class GameLogic
    {
        public void Run(IRegistry registry)
        {
            var board = registry.Board;
            var model = registry.Model;
            var userInteraction = registry.UserInteraction;
            
            board.SetupBoard(30, 20);
            board.Draw(model);

            model.Hero.StartListenInput(userInteraction);

            foreach (var mine in model.Mines)
            {
                mine.StartListenHero(userInteraction);
            }

            board.StartListenInput(userInteraction);         

            userInteraction.StartListening(model, board);            
        }
    }
}
