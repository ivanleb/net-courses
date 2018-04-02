using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Drawer
{
    class Program
    {
        delegate void Draw();
        

        static void Main(string[] args)
        {
            DrawOnBoard dr = new DrawOnBoard();

            ConsoleKeyInfo key;
            Draw draw = null;
            
            dr.DrawBoarder();

            do
            {
                key = Console.ReadKey();

                while (key.Key != ConsoleKey.Enter)
                {
                    switch (key.KeyChar.ToString())
                    {
                        case "1":
                            draw += dr.DrawPoint;
                            break;
                        case "2":
                            draw += dr.DrawVerticalLine;
                            break;
                        case "3":
                            draw += dr.DrawHorizontalLine;
                            break;
                        default:
                            break;
                    }

                    key = Console.ReadKey();
                }

                draw();
                Console.SetCursorPosition(0, dr.x + 1);
            } while (key.Key != ConsoleKey.Escape);
        }

        static void KeyToMethod(Draw draw, string key, DrawOnBoard drawer)
        { 
            switch (key)
            {
                case "1":
                    draw += drawer.DrawPoint;
                    break;
                case "2":
                    draw += drawer.DrawVerticalLine;
                    break;
                case "3":
                    draw += drawer.DrawHorizontalLine;
                    break;
                default:
                    break;
            }
                 
        }
    }
}
