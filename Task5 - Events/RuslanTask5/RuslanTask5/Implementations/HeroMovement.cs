using System;
using System.Collections.Generic;
using RuslanTask5.Abstractions;

namespace RuslanTask5.Implementations
{

    class HeroMovementArgs : EventArgs
    {
        public ConsoleKey key;
        public HeroMovementArgs(IHero hero, IBoard board)
        {
            this.hero = hero;
            this.board = board;
        }
        public IHero hero = new Hero();
        public IBoard board = new Board();
    }

    class HeroMovement : IHeroMovement
    {
        public void StartListen(IInputProcess input)
        {
            input.InputReceived += OnNextMove;
        }
        private void OnNextMove(object key, EventArgs eventArgs)
        {
            var args = (HeroMovementArgs)eventArgs;
            try
            {
                args.key = (ConsoleKey)key;
                if (IsNextMove(key, args))
                    HeroMove(this, args);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private bool IsNextMove(object nextMove, EventArgs arg)
        {
            var args = (HeroMovementArgs)arg;
            switch (nextMove)
            {
                case ConsoleKey.UpArrow:
                    {
                        if (args.hero.PositionY - 1 == args.board.StartBoardPositionY)
                        {
                            new DrawAllComponents().BoardReactionOnHero(args.board, new Hero()
                            {
                                PositionY = args.hero.PositionY - 1,
                                PositionX = args.hero.PositionX
                            });
                            return false;
                        }
                        return true;
                    }
                case ConsoleKey.DownArrow:
                    {
                        if (args.hero.PositionY + 1 == args.board.StartBoardPositionY + args.board.SizeY)
                        {
                            new DrawAllComponents().BoardReactionOnHero(args.board, new Hero()
                            {
                                PositionY = args.hero.PositionY + 1,
                                PositionX = args.hero.PositionX
                            });
                            return false;
                        }
                        return true;
                    }
                case ConsoleKey.RightArrow:
                    {
                        if (args.hero.PositionX + 1 == args.board.StartBoardPositionX + args.board.SizeX)
                        {
                            new DrawAllComponents().BoardReactionOnHero(args.board, new Hero()
                            {
                                PositionY = args.hero.PositionY,
                                PositionX = args.hero.PositionX + 1
                            });
                            return false;
                        }
                        return true;
                    }
                case ConsoleKey.LeftArrow:
                    {
                        if (args.hero.PositionX - 1 == args.board.StartBoardPositionX)
                        {
                            new DrawAllComponents().BoardReactionOnHero(args.board, new Hero()
                            {
                                PositionY = args.hero.PositionY,
                                PositionX = args.hero.PositionX - 1
                            });
                            return false;
                        }
                        return true;
                    }
                default: return false;
            }
        }

        private void HeroMove(object sender, EventArgs args)
        {
            var arg = (HeroMovementArgs)args;
            switch (arg.key)
            {
                case ConsoleKey.UpArrow:
                    {
                        new DrawAllComponents().Draw(' ', arg.hero.PositionX, arg.hero.PositionY);
                        int nextAllegedPosition = arg.hero.PositionY - 1;
                        if (nextAllegedPosition != arg.hero.PositionY)
                        {
                            arg.hero.PositionY -= 1;
                            new DrawAllComponents().DrawHero(arg.hero);
                        }
                        else
                            new DrawAllComponents().BoardReactionOnHero(arg.board, arg.hero);
                        break;
                    }
                case ConsoleKey.DownArrow:
                    {
                        new DrawAllComponents().Draw(' ', arg.hero.PositionX, arg.hero.PositionY);
                        int nextAllegedPosition = arg.hero.PositionY + 1;
                        if (nextAllegedPosition != arg.hero.PositionY)
                        {
                            arg.hero.PositionY += 1;
                            new DrawAllComponents().DrawHero(arg.hero);
                        }
                        else
                            new DrawAllComponents().BoardReactionOnHero(arg.board, arg.hero);
                        break;
                    }
                case ConsoleKey.LeftArrow:
                    {
                        new DrawAllComponents().Draw(' ', arg.hero.PositionX, arg.hero.PositionY);
                        int nextAllegedPosition = arg.hero.PositionX - 1;
                        if (nextAllegedPosition != arg.hero.PositionX)
                        {
                            arg.hero.PositionX -= 1;
                            new DrawAllComponents().DrawHero(arg.hero);
                        }
                        else
                            new DrawAllComponents().BoardReactionOnHero(arg.board, arg.hero);
                        break;
                    }
                case ConsoleKey.RightArrow:
                    {
                        new DrawAllComponents().Draw(' ', arg.hero.PositionX, arg.hero.PositionY);
                        int nextAllegedPosition = arg.hero.PositionX + 1;
                        if (nextAllegedPosition != arg.hero.PositionX)
                        {
                            arg.hero.PositionX += 1;
                            new DrawAllComponents().DrawHero(arg.hero);
                        }
                        else
                            new DrawAllComponents().BoardReactionOnHero(arg.board, arg.hero);
                        break;
                    }
            }
        }

    }
}
