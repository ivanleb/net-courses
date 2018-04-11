using Game.Core;
using Game.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.ConsoleApp.Implementations
{
    public class Registry : IRegistry
    {
        public IBoard Board { get; set; }
        public IModel Model { get; set; }
        public IUserInteraction UserInteraction { get; set; }

        public Registry()
        {
            Model = new GameModel(
                new ConsoleHero(15, 10),
                new IMine[]
                {
                    new ConsoleMine(10, 10),
                    new ConsoleMine(5,5),
                    new ConsoleMine(7,10),
                    new ConsoleMine(20, 16),
                    new ConsoleMine(20, 6)
                });

            Board = new Board(this.Model);
            UserInteraction = new ConsoleUserInteraction();
        }
    }
}
