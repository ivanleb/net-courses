using System;

namespace SESimulator.Abstractions
{
    public interface IUserInput
    {
        void ListenToUser();
        event EventHandler<ConsoleKey> OnUserInputRecieved;
    }
}
