using System;
using System.Windows.Forms;
using RuslanTask5.Abstractions;

namespace RuslanTask5.Implementations
{

    class Bomb : IBomb
    {
        
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public char Marker { get; set; }
        public Bomb(IBoard board, char marker)
        {
            GetRandomBomb(board);
            new DrawAllComponents().Draw(marker, PositionX, PositionY);
        }
        public Bomb(char marker)
        { this.Marker = marker; }

        private void OnInputReceived(object sender, EventArgs eventArgs)
        {
            var args = (HeroMovementArgs)eventArgs;
            if (args.hero.PositionX == PositionX && args.hero.PositionY == PositionY)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                new DrawAllComponents().Draw('x', args.hero.PositionX, args.hero.PositionY);
                MessageBox.Show("Game Over!!!");
                Environment.Exit(0);
            }
        }


        private static Random randomValue = new Random();

        private void GetRandomBomb(IBoard board)
        {
            this.PositionX = randomValue.Next(board.StartBoardPositionX + 1, board.SizeX - 1);
            this.PositionY = randomValue.Next(board.StartBoardPositionY + 1, board.SizeY - 1);
        }

        public void StartListening(IInputProcess input)
        {
            input.InputReceived += OnInputReceived;
        }
    }
}
