using System;
using System.Collections.Generic;
using Events.Core.Abstractions;
using Task5_Events.Implementations;

namespace Events.Console.Implementations
{
    public class ConsoleModel : IModel
    {
        public IHero Hero { get; set; }
        public IList<IMine> Mines { get; set; }

        public ConsoleModel()
        {
            Mines = new List<IMine>();
            
            Hero = new ConsoleHero
            {
                X = 1, 
                Y = 2, 
            };
            
            var random = new Random();
            var minesCount = random.Next(5, 10);

            for (var i = 0; i < minesCount; i++)
            {
                Mines.Add(
                    new ConsoleMine
                    {
                        X = random.Next(1, StaticRegistry.Board.Width - 1),
                        Y = random.Next(1, StaticRegistry.Board.Height - 1)
                    });
            }
        }
    }
}