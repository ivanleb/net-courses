using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoardDrawing.Core.Abstractions;

namespace BoardDrawing.Core
{


    public class ProgramLogic
    {
        public void Run(IRegistry registry)
        {
            var board = registry.Board;
            var model = registry.Model;
            var showMessageToUser = registry.ShowMessageToUser;
            var input = registry.UserInteraction;
            showMessageToUser.ShowMessage("To start a game press any key");
            board.PrepareBoard(20,20);
            foreach (var hero in model.Heroes)
            {
                hero.StartListenInput(input);
                foreach(var mine in model.Mines)
                {
                    mine.StartListenHero(input);
                }
            }              
            board.StartListenInput(input);

            input.StartListening(model, board);
        }
    }
}
