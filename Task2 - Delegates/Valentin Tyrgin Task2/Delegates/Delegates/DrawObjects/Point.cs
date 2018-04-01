namespace Delegates
{
    internal class Point : Board
    {
        public string Name { get; } = "Точка";

        public override void Draw(IBoard board)
        {
            var utils = new Utils();
            utils.WriteAt("x", board.GetWidth() / 4, board.GetHeight() / 4);
        }

        public override string GetName()
        {
            return Name;
        }
    }
}