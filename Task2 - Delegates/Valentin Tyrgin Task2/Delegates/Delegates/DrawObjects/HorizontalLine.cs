namespace Delegates
{
    internal class HorizontalLine : Board
    {
        public string Name { get; } = "Горизонтальная линия";

        public override void Draw(IBoard board)
        {
            for (var i = 1; i < board.GetWidth(); i++)
            {
                var utils = new Utils();
                utils.WriteAt("-", i, board.GetHeight() / 2);
            }
        }

        public override string GetName()
        {
            return Name;
        }
    }
}