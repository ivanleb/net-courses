namespace BoardGame.Core.Abstractions
{
    public interface IMessager
    {
        void ShowInfromtaion(string message);
        void ShowError(string message);
    }
}
