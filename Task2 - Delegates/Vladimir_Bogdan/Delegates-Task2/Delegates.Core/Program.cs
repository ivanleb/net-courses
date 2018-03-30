using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegates.Core
{
    interface INotifier
    {
        void WelcomeUser();
        void SayGoodbyeToUser();
        void ShowCustomMessageToUser(String message);
    }
    abstract class Board
    {
        public Board(int width, int height)
        {
            boardSizeX = width;
            boardSizeY = height;
        }
        public delegate void Draw(Board board);
        int boardSizeX { get; set; }
        int boardSizeY { get; set; }
    }
    interface IProcessUserInput
    {
        int GetAction();
    }
    interface IInitializer
    {
        void DrawInitBoard(Board board);
    }
    interface IActionManager
    {
        void AffectBoard(Board board);
        bool isExit(int action);
        bool isValid(int action);
    }
    interface IRegistry
    {
        Board Board { get; set; }
        IProcessUserInput ProccessUserInput { get; set; }
        IInitializer Initializer { get; set; }
        IActionManager ActionManager { get; set; }
        INotifier Notifier { get; set; }
    }
    public class AppLogic
    {
        void Run(IRegistry registry)
        {
            var board = registry.Board;
            var proccessUserInput = registry.ProccessUserInput;
            var initializer = registry.Initializer;
            var actionManager = registry.ActionManager;
            var notifier = registry.Notifier;
            Board.Draw draw = null;
            draw += actionManager.AffectBoard;
            notifier.WelcomeUser();
            initializer.DrawInitBoard(board);
            while (true)
            {
                var currentAction = proccessUserInput.GetAction();
                if (actionManager.isExit(currentAction)) break;
                if (actionManager.isValid(currentAction))
                {
                    notifier.ShowCustomMessageToUser("The entered action is not valid. Try again.");
                    continue;
                }
                draw.Invoke(board);
            }
            notifier.SayGoodbyeToUser();
        }
    }
}
