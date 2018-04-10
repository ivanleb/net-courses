using System;
using RuslanTask5.Abstractions;
namespace RuslanTask5.Implementations
{
    class InputProcess : IInputProcess
    {
        public event EventHandler InputReceived;

        public void Start(IHero hero,IBoard board)
        {
            while (true)
            {
                var key = Console.ReadKey(true).Key;
                InputReceived?.Invoke(key, new HeroMovementArgs(hero, board));
            }
        }
    }
}
