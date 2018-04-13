namespace Delegates.Core.Abstractions
{
    public interface IRegistry
    {
        IBoard Board { get; set; }
        IDrawing Drawing { get; set; }
        IShowMessageToUser ShowMessageToUser { get; set; }   
        IProcessUserChoice ProcessUserChoice { get; set; }
    }
}