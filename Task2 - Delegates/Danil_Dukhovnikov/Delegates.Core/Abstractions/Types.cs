namespace Delegates.Core.Abstractions
{
    public delegate void Draw(IBoard board);

    public enum DrawType
    {
        Point,
        VerticalLine,
        HorizontalLine,
        Clear,
        Stop,
        Error
    }
}