namespace Delegates.Core.Abstractions
{
    public interface IUserActions
    {
        void SetBoardSize(IBoard board);
        int SelectMenuOption();
    }
}
