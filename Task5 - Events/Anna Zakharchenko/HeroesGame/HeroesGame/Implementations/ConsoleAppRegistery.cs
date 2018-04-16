using System;
using HeroesCore.Abstractions;
using HeroesGame.Implementations;


namespace HeroesGame.Implementations
{
    class ConsoleAppRegistery : IRegistery
    {
        public IBoard Board { get; set; }
        public IModel Model { get; set; }
        public IUserIteraction UserIteraction { get; set; }

        public ConsoleAppRegistery()
        {
            Model = new GameModel(               
                new StarHero() { PosX = 5, PosY = 10 },
                mines: new IHero[]
                {
                    new MinHero() { PosX = 10, PosY = 4},
                    new MinHero() { PosX = 4, PosY = 14},
                    new MinHero() { PosX = 19, PosY = 6},
                    new MinHero() { PosX = 20, PosY = 10},
                    new MinHero() { PosX = 26, PosY = 3},
                }
                
                );
            Board = new ConsoleAppBoard(Model);
            UserIteraction = new ConsoleUserIteraction();
        }
    }
}
