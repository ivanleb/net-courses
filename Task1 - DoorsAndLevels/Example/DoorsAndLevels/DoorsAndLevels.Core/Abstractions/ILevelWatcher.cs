namespace DoorsAndLevels.Core.Abstractions
{
    public interface ILevelWatcher
    {
        void SetDoorsOnStart(int[] doorsNumbers);
        void GoNextLevel(int doorNumber);
        void GoPreviousLevel();
        int GetCurrentLevel();
        int[] GetDoorsOnCurrentLevel();
    }
}
