using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegates.Core
{
    public interface INotifier
    {
        void WelcomeUser();
        void SayGoodbyeToUser();
        void ShowCustomMessageToUser(String message);
    }
    public abstract class Board
    {
        public Board(int width, int height)
        {
            boardSizeX = width;
            boardSizeY = height;
        }
        public int boardSizeX { get; }
        public int boardSizeY { get; }
    }
    public interface IUserInput
    {
        int GetAction();
    }
    public interface IInitializer
    {
        void DrawInitBoard(Board board);
    }
    public delegate void Draw(Board board);
    public interface IActionManager
    {
        void AffectBoard(int Action);
        bool isExit(int Action);
        bool isValid(int Action);
    }
    public interface IRegistry
    {
        Board Board { get; set; }
        IUserInput ProccessUserInput { get; set; }
        IInitializer Initializer { get; set; }
        IActionManager ActionManager { get; set; }
        INotifier Notifier { get; set; }
    }
    public class AppLogic
    {
        public static void Run(IRegistry registry)
        {
            var board = registry.Board;
            var proccessUserInput = registry.ProccessUserInput;
            var initializer = registry.Initializer;
            var actionManager = registry.ActionManager;
            var notifier = registry.Notifier;
            notifier.WelcomeUser();
            initializer.DrawInitBoard(board);
            while (true)
            {
                var currentAction = proccessUserInput.GetAction();
                if (actionManager.isExit(currentAction)) break;
                if (!actionManager.isValid(currentAction))
                {
                    notifier.ShowCustomMessageToUser("The entered action is not valid. Try again.");
                    continue;
                }
                actionManager.AffectBoard((int)currentAction);
            }
            notifier.SayGoodbyeToUser();
        }
    }
}
