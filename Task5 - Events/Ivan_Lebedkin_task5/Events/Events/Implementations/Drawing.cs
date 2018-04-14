using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Events.Core.Abstractions;
using Events.Core;
using Events.Implementations;

namespace Events.Implementations
{
    class Drawing : IShowDrawingToUser
    {
        public Drawing()
        {
            DrawSmth = null;
            DrawSmth += DrawBoard;
            DrawSmth += DrawPoint;
            DrawSmth += DrawMines;
        }

        public Action<IBoard> DrawSmth { get; set; }        
         
        protected static int origRow;
        protected static int origCol;

        public void DrawPoint(IBoard board)
        {            
            WriteAt("+", board.Point.X, board.Point.Y);
        }

        public void DrawMines(IHaveMines board)
        {
            foreach (IGeometryObject mine in board.Mines)
            {
                WriteAt("X", mine.X, mine.Y);
            }            
        }

        public void DrawBoard(IBoard board)
        {
            Console.Clear();
            origRow = Console.CursorTop;
            origCol = Console.CursorLeft;

            if (board.Point.X <= 1) Console.ForegroundColor = ConsoleColor.Green;

            for (int i = 1; i < board.boardSizeY; i++)
            {
                WriteAt("|", 0, i);                    
            }
            WriteAt("+", 0, 0);
            WriteAt("+", 0, board.boardSizeY);

            Console.ForegroundColor = ConsoleColor.White;

            if (board.Point.X >= board.boardSizeX - 1) Console.ForegroundColor = ConsoleColor.Green;
            for (int i = 1; i < board.boardSizeY; i++)
            {
                WriteAt("|", board.boardSizeX, i);
            }            
            Console.ForegroundColor = ConsoleColor.White;

            if (board.Point.Y <= 1) Console.ForegroundColor = ConsoleColor.Green;
            for (int i = 1; i < board.boardSizeX; i++)
            {
                WriteAt("--", i, 0);
            }
            WriteAt("+", board.boardSizeX, 0);
            Console.ForegroundColor = ConsoleColor.White;

            if (board.Point.Y >= board.boardSizeY - 1) Console.ForegroundColor = ConsoleColor.Green;
            for (int i = 1; i < board.boardSizeX; i++)
            {
                WriteAt("--", i, board.boardSizeY);
            }
            WriteAt("+", board.boardSizeX, board.boardSizeY);
            Console.ForegroundColor = ConsoleColor.White;
        }
        protected static void WriteAt(string s, int x, int y)
        {
            try
            {
                Console.SetCursorPosition(origCol + x, origRow + y);
                Console.Write(s);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.Clear();
                Console.WriteLine(e.Message);
            }
        }

        public void ShiftHandler(object o, EventArgs e)
        {
            ConsoleKeyEventsArgs ce = (ConsoleKeyEventsArgs)e;

            switch (ce.CurrentConsoleKey)
            {
                case ConsoleKey.RightArrow:
                    ((IBoard)o).Point = new Point(((IBoard)o).Point.X + 1, ((IBoard)o).Point.Y);
                    break;
                case ConsoleKey.LeftArrow:
                    ((IBoard)o).Point = new Point(((IBoard)o).Point.X - 1, ((IBoard)o).Point.Y);
                    break;
                case ConsoleKey.DownArrow:
                    ((IBoard)o).Point = new Point(((IBoard)o).Point.X, ((IBoard)o).Point.Y + 1);
                    break;
                case ConsoleKey.UpArrow:
                    ((IBoard)o).Point = new Point(((IBoard)o).Point.X, ((IBoard)o).Point.Y - 1);
                    break;
                default:
                    break;
            }
            if (IsPointOutOfBoard((IBoard)o))
            {
                switch (ce.CurrentConsoleKey)
                {
                    case ConsoleKey.RightArrow:
                        ((IBoard)o).Point = new Point( ((IBoard)o).Point.X - 1, ((IBoard)o).Point.Y);
                        break;
                    case ConsoleKey.LeftArrow:
                        ((IBoard)o).Point = new Point(((IBoard)o).Point.X + 1, ((IBoard)o).Point.Y);
                        break;
                    case ConsoleKey.DownArrow:
                        ((IBoard)o).Point = new Point(((IBoard)o).Point.X, ((IBoard)o).Point.Y - 1);
                        break;
                    case ConsoleKey.UpArrow:
                        ((IBoard)o).Point = new Point(((IBoard)o).Point.X, ((IBoard)o).Point.Y + 1);
                        break;
                    default:
                        break;
                }
            }

            DrawSmth((IBoard)o);
        }
        public static bool IsPointOutOfBoard(IBoard board)
        {
            return (board.boardSizeX <= board.Point.X || 0 >= board.Point.X) 
                || (board.boardSizeY <= board.Point.Y || 0 >= board.Point.Y);
        }

        public void HitHandler()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Hit!\nPress any key for continue");
            Console.ReadKey();
            Thread.Sleep(300);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
