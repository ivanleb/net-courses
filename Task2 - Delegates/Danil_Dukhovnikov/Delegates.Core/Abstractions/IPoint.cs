namespace Delegates.Core.Abstractions
{
    public interface IPoint
    {
        int X { get; }
        int Y { get; }
        string Content { get; }
        
        void Draw();
    }
}