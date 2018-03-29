namespace DoorsAndLevels.Core.Abstractions
{
    public interface IRegistry
    {
        IDoorsNumbersBuilder DoorsNumbersBuilder { get; set; }
        IShowMessageToUser ShowMessageToUser { get; set; }
        IProcessUserInput ProcessUserInput { get; set; }
        ILevelWatcher LevelWatcher { get; set; }
    }
}
