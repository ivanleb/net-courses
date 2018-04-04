
namespace FiguresOnTheBoard.Core.Abstractions
{
    public interface IExecuteUserChoice
    {
        //как-то по-другому сделать, мне не нравки
        void MakeFigure(IBoard board, IDrawing drawing, int action);
        bool IsExit(int action);
        bool IsValid(int action);
    }
}
