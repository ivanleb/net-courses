using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPAM_homework_events
{
    class ConsoleController : IController
    {
        public event Action<Point> OnMotionHero;

        public int ProcessUserInput()
        {
            var c = Console.ReadKey().Key;

            switch (c)
            {
                case ConsoleKey.LeftArrow:
                    OnMotionHero(new Point(-1, 0));
                    break;
                case ConsoleKey.RightArrow:
                    OnMotionHero(new Point(1, 0));
                    break;
                case ConsoleKey.UpArrow:
                    OnMotionHero(new Point(0, -1));
                    break;
                case ConsoleKey.DownArrow:
                    OnMotionHero(new Point(0, 1));
                    break;
                case ConsoleKey.Q:
                    return -1;
                default:
                    break;
            }

            return 1;
        }
    }
}
