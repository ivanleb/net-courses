using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoardDrawing.Core;
using BoardDrawing.Core.Abstractions;

namespace BoardDrawing.ConsoleApp.Implementations
{
    public class ConsoleAppUserInteraction : IUserInteraction
    {
        public event HeroMovesHandler InputRecieved;
        public event ExplosionHandler HeroStepsOnMine;

        public void StartListening(IModel model, IBoard board)
        {
            while (true)
            {
                var key = Console.ReadKey();
                if(InputRecieved != null)
                {
                    InputRecieved(this, new CommandEventArgs()
                    {
                        PressedKey = key
                    });
                }
                if (HeroStepsOnMine != null)
                {
                    int height = board.BoardSizeY;
                    HeroStepsOnMine(model.Hero, new MineArgs()
                    {
                        WhereToWrite = height
                    });
                }
            }
        }
    }
}
