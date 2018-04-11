using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Core.Abstractions
{
    public delegate void HeroMotionHandler(object sender, GameEventArgs eventArgs);
    public delegate void StepOnMineHandler(IHero hero, GameEventArgs eventArgs);

    public interface IUserInteraction
    {
        void StartListening(IModel model, IBoard board);
        event HeroMotionHandler InputReceived;
        event StepOnMineHandler StepOnMine;
    }
}
