using Game.Core;
using Game.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.ConsoleApp.Implementations
{
    public class ConsoleUserInteraction : IUserInteraction
    {
        public event HeroMotionHandler InputReceived;
        public event StepOnMineHandler StepOnMine;        

        public void StartListening(IModel model, IBoard board)
        {
            while (true)
            {
                var keyInfo = Console.ReadKey();

                InputReceived?.Invoke(this, new CommandEventArgs()
                {
                    ReceivedCommand = keyInfo
                });

                StepOnMine?.Invoke(model.Hero, new GameEventArgs());
            }
        }
    }
}
