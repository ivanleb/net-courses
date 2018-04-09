

namespace EventsExampleConsoleApp
{
    using EventsExampleAbstractions;
    using EventsExampleImplementations;
    
    class Program
    {
        static void Main(string[] args)
        {
            var gameModel = new GameModel(
               heroes: new IHero[]
               {
                    new PlusHero() { PosX = 5, PosY = 10 },
                    new PlusHero() { PosX = 10, PosY = 5 }
               });

            new Game()
           .Run(
               board: new ConsoleAppBoard(gameModel),
               model: gameModel,
               input: new ConsoleUserIteraction());
        }
    }
}

namespace EventsExampleAbstractions
{
    using System;
    using System.Collections.Generic;


    public interface IHero
    {
        int PosX { get; set; }
        int PosY { get; set; }

        string MarkSymbol { get; }

        void StartListenInput(IUserIteraction input);
    }
    
    public interface IModel
    {
        IEnumerable<IHero> Heroes { get; set; }
    }
    
    public interface IBoard
    {
        int SizeX { get; set; }
        int SizeY { get; set; }
        void SetupBoard(int sizeX, int sizeY);
        void Draw(IModel model);
        void StartListenInput(IUserIteraction input);
    }
    
    public class GameEventArgs
    {

    }

    public delegate void GameEventHandler(object sender, GameEventArgs eventArgs);
    public interface IUserIteraction
    {
        void StartListening();
        event GameEventHandler InputReceived;
    }

    public class CommandEventArgs : GameEventArgs
    {
        public ConsoleKeyInfo ReceivedCommand { get; set; }
    }


}

namespace EventsExampleImplementations
{
    using EventsExampleAbstractions;
    using System;
    using System.Collections.Generic;

    class PlusHero : IHero
    {
        public int PosX { get; set; }
        public int PosY { get; set; }

        public string MarkSymbol { get { return "+"; } }

        public void StartListenInput(IUserIteraction input)
        {
            input.InputReceived += OnInputReceived;
        }

        private void OnInputReceived(object sender, GameEventArgs eventArgs)
        {
            var args = (CommandEventArgs)eventArgs;

            if (args.ReceivedCommand.Key == System.ConsoleKey.LeftArrow) this.PosX--;
            if (args.ReceivedCommand.Key == System.ConsoleKey.RightArrow) this.PosX++;
            if (args.ReceivedCommand.Key == System.ConsoleKey.UpArrow) this.PosY--;
            if (args.ReceivedCommand.Key == System.ConsoleKey.DownArrow) this.PosY++;
        }
    }

    class GameModel : IModel
    {
        public IEnumerable<IHero> Heroes { get; set; }

        public GameModel(params IHero[] heroes)
        {
            this.Heroes = heroes;
        }
    }

    class ConsoleAppBoard : IBoard
    {
        delegate void DrawPart(IModel model);
        DrawPart drawAll;

        public int SizeX { get; set; }
        public int SizeY { get; set; }

        private readonly IModel model;

        public ConsoleAppBoard(IModel model)
        {
            this.model = model;
        }

        public void SetupBoard(int sizeX, int sizeY)
        {
            this.SizeX = sizeX;
            this.SizeY = sizeY;

            this.drawAll = DrawCanvas;
            this.drawAll += DrawHero;
        }

        public void Draw(IModel model)
        {
            Console.Clear();

            this.drawAll?.Invoke(model);
        }

        private void DrawCanvas(IModel model)
        {
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
            foreach (var hero in model.Heroes)
            {
                this.WriteAt(hero.MarkSymbol, hero.PosX, hero.PosY);
            }


        }
        private void WriteAt(string s, int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(s);
        }

        public void StartListenInput(IUserIteraction input)
        {
            input.InputReceived += OnInputReceived;
        }

        private void OnInputReceived(object sender, GameEventArgs eventArgs)
        {
            this.Draw(this.model);
        }
    }
    
    class ConsoleUserIteraction : IUserIteraction
    {

        public event GameEventHandler InputReceived;

        public void StartListening()
        {
            while (true)
            {
                var keyInfo = Console.ReadKey();


                InputReceived?.Invoke(this, new CommandEventArgs()
                {
                    ReceivedCommand = keyInfo
                });
            }
        }
    }
    
    class Game
    {
        public void Run(
            IBoard board,
            IModel model,
            IUserIteraction input)
        {
            board.SetupBoard(50, 20);
            board.Draw(model);

            foreach (var hero in model.Heroes)
            {
                hero.StartListenInput(input);
            }

            board.StartListenInput(input);

            input.StartListening();
        }
    }
}