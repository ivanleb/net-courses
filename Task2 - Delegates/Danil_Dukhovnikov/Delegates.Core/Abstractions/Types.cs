namespace Delegates.Core.Abstractions
{
    internal delegate void Draw(IBoard board);
    
    public enum MenuOptions
    {
        Point,
        VerticalLine,
        HorizontalLine,
        Clear,
        Error,
        Exit
    }
}