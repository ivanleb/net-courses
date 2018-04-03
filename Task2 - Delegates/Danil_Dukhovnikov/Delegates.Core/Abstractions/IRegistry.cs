namespace Delegates.Core.Abstractions
{
    public interface IRegistry
    {
        IBoard Board { get; set; }
        IShowMessageToUser ShowMessageToUser { get; set; }   
        IProcessUserChoice ProcessUserChoice { get; set; }
    }
}