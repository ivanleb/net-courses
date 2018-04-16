using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events
{
    class GameLogic
    {
        public static void Run ()
        {
            IBoard board = StaticRegistry.board;
            IModel model = StaticRegistry.model;
            IUserInput input = StaticRegistry.input;

            board.Draw();
            model.Mover.startListenInput(input);
            board.StartListenInput(input);
            foreach (var prob in model.problem)
            {
                prob.WatchMoverTurn(model.Mover);
            }
            input.StartListen();
        }

    }
}
