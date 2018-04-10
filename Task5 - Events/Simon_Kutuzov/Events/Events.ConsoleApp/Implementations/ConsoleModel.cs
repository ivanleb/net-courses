using System;
using System.Collections.Generic;
using Events.Core.Abstractions;

namespace Events.ConsoleApp.Implementations
{
    public class ConsoleModel : IModel
    {
        public IHero Hero { get; set; }
        public IList<IMine> Mines { get; set; }

        public ConsoleModel()
        {
            this.Hero = new PlusHero { X = 1, Y = 2 };

            var rng = new Random();
            this.Mines = this.Mines = new List<IMine>(7);
            for (int i = 0; i < 7; i++)
            {
                this.Mines.Add(
                    new ConsoleMine
                    {
                        X = rng.Next(1, StaticRegistry.board.Width - 1),
                        Y = rng.Next(1, StaticRegistry.board.Height - 1)
                    });
            }
        }
    }
}
