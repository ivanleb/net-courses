using System;
using HeroesCore.Abstractions;

namespace HeroesGame.Implementations
{
    class ConsoleAppBoard : IBoard
    {
        public int SizeX { get; set; }
        public int SizeY { get; set; }

        private IModel model;
        public ConsoleAppBoard(IModel model) 
        {
            this.model = model;
        }

        delegate void DrawPart(IModel model);
        DrawPart drawAll;

        public void SetUpBoard(int sizeX, int sizeY)
        {
            SizeX = sizeX;
            SizeY = sizeY;

            drawAll = DrawCanvas;
            drawAll += DrawHero;
            drawAll += DrawMines; 
        }

        private void DrawMines(IModel model)
        {
            foreach (MinHero mina in model.Mines)
            {
                WriteAt(mina.MarkSymbol, mina.PosX, mina.PosY);
            }
        }

        public void Draw(IModel model)
        {
            Console.Clear();
            if(drawAll!=null) drawAll(model);
        }

        public void StartListenInput(IUserIteraction input)
        {
            input.InputReceived += OnInputReceived;
            input.InputReceived += OnHeroLiveBoard;
        }

        private void OnHeroLiveBoard(object sender, GameEventArgs eventArgs)
        {
            DrawBoarSide(model);
        }

        private void OnInputReceived(object sender, GameEventArgs eventArgs)
        {
           Draw(model);
        }

        public void DrawCanvas(IModel model)
        {
            WriteAt("+", 0, 0);
            WriteAt("+", SizeX, 0);
            WriteAt("+", 0, SizeY);
            WriteAt("+", SizeX, SizeY);
            for (int i = 1; i < SizeX; i++) 
            {
                WriteAt("-", i, 0);
            }            
            for (int i = 1; i < SizeY; i++)
            {
                WriteAt("|", 0, i);
            }            
            for (int i = 1; i < SizeX; i++)
            {
                WriteAt("-", i, SizeY);
            }
            for (int i = 1; i < SizeY; i++)
            {
                WriteAt("|", SizeX, i);
            }
        }

        private void WriteAt(string str, int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(str);
        }

        public void DrawHero(IModel model) 
        {
                WriteAt(model.Hero.MarkSymbol, model.Hero.PosX, model.Hero.PosY);      
        }

        public void DrawBoarSide(IModel model)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            IHero hero = model.Hero;

            if (hero.PosX == 0)
            {
                for (int i = 1; i < SizeY; i++)
                {
                    WriteAt("|", 0, i);
                }
                hero.PosX++;
            }

            if (hero.PosX == SizeX)
            {
                for (int i = 1; i < SizeY; i++)
                {
                    WriteAt("|", SizeX, i);
                }
                hero.PosX--;
            }

            if (hero.PosY == 0)
            {
                for (int i = 1; i < SizeX; i++)
                {
                    WriteAt("-", i, 0);
                }
                hero.PosY++;
            }

            if (hero.PosY == SizeY)
            {
                for (int i = 1; i < SizeX; i++)
                {
                    WriteAt("-", i, SizeY);
                }
                hero.PosY--;
            }

            Console.ForegroundColor = ConsoleColor.White;
            DrawHero(model);
        }
    }
}
