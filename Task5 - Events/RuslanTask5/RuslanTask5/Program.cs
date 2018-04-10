using System;
using System.Collections.Generic;
using RuslanTask5.Abstractions;
using RuslanTask5.Implementations;

namespace RuslanTask5
{
    class Program
    {
        static void Main(string[] args)
        {
            new Game(new Game.StartParameters
            {
                Hero = new Hero() { PositionX = 5, PositionY = 5, Marker = '+' },
                Board = new Board() { SizeX = 20, SizeY = 20 },
                CursorVisible = false,
                BombsCount = 100,
                Bomb = new Bomb('X')
            }).Run(new HeroMovement(),new InputProcess());
        }
    }
}

