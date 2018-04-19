using System;

namespace HeroesCore.Abstractions
{
    public class GameEventArgs 
    {        
    }

    public delegate void GameEventHandler(object sender, GameEventArgs eventArgs);
    public delegate void MineEventHandler(IHero hero, GameEventArgs eventArgs);
    public interface IUserIteraction
    {
        void StartListening(IModel model, IBoard board);
        event GameEventHandler InputReceived;
        event MineEventHandler HeroTripMine;
    }
}
