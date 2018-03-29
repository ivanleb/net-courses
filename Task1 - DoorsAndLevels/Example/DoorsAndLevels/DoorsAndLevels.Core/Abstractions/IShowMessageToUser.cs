namespace DoorsAndLevels.Core.Abstractions
{
    public interface IShowMessageToUser
    {
        void ShowMessage(string message);
        void ShowDoorNumbers(int[] doorNumber);
    }
}
