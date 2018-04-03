using System.Security.Principal;
using Delegates.Core.Abstractions;

namespace Delegates.App.Implementations
{
    internal class Point : Board
    {
        //public string Name { get; private set; };
        private const string symbol = "+";
        Utils utils = new Utils();

        public Point()
        {
            Name = "Точка";
        }

        public override void Draw(IBoard board)
        {
            Draw(board.Width / 4, board.Height / 4);
        }

        public void Draw(int x, int y)
        {
            utils.WriteAt(symbol, x, y);
        }

        //public override string GetName()
        //{
        //    return Name;
        //}
    }
}