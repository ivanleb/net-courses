using System;
using System.Collections.Generic;
using EventsGame.Core.Abstractions;

namespace EventsGame.Core
{
    public class GameLogic
    {
//        С помощью событий:
//а) сделать так, чтобы этот символ мог перемещаться с помощью нажатия клавиш в консоли.
//б) сделать так, чтобы выход за пределы доски был невозможен.
//в) сделать так, чтобы при касании предела доски - грань подсвечивалась другим цветом.

        public void Run(IRegistry registry)
        {
            Console.CursorVisible = false;

            registry.Board.SetupBoardSize(20, 10);
            registry.Hero.SetupHeroInitialPosition(10, 5, registry.Board);
            registry.Drawing.SetupDrawing(registry.Board, registry.Hero);

            registry.Drawing.DrawAll();

            registry.Hero.StartListenInput(registry.UserInteraction);
            registry.Drawing.StartListenInput(registry.UserInteraction);
            registry.UserInteraction.StartListening();
        }
    }
}
