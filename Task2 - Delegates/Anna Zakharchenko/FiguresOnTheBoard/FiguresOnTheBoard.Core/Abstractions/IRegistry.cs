
namespace FiguresOnTheBoard.Core.Abstractions
{
    public interface IRegistry
    {
        IBoard Board { get; set; }
        IProcessUserInput ProcessUserInput { get; set; }
        IShowMessageToUser ShowMessageToUser { get; set; }
        IDrawing Drawing { get; set; }
        IExecuteUserChoice ExecuteUserChoice { get; set; }      
    }
}
