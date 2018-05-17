using System;
using System.Collections.Generic;
using HeroesCore.Abstractions;

namespace HeroesGame.Implementations
{
    class GameModel : IModel
    {

        public IHero Hero { get; set; }
        public IEnumerable<IHero> Mines { get; set; }

        public GameModel(IHero hero, IHero[] mines) 
        {
            Hero = hero;
            Mines = mines;
        }


    }
}
