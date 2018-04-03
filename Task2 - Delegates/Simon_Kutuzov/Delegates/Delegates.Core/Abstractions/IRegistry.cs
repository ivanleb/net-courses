namespace Delegates.Core.Abstractions
{
    public interface IRegistry
    {
        IBoard Board { get; set; }
        IDrawing Drawing { get; set; }
        IStringOutput StringOutput { get; set; }
        IUserActions UserActions { get; set; }
    }
}
