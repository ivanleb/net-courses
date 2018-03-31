using System;
using System.Linq;
using System.Windows.Forms;
using Delegates.Core;

namespace Delegates.App.Implementations
{
    public static class StringExtention
    {
        public static void WriteAt(this string s, int x, int y)
        {
            try
            {
                Console.SetCursorPosition(x, y);
                Console.Write(s);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.Clear();
                Console.WriteLine(e.Message);
            }
        }
    }

    class BoardNotifier : INotifier
    {
        private Board board;
        public BoardNotifier(Board Board)
        {
            board = Board;
        }
        public void SayGoodbyeToUser()
        {
            MessageBox.Show("Goodbye!");
        }

        public void ShowCustomMessageToUser(string message)
        {
            if (Console.CursorTop<=board.boardSizeY)
            {
                Console.SetCursorPosition(0, board.boardSizeY + 1);
            }
            Console.WriteLine(message);
        }
        public void WelcomeUser()
        {
            MessageBox.Show("Hello! Let's paint!");
        }
    }

    class CustomBoard : Board
    {
        public CustomBoard(int width, int height) : base(width, height) { }
    }

    class NotifyingInput : IUserInput
    {
        private INotifier notifier;
        public NotifyingInput()
        {
            notifier = null;
        }
        public NotifyingInput(INotifier Notifier)
        {
            notifier = Notifier;
        }
        public int GetAction()
        {
            notifier?.ShowCustomMessageToUser("Enter the action to affect the board.");
            string input = Console.ReadLine();
            int action;
            if (Int32.TryParse(input, out action))
            { return action; }
            notifier?.ShowCustomMessageToUser("Couldn't parse your input.");
            return -1;
        }
    }

    class BoardInitializer : IInitializer
    {
        public void DrawInitBoard(Board board)
        {
            Console.Clear();
            for (int i=2; i<board.boardSizeX;i++)
            {
                "-".WriteAt(i, 1);
                "-".WriteAt(i, board.boardSizeY);
            }
            for (int j = 2; j < board.boardSizeY; j++)
            {
                "|".WriteAt(1, j);
                "|".WriteAt(board.boardSizeX, j);
            }
            "+".WriteAt(1, 1);
            "+".WriteAt(1, board.boardSizeY);
            "+".WriteAt(board.boardSizeX, 1);
            "+".WriteAt(board.boardSizeX, board.boardSizeY);
            Console.WriteLine();
        }
    }

    class BoardActionManager : IActionManager
    {
        public const int ExitAction = 0;
        private Board board;
        private int[] allowedActions;
        private Draw drawDelegate;
        private Draw[] ActionDelegates;
        private void DrawPoint(Board board)
        {

            "*".WriteAt((((board.boardSizeX+1) / 2)+1)/2, ((board.boardSizeY+1)/2+1) / 2);
        }
        private void DrawHorizontalLine(Board board)
        {
            for (int i = 2; i <= board.boardSizeX - 1; i++)
            {
                "-".WriteAt(i, (board.boardSizeY+1) / 2);
            }
        }
        private void DrawVerticalLine(Board board)
        {
            for (int i = 2; i <= board.boardSizeY - 1; i++)
            {
                "|".WriteAt((board.boardSizeX + 1) / 2, i);
            }
        }
        public BoardActionManager(Board Board, BoardInitializer BoardInitializer)
        {
            allowedActions = new int[] { 1, 2, 3 };
            drawDelegate = null;
            board = Board;
            ActionDelegates = new Draw[] { DrawPoint, DrawHorizontalLine, DrawVerticalLine };
            drawDelegate = BoardInitializer.DrawInitBoard;
        }
        public void Affect(int Action)
        {
            int i = Array.IndexOf(allowedActions, Action);
            if (!drawDelegate.GetInvocationList().Contains(ActionDelegates[i]))
            {
                drawDelegate += ActionDelegates[i];
            }
            drawDelegate.Invoke(board);
        }
        public bool isExit(int Action)
        {
            return Action == ExitAction;
        }

        public bool isValid(int Action)
        {
            return allowedActions.Contains(Action);
        }
    }

    public class CustomRegistry : IRegistry
    {
        public IActionManager ActionManager { get; set; }

        public Board Board { get; set; }

        public IInitializer Initializer { get; set; }

        public INotifier Notifier { get; set; }

        public IUserInput ProccessUserInput { get; set; }

        public CustomRegistry()
        {
            Board = new CustomBoard(10, 10);
            Initializer = new BoardInitializer();
            ActionManager = new BoardActionManager(Board, (BoardInitializer) Initializer);
            Notifier = new BoardNotifier(Board);
            ProccessUserInput = new NotifyingInput(Notifier);
        }
    }
}
