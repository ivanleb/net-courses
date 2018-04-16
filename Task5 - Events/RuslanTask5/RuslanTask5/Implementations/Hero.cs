using System;
using System.Collections.Generic;
using RuslanTask5.Abstractions;


namespace RuslanTask5.Implementations
{
    class Hero : IHero
    {
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public char Marker { get; set; }
    }
}
