namespace Delegates.Core.Abstractions
{
    public interface IBoard
    {
        int Width { get; set; }
        int Height { get; set; }
        char VerticalLineChar { get; set; }
        char HorizontalLineChar { get; set; }
    }
}
