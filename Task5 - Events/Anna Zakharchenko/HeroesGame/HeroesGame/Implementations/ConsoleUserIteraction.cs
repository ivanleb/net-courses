using System;
using HeroesCore.Abstractions;

namespace HeroesGame.Implementations
{
    public class CommandEventArgs : GameEventArgs
    {
        public ConsoleKeyInfo ReceivedCommand { get; set; }
        public IModel WithMinModel { get; set; }
    }
    class ConsoleUserIteraction : IUserIteraction
    {
        public event GameEventHandler InputReceived;

        public void StartListening()
        {
            while (true) 
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                if (InputReceived != null)
                {
                    InputReceived(this, new CommandEventArgs() { ReceivedCommand = keyInfo });        
                }

            }
        }            
    }
}
