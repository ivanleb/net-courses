using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoardDrawing.Core.Abstractions;

namespace BoardDrawing.ConsoleApp.Implementations
{
    class Hero : Point, IHero
    {
        public Hero(int x, int y, char c):base(x,y,c)
        {
            PosX = x;
            PosY = y;
            Sym = c;
        }

        public void OnInputRecieved(object sender, GameEventArgs eventArgs)
        {
            var args = (CommandEventArgs)eventArgs;

            if (args.PressedKey.Key == ConsoleKey.RightArrow)
            {
                PosX += 1;
            }
            if (args.PressedKey.Key == ConsoleKey.LeftArrow)
            {
                PosX -= 1;
            }
            if (args.PressedKey.Key == ConsoleKey.UpArrow)
            {
                PosY -= 1;
            }
            if (args.PressedKey.Key == ConsoleKey.DownArrow)
            {
                PosY += 1;
            }
        }

        public void StartListen(IUserInteraction input)
        {
            input.InputRecieved += OnInputRecieved;
        }
    }
}
