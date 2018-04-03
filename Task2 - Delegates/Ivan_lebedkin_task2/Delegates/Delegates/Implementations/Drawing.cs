using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Delegates.Core.Abstractions;
using Delegates.Core;

namespace Delegates.Implementations
{
    class Drawing : IShowDrawingToUser
    {
        protected static int origRow;
        protected static int origCol;

        public void Draw(IBoard board)
        {
            Console.Clear();
            origRow = Console.CursorTop;
            origCol = Console.CursorLeft;
            
            Int32 size = 15;
            for (int i = 1; i < size; i++)
            {
                WriteAt("|", 0, i);
                WriteAt("|", size, i);
                WriteAt("-", i, size);
                WriteAt("-", i, 0);
            }
            WriteAt("+", 0, 0);
            WriteAt("+", 0, size);
            WriteAt("+", size, 0);
            WriteAt("+", size, size);

            IEnumerable<IDrawingObject> currentDrawingObjects = board.GetObjects();
            foreach (var obj in currentDrawingObjects)
            {
                if (obj is Point)
                {
                    WriteAt("+", size/4, size/4);
                    Console.SetCursorPosition(size + 1, size + 1);
                }
                if (obj is HorizontalLine)
                {
                    for (int i = 1; i < size; i++)
                    {
                        WriteAt("-", i, size/2);
                    }
                    Console.SetCursorPosition(size + 1, size + 1);
                }
                if (obj is VerticalLine)
                {
                    for (int i = 1; i < size; i++)
                    {
                        WriteAt("|", size/2,i);
                    }
                    Console.SetCursorPosition(size + 1, size + 1);
                }
            }
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
    }
}
