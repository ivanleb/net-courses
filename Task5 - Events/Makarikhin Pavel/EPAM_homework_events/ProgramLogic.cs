using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPAM_homework_events
{
    class ProgramLogic
    {
        delegate void Draw(IBoard board);

        public void Run(IRegistry registry)
        {
            Random rand = new Random();

            var UserInterface = registry.UserInterface;
            var Controller = registry.Controller;
            var Board = registry.Board;

            ((ConsoleGameModel)Board.GameModel).OnBoom += ((ConsoleInterface)UserInterface).BombDestroyedInfo;

            Board.GameModel.GenerateRandomLevel();

            Controller.OnMotionHero += Board.GameModel.MoveHero;

            Board.DrawBoard();

            while (Controller.ProcessUserInput() > 0)
            {
                Console.Clear();

                ((ConsoleInterface)UserInterface).DisplayLogs();

                Board.DrawBoard();
            }
    }
}
}
