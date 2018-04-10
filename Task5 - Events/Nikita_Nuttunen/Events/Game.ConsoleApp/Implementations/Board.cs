using Game.Core;
using Game.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.ConsoleApp.Implementations
{
    public class Board : IBoard
    {
        delegate void DrawPart(IModel model);
        DrawPart drawAll;

        public int SizeX { get; set; }
        public int SizeY { get; set; }

        private readonly IModel model;

        public Board(IModel model)
        {
            this.model = model;
        }

        public void SetupBoard(int sizeX, int sizeY)
        {
            Console.CursorVisible = false;
            this.SizeX = sizeX;
            this.SizeY = sizeY;

            drawAll = DrawCanvas;
            drawAll += DrawHero;
            drawAll += DrawMines;
        }

        public void Draw(IModel model)
        {
            Console.Clear();
            drawAll?.Invoke(model);
        }

        private void DrawCanvas(IModel model)
        {
            WriteAt("+", 0, 0);
            WriteAt("+", 0, this.SizeY);
            WriteAt("+", this.SizeX, 0);
            WriteAt("+", this.SizeX, this.SizeY);

            for (int i = 1; i < this.SizeX; i++)
            {
                this.WriteAt("-", i, 0);
            }

            for (int i = 1; i < this.SizeY; i++)
            {
                this.WriteAt("|", 0, i);
            }

            for (int i = 1; i < this.SizeX; i++)
            {
                this.WriteAt("-", i, this.SizeY);
            }

            for (int i = 1; i < this.SizeY; i++)
            {
                this.WriteAt("|", this.SizeX, i);
            }
        }

        private void DrawHero(IModel model)
        {
            var hero = model.Hero;
            this.WriteAt(hero.HeroSymbol.ToString(), hero.PositionX, hero.PositionY);
        }

        private void DrawMines(IModel model)
        {
            foreach (var mine in model.Mines)
            {
                this.WriteAt(mine.MineSymbol.ToString(), mine.PositionX, mine.PositionY);
            }
        }

        private void WriteAt(string s, int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(s);
        }

        public void StartListenInput(IUserInteraction input)
        {
            input.InputReceived += OnInputReceived;
            input.InputReceived += HeroReachedWall;                        
        }
        
        private void OnInputReceived(object sender, GameEventArgs eventArgs)
        {
            Draw(this.model);
        }

        private void HeroReachedWall(object sender, GameEventArgs eventArgs)
        {
            var hero = model.Hero;
            Console.ForegroundColor = ConsoleColor.Red;

            if (hero.PositionX == 0)
            {
                for (int i = 1; i < this.SizeY; i++)
                {
                    this.WriteAt("|", 0, i);
                }
                hero.PositionX++;
            }

            if (hero.PositionX == this.SizeX)
            {
                for (int i = 1; i < this.SizeY; i++)
                {
                    this.WriteAt("|", this.SizeX, i);
                }
                hero.PositionX--;
            }

            if (hero.PositionY == 0)
            {
                for (int i = 1; i < this.SizeX; i++)
                {
                    this.WriteAt("-", i, 0);
                }
                hero.PositionY++;
            }

            if (hero.PositionY == this.SizeY)
            {
                for (int i = 1; i < this.SizeX; i++)
                {
                    this.WriteAt("-", i, this.SizeY);
                }
                hero.PositionY--;
            }

            Console.ForegroundColor = ConsoleColor.White;
            DrawHero(model);
        }        
    }
}
