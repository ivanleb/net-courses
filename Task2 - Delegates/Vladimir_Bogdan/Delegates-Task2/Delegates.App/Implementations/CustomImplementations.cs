using System;
using System.Linq;
using System.Windows.Forms;
using Delegates.Core;

namespace Delegates.App.Implementations
{
    public static class StringExtention
    {
        public static void WriteUnderBoard(this string s, Board Board)
        {
            s.WriteAboutBoard(Board, 0, 1);
        }
        public static void WriteAboutBoard(this string s, Board Board, int x, int y)
        {
            try
            {
                Console.SetCursorPosition(Console.CursorLeft+x, Board.boardSizeY+Console.CursorTop + y);
                Console.Write(s);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.Clear();
                Console.WriteLine(e.Message);
            }
        }
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
            message.WriteUnderBoard(board);
        }
        public void WelcomeUser()
        {
            Console.WriteLine("Hello! Let's paint!");
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
            for (int j=0; j<board.boardSizeY; j++)
            {
                string side = "|";
                string middle = " ";
                if (j*(j-board.boardSizeY+1)==0) { side = "+"; middle = "-"; }
                side.WriteAt(0, j);
                side.WriteAt(board.boardSizeX-1, j);
                for(int i=1; i<board.boardSizeX-1;i++)
                {
                    middle.WriteAt(i, j);
                }
            }
        }
    }
    public class BoardActionManager : IActionManager
    {
        private const int ExitAction = 0;
        private Board board;
        private int[] allowedActions;
        Draw[] drawActionDelegates;
        public BoardActionManager(Board Board)
        {
            allowedActions = new int[] { 1, 2, 3 };
            drawActionDelegates = new Draw[allowedActions.Length];
            board = Board;
        }
        public void AffectBoard(int Action)
        {
            drawActionDelegates[Array.IndexOf(allowedActions, Action)]?.Invoke(board);
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
            Board = new CustomBoard(20, 10);
            ActionManager = new BoardActionManager(Board);
            Initializer = new BoardInitializer();
            Notifier = new BoardNotifier(Board);
            ProccessUserInput = new NotifyingInput(Notifier);
        }
    }
}
