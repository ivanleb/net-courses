using System;

namespace HeroesCore.Abstractions
{
    public class GameEventArgs 
    {        
    }

    public delegate void GameEventHandler(object sender, GameEventArgs eventArgs);
    public interface IUserIteraction
    {
        void StartListening();
        event GameEventHandler InputReceived;
    }
}
