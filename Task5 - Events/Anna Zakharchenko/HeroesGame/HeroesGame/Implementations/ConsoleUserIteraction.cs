using System;
using HeroesCore.Abstractions;

namespace HeroesGame.Implementations
{
    public class CommandEventArgs : GameEventArgs
    {
        public ConsoleKeyInfo ReceivedCommand { get; set; }
    }
    class ConsoleUserIteraction : IUserIteraction
    {
        public event GameEventHandler InputReceived;
        public event MineEventHandler HeroTripMine;

        public void StartListening(IModel model, IBoard board)
        {
            while (true) 
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                InputReceived?.Invoke(this, new CommandEventArgs()
                                            {
                                            ReceivedCommand = keyInfo
                                            });
                HeroTripMine?.Invoke(model.Hero, new GameEventArgs());

            }
        }            
    }
}
