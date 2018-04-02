namespace Delegates
{
    internal class VerticalLine : Board
    {
        public string Name { get; } = "Вертикальная линия";

        public override void Draw(IBoard board)
        {
            for (var i = 1; i < board.GetHeight(); i++)
            {
                var utils = new Utils();
                utils.WriteAt("|", board.GetWidth() / 2, i);
            }
        }

        public override string GetName()
        {
            return Name;
        }
    }
}