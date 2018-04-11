using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Delegates
{
    class Program
    {
        public delegate void DrawBoard(IBoard board);
        public delegate int CheckInput(IBoard board);

        public static void MyDrawBoard (IBoard board)
        {
            Console.SetCursorPosition(0, 0);
            Console.Write("+");
            Console.SetCursorPosition(0, board.BoardSizeY);
            Console.Write("+");
            Console.SetCursorPosition(board.BoardSizeX, 0);
            Console.Write("+");
            Console.SetCursorPosition(board.BoardSizeX, board.BoardSizeY);
            Console.Write("+");

            for (int i = 1; i < board.BoardSizeX; i++)
            {
                Console.SetCursorPosition(i, 0);
                Console.Write("-");
                Console.SetCursorPosition(i, board.BoardSizeY);
                Console.Write("-");
            }
            for (int j = 1; j < board.BoardSizeY; j++)
            {
                Console.SetCursorPosition(0, j);
                Console.Write("|");
                Console.SetCursorPosition(board.BoardSizeX, j);
                Console.Write("|");
            }

            Console.SetCursorPosition(0, board.BoardSizeY+2);

        }
        public static int MyCheckInput(IBoard board)
        {
            Console.SetCursorPosition(0, board.BoardSizeY + 1);
            bool flag = false;
            string s = "";
            while (!flag)
            {
                s = Console.ReadLine();
                Console.SetCursorPosition(0, board.BoardSizeY + 1);
                int number;
                if (Int32.TryParse(s, out number))
                {
                    if ((Convert.ToInt32(s) == 1) || (Convert.ToInt32(s) == 2) || (Convert.ToInt32(s) == 3))
                        flag = true;
                }
                if (!flag)
                {
                    Console.SetCursorPosition(0, board.BoardSizeY + 1);
                    Console.Write("                  ");
                    Console.SetCursorPosition(0, board.BoardSizeY + 1);
                }

            }
            return Convert.ToInt32(s);

        }
        public static void DrawVLine(IBoard board)
        {           
            for ( int i =1; i < board.BoardSizeY; i++)
            {
                Console.SetCursorPosition(board.BoardSizeX / 2, i);
                Console.Write("|");
            }
            Console.SetCursorPosition(0,board.BoardSizeY+1);
        }

        public static void DrawHLine(IBoard board)
        {
            for (int i = 0; i < board.BoardSizeX; i++)
            {
                Console.SetCursorPosition(i,board.BoardSizeY / 2);
                Console.Write("-");
            }
            Console.SetCursorPosition(0, board.BoardSizeY + 1);
        }

        public static void DrawPlus(IBoard board)
        {            
                Console.SetCursorPosition(board.BoardSizeX / 4, board.BoardSizeY/4);
                Console.Write("+");
                Console.SetCursorPosition(0, board.BoardSizeY + 1);
        }


        public  interface IBoard
        {
            int BoardSizeX { get; set; }
            int BoardSizeY { get; set; }
            void Message();
        }

       public interface IClient
        {
             DrawBoard ReturnChoice(int y);
        }

        class MyClient : IClient
        {
            public int choice ;
            public DrawBoard ReturnChoice (int y)
                {
                if (y == 1) return DrawPlus;
                else
                    if (y == 2) return DrawVLine;
                return DrawHLine;
                }

        }

        class MyBoard: IBoard
        {
          public  int BoardSizeX { get; set; }
          public int BoardSizeY { get; set; }
          public MyBoard(int x , int y)
            {
                this.BoardSizeX = x;
                this.BoardSizeY = y;
            }

            public delegate void DrawSymbol(IBoard board);

            public void Message()
            {
                Console.WriteLine(" Выберите, что Вы бы хотели ввести:");
                Console.WriteLine("1 - точку(в центре левого верхнего угла доски)");
                Console.WriteLine("2 - вертикальную линию, которая делит доску пополам");
                Console.WriteLine("3 - горизонтальную линию, которая делит доску пополам.");
            }

        }
       
        static void Main(string[] args)
        {
            MyBoard board = new MyBoard(30,10);
            DrawBoard DelDraw = MyDrawBoard;
            CheckInput DelCheck = MyCheckInput;
            DelDraw(board);
            board.Message();
            Console.SetCursorPosition(0, board.BoardSizeY + 1);
            MyClient Client = new MyClient();

            while (true) 
            {
                Client.choice = MyCheckInput(board);
                DelDraw += Client.ReturnChoice(Client.choice);
                DelDraw(board);             
            } 
           
            Console.ReadKey();
        }
    }
}
