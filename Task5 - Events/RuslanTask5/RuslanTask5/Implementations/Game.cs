using System;
using RuslanTask5.Abstractions;
using System.Collections.Generic;
namespace RuslanTask5.Implementations
{
    class Game
    {
        public struct StartParameters
        {
            public Board Board { get; set; }
            public Hero Hero { get; set; }
            public Bomb Bomb { get; set; }
            public int BombsCount { get; set; }
            public bool CursorVisible { get; set; }
        }
        private StartParameters startParameters;

        public Game(StartParameters startParameters)
        {
            this.startParameters = startParameters;
        }

        public void Run(IHeroMovement heroMoves,IInputProcess input)
        {
            Console.CursorVisible = startParameters.CursorVisible;
            new DrawAllComponents().DrawBoard(startParameters.Board);
            new DrawAllComponents().DrawHero(startParameters.Hero);

            heroMoves.StartListen(input);
            List<Bomb> bombs = new List<Bomb>();
            
            for (int i = 0; i < startParameters.BombsCount; i++)
            {
                bombs.Add(new Bomb(startParameters.Board, startParameters.Bomb.Marker));
            }
            foreach(var bomb in bombs)
                bomb.StartListening(input);

            input.Start(startParameters.Hero, startParameters.Board);
            

            Console.Read();
        }
        
    }
}
